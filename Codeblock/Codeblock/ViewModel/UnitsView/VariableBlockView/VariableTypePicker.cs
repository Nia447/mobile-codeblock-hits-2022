using System;
using Xamarin.Forms;
using Codeblock.Model;

namespace Codeblock.ViewModel.UnitsView.VariableBlockView
{
    public class VariableTypePicker
    {
        Picker TypePicker;
        public Variable Variable;
        public VariableTypePicker(Variable variable)
        {
            Variable = variable;
            TypePicker = new Picker()
            {
                BackgroundColor = new Color(),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Title = "Type",
            };
            TypePicker.Items.Add("int");
            TypePicker.Items.Add("double");
            TypePicker.Items.Add("char");
            TypePicker.Items.Add("string");
            TypePicker.Items.Add("bool");
            TypePicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        }
        public Picker GetVariableTypePicker()
        {
            return TypePicker;
        }
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Variable.Type = picker.Items[selectedIndex];
            }
        }
    }
}
