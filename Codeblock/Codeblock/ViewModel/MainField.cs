using Codeblock.ViewModel.BlockViews;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Codeblock.ViewModel
{
    public class Field
    {
        private AbsoluteLayout mainField;
        private List<StackLayout> StackLayoutList;
        private List<BlockView> BlockViews;

        public MainBlockView MainBlockView;
        public Field()
        {
            StackLayoutList = new List<StackLayout>();
            BlockViews = new List<BlockView>();
            mainField = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(0.0 / 255.0, 8.0 / 255.0, 20.0 / 255.0),
            };
            ComoseField();
        }
        private void ComoseField()
        {
            MainBlockView mbv = new MainBlockView(mainField, BlockViews, this);
            MainBlockView = mbv;
            PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += mbv.Move;
            foreach (BlockView blockView in BlockViews)
            {
                panGestureRecognizer.PanUpdated += blockView.Move;
            }
            mainField.GestureRecognizers.Add(panGestureRecognizer);
            mainField.Children.Add(mbv.GetElderBlock());
            MainBlockView.DrowLine();
        }
        public void AddBlockView(BlockView blockView)
        {
            PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += blockView.Move;
            mainField.GestureRecognizers.Add(panGestureRecognizer);
            mainField.Children.Add(blockView.GetBlockView());
        }
        public AbsoluteLayout GetField()
        {
            return mainField;
        }
    }
}
