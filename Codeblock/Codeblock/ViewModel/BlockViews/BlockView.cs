using Codeblock.ViewModel.UnitsView;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Codeblock.Model;
using Xamarin.Forms.Shapes;

namespace Codeblock.ViewModel.BlockViews
{
    public class BlockView
    {
        private StackLayout BlockLayout;
        private AbsoluteLayout AbsoluteParent;
        private Point point;
        private List<ConnectionLine> Line;
        private ConnectionLine ParentLine;
        private View Parent;
        private List<BlockView> BlockViews;
        private Field Field;

        public CodeBlock CodeBlock;
        public BlockView(AbsoluteLayout absoluteParent, View parent, ConnectionLine line, CodeBlock codeBlock, Field field)
        {
            AbsoluteParent = absoluteParent;
            Line = new List<ConnectionLine>();
            BlockViews = new List<BlockView>();
            ParentLine = line;
            Parent = parent;
            Field = field;
            CodeBlock = codeBlock;
            BlockLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
            };
            ComposeBlockView();
        }
        private void ComposeBlockView()
        {
            SetAllLines();

            StackLayout FrameLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
            };

            NewUnitButton NUB = new NewUnitButton(false, FrameLayout, AbsoluteParent, Field, CodeBlock);

            FrameLayout.Children.Add(NUB.GetUnitBtn());

            Frame frame = new Frame()
            {
                Content = FrameLayout,
                BackgroundColor = new Color(52.0 / 255.0, 0.0 / 255.0, 156.0 / 255.0),
                CornerRadius = 10,
                BorderColor = ParentLine.Color,
                Margin = 0,
                WidthRequest = Helpers.Helper.BlockWidth,
                Padding = Helpers.Helper.Padding,
            };

            PanGestureRecognizer localPanGestureRecognizer = new PanGestureRecognizer();
            PinchGestureRecognizer localPinchGestureRecognizer = new PinchGestureRecognizer();

            localPanGestureRecognizer.PanUpdated += MoveBlock;
            localPinchGestureRecognizer.PinchUpdated += PinchBlock;

            BlockLayout.GestureRecognizers.Add(localPanGestureRecognizer);
            BlockLayout.GestureRecognizers.Add(localPinchGestureRecognizer);

            BlockLayout.Children.Add(frame);
        }
        #region MOVE,SCALE,PINCH
        private double startScale, currentScale, xOffset, yOffset;
        public void Move(object sender, PanUpdatedEventArgs e)
        {

            if (e.StatusType == GestureStatus.Started)
            {
                point = new Point(BlockLayout.TranslationX, BlockLayout.TranslationY);
            }
            if (e.StatusType == GestureStatus.Running)
            {
                BlockLayout.TranslationX = point.X + e.TotalX;
                BlockLayout.TranslationY = point.Y + e.TotalY;

                DrowLineWithParent();
                DrawLineWithChildren();
            }
            if (e.StatusType == GestureStatus.Completed)
            {
                point = new Point(BlockLayout.TranslationX, BlockLayout.TranslationY);
            }

        }
        public void Pinch(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                startScale = BlockLayout.Scale;
                BlockLayout.AnchorX = 0;
                BlockLayout.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                double Width = BlockLayout.Width;
                double Height = BlockLayout.Height;

                double renderedX = BlockLayout.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (BlockLayout.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                double renderedY = BlockLayout.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (BlockLayout.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                double targetX = xOffset - (originX * BlockLayout.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * BlockLayout.Height) * (currentScale - startScale);

                BlockLayout.TranslationX = targetX;
                BlockLayout.TranslationY = targetY;

                BlockLayout.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                xOffset = BlockLayout.TranslationX;
                yOffset = BlockLayout.TranslationY;
            }
        }
        public void MoveBlock(object sender, PanUpdatedEventArgs e)
        {

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    var maxTranslationX = BlockLayout.Scale * BlockLayout.Width - BlockLayout.Width;
                    BlockLayout.TranslationX = BlockLayout.TranslationX + e.TotalX;

                    var maxTranslationY = BlockLayout.Scale * BlockLayout.Height - BlockLayout.Height;
                    BlockLayout.TranslationY = BlockLayout.TranslationY + e.TotalY;

                    DrowLineWithParent();
                    DrawLineWithChildren();
                    break;
            }
            point = new Point(BlockLayout.TranslationX, BlockLayout.TranslationY);
        }
        public void PinchBlock(object sender, PinchGestureUpdatedEventArgs e) { }
        #endregion
        #region Draw lines with other Blocks
        private void SetAllLines()
        {
            AbsoluteParent.Children.Add(ParentLine.GetLine());

            foreach (BlockView bv in BlockViews)
            {
                int i = 0;
                AbsoluteParent.Children.Add(Line[i].DrowLine(
                    new Point(
                        BlockLayout.TranslationX + Helpers.Helper.Padding + Helpers.Helper.BlockWidth - Helpers.Helper.Bias,
                        BlockLayout.TranslationY + Helpers.Helper.Padding + Helpers.Helper.Bias),
                    new Point(
                        bv.GetBlockView().TranslationX + Helpers.Helper.BlockWidth + Helpers.Helper.Bias + Helpers.Helper.BlockWidth / 3,
                        bv.GetBlockView().TranslationY + Helpers.Helper.Bias + 200)
                    )
                );
                i++;
            }
        }
        private void DrowLineWithParent()
        {
            AbsoluteParent.Children.Remove(ParentLine.GetLine());

            AbsoluteParent.Children.Add(ParentLine.DrowLine(
                new Point(
                    Parent.TranslationX + Helpers.Helper.Padding + Helpers.Helper.BlockWidth - Helpers.Helper.Bias,
                    Parent.TranslationY + Helpers.Helper.Padding + Helpers.Helper.Bias),
                new Point(
                    BlockLayout.TranslationX + Helpers.Helper.BlockWidth + Helpers.Helper.Bias + Helpers.Helper.BlockWidth / 3,
                    BlockLayout.TranslationY + Helpers.Helper.Bias + 200)
                ));
        }
        private void DrawLineWithChildren()
        {
            foreach(ConnectionLine l in Line)
            {
                AbsoluteParent.Children.Remove(l.GetLine());
            }

            foreach (BlockView bv in BlockViews)
            {
                int i = 0;

                AbsoluteParent.Children.Add(Line[i].DrowLine(
                    new Point(
                        BlockLayout.TranslationX + Helpers.Helper.Padding + Helpers.Helper.BlockWidth - Helpers.Helper.Bias,
                        BlockLayout.TranslationY + Helpers.Helper.Padding + Helpers.Helper.Bias),
                    new Point(
                        bv.GetBlockView().TranslationX + Helpers.Helper.BlockWidth + Helpers.Helper.Bias + Helpers.Helper.BlockWidth / 3,
                        bv.GetBlockView().TranslationY + Helpers.Helper.Bias + 200)
                    )
                );
                i++;
            }
        }
        #endregion
        public View GetBlockView()
        {
            return BlockLayout;
        }
    }
}
