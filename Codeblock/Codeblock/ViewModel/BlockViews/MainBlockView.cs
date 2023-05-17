using System;
using System.Collections.Generic;
using System.Text;
using Codeblock.Model;
using Codeblock.ViewModel.BlockViews;
using Codeblock.ViewModel.UnitsView;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace Codeblock.ViewModel.BlockViews
{
    public class MainBlockView
    {
        private StackLayout ElderBlockLayout;
        private AbsoluteLayout AbsoluteParent;
        private List<BlockView> BlockViews;
        private BlockView RunCodeBlock;
        private Field Field;

        private RunPointView RPV;//run point(Yellow Block)
        private NewUnitButton NUB;

        private List<ConnectionLine> Line;
        private Point point;

        public ElderCodeBlock ElderCodeBlock;
        public MainBlockView(AbsoluteLayout absoluteParent, List<BlockView> blockViews, Field field)
        {
            AbsoluteParent = absoluteParent;
            BlockViews = blockViews;
            Field = field;
            Line = new List<ConnectionLine>();
            Line.Add(new ConnectionLine(Helpers.Helper.RunPointColor));
            point = new Point(0, 0);

            ElderCodeBlock = new ElderCodeBlock();

            ElderBlockLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
            };

            AddRunCodeBlock();
            ComposeElderBlockView();
        }
        private void ComposeElderBlockView()
        {
            StackLayout Framelayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
            };

            NUB = new NewUnitButton(true, Framelayout, AbsoluteParent, Field, ElderCodeBlock.StartCodeBlock);

            RPV = new RunPointView(RunCodeBlock);

            Framelayout.Children.Add(RPV.GetRunPointFrame());
            Framelayout.Children.Add(NUB.GetUnitBtn());

            Frame frame = new Frame()
            {
                Content = Framelayout,
                BackgroundColor = new Color(52.0 / 255.0, 0.0 / 255.0, 156.0 / 255.0),
                CornerRadius = 10,
                BorderColor = new Color(76.0 / 255.0, 0.0 / 255.0, 227.0 / 255.0),
                Margin = 0,
                Padding = Helpers.Helper.Padding,
            };

            StackLayout unitsStack = new StackLayout();

            PanGestureRecognizer localPanGestureRecognizer = new PanGestureRecognizer();
            PinchGestureRecognizer localPinchGestureRecognizer = new PinchGestureRecognizer();

            localPanGestureRecognizer.PanUpdated += MoveBlock;
            localPinchGestureRecognizer.PinchUpdated += PinchBlock;

            unitsStack.Children.Add(frame);

            ElderBlockLayout.GestureRecognizers.Add(localPanGestureRecognizer);
            ElderBlockLayout.GestureRecognizers.Add(localPinchGestureRecognizer);

            ElderBlockLayout.Children.Add(unitsStack);
        }
        private void AddRunCodeBlock()
        {
            RunCodeBlock = new BlockView(AbsoluteParent, ElderBlockLayout, Line[0], ElderCodeBlock.Main, Field);
            AbsoluteParent.Children.Add(
                RunCodeBlock.GetBlockView(),
                new Point(
                    AbsoluteParent.TranslationX + Helpers.Helper.BlockWidth + Helpers.Helper.BlockWidth/3,
                    AbsoluteParent.TranslationY + 200)
                );

            BlockViews.Add(RunCodeBlock);
            DrowLine();
        }
        #region MOVE,SCALE,PINCH
        private double startScale, currentScale, xOffset, yOffset;
        public void Move(object sender, PanUpdatedEventArgs e)
        {

            if (e.StatusType == GestureStatus.Started)
            {
                point = new Point(ElderBlockLayout.TranslationX, ElderBlockLayout.TranslationY);

            }
            if (e.StatusType == GestureStatus.Running)
            {
                ElderBlockLayout.TranslationX = point.X + e.TotalX;
                ElderBlockLayout.TranslationY = point.Y + e.TotalY;

                DrowLine();
            }
            if (e.StatusType == GestureStatus.Completed)
            {
                point = new Point(ElderBlockLayout.TranslationX, ElderBlockLayout.TranslationY);
            }

        }
        public void Pinch(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                startScale = ElderBlockLayout.Scale;
                ElderBlockLayout.AnchorX = 0;
                ElderBlockLayout.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                double Width = ElderBlockLayout.Width;
                double Height = ElderBlockLayout.Height;

                double renderedX = ElderBlockLayout.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (ElderBlockLayout.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                double renderedY = ElderBlockLayout.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (ElderBlockLayout.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                double targetX = xOffset - (originX * ElderBlockLayout.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * ElderBlockLayout.Height) * (currentScale - startScale);

                ElderBlockLayout.TranslationX = targetX;
                ElderBlockLayout.TranslationY = targetY;

                ElderBlockLayout.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                xOffset = ElderBlockLayout.TranslationX;
                yOffset = ElderBlockLayout.TranslationY;
            }
        }
        public void MoveBlock(object sender, PanUpdatedEventArgs e)
        {

            switch (e.StatusType)
            {
                case GestureStatus.Running:

                    var maxTranslationX = ElderBlockLayout.Scale * ElderBlockLayout.Width - ElderBlockLayout.Width;
                    ElderBlockLayout.TranslationX = ElderBlockLayout.TranslationX + e.TotalX;//xOffset + e.TotalX - startX;

                    var maxTranslationY = ElderBlockLayout.Scale * ElderBlockLayout.Height - ElderBlockLayout.Height;
                    ElderBlockLayout.TranslationY = ElderBlockLayout.TranslationY + e.TotalY;//yOffset + e.TotalY - startY;

                    DrowLine();
                    break;
            }
            point = new Point(ElderBlockLayout.TranslationX, ElderBlockLayout.TranslationY);
        }
        public void PinchBlock(object sender, PinchGestureUpdatedEventArgs e) { }
        #endregion
        public void DrowLine()
        {
            foreach(ConnectionLine l in Line)
            {
                AbsoluteParent.Children.Remove(l.GetLine());
            }
            int i = 0;
            foreach(BlockView bv in BlockViews)
            {
                AbsoluteParent.Children.Add(Line[i].DrowLine(
                    new Point(
                        ElderBlockLayout.TranslationX + Helpers.Helper.Padding + Helpers.Helper.BlockWidth - Helpers.Helper.Bias,
                        ElderBlockLayout.TranslationY + Helpers.Helper.Padding + Helpers.Helper.Bias),
                    new Point(
                        bv.GetBlockView().TranslationX + Helpers.Helper.BlockWidth + Helpers.Helper.Bias + Helpers.Helper.BlockWidth / 3,
                        bv.GetBlockView().TranslationY + Helpers.Helper.Bias + 200)
                    )
                );
                i++;
            }
        }
        public StackLayout GetElderBlock()
        {
            return ElderBlockLayout;
        }
    }
}
