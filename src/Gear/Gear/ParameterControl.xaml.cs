using System.Windows.Controls;
using Core;

namespace Gear
{
    /// <summary>
    /// Interaction logic for ParameterControl.xaml
    /// </summary>
    public partial class ParameterControl : UserControl
    {
        public ParameterControl()
        {
            InitializeComponent();
        }

        private void ParameterControl_OnError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Parameter)this.DataContext).IsValidData = false;
            }
        }
    }
}
