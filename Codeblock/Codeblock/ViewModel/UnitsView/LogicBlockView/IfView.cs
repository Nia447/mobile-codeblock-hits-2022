using Codeblock.Model;
using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Codeblock.ViewModel.BlockViews;

namespace Codeblock.ViewModel.UnitsView.VariableBlockView
{
    public class IfView
    {
        private StackLayout PositionalStackLayout;
        private StackLayout FrameLayout;

        private AbsoluteLayout AbsoluteLayout;
        private Field Field;
        private List<ConnectionLine> Line;
        private List<BlockView> BlockViews;
        private Point point;

        public LogicObject LogicObject;
        public LogicBlock LogicBlock;
        public IfView(LogicBlock logicBlock, AbsoluteLayout absoluteLayout, Field field)
        {
            LogicBlock = logicBlock;
            LogicObject = new LogicObject(LogicBlock.CurrentCodeBlock);

            LogicBlock.AreaLogicObjects.Add(LogicObject);

            Field = field;
            AbsoluteLayout = absoluteLayout;
            BlockViews = new List<BlockView>();
            Line = new List<ConnectionLine>();
            Line.Add(new ConnectionLine(Helpers.Helper.RunPointColor));

            PositionalStackLayout = new StackLayout();
            
            ComposeIfView();
            ComposeBlockView();
        }
        public void ComposeIfView()
        {
            FrameLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(0 / 255.0, 255 / 255.0, 255 / 255.0),
            };

            Entry IfValueEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Placeholder = "Expression",
            };
            IfValueEntry.TextChanged += EntryValueChanged;

            FrameLayout.Children.Add(IfValueEntry);

            Frame frame = new Frame()
            {
                Content = FrameLayout,
                CornerRadius = 10,
                Padding = 0,
                BorderColor = new Color(0 / 255.0, 255 / 255.0, 255 / 255.0),
            };
            PositionalStackLayout.Children.Add(frame);

        }
        public void ComposeBlockView()
        {
            BlockViews.Add(new BlockView(AbsoluteLayout, FrameLayout, Line[0], LogicObject.Commands, Field));
            Field.AddBlockView(BlockViews[0]);
        }
        public void DrowLine()
        {
            foreach (ConnectionLine l in Line)
            {
                AbsoluteLayout.Children.Remove(l.GetLine());
            }
            int i = 0;
            foreach (BlockView bv in BlockViews)
            {
                AbsoluteLayout.Children.Add(Line[i].DrowLine(
                    new Point(
                        FrameLayout.TranslationX + Helpers.Helper.Padding + Helpers.Helper.BlockWidth - Helpers.Helper.Bias,
                        FrameLayout.TranslationY + Helpers.Helper.Padding + Helpers.Helper.Bias),
                    new Point(
                        bv.GetBlockView().TranslationX + Helpers.Helper.BlockWidth + Helpers.Helper.Bias + Helpers.Helper.BlockWidth / 3,
                        bv.GetBlockView().TranslationY + Helpers.Helper.Bias + 200)
                    )
                );
                i++;
            }
        }
        public void EntryValueChanged(object sender, TextChangedEventArgs e)
        {
            LogicObject.Input = e.NewTextValue;
        }
        public StackLayout GetIfView()
        {
            return PositionalStackLayout;
        }
    }
}
