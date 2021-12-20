using System;
using System.Windows.Controls;
using Core;

namespace Gear
{
    /// <summary>
    /// Interaction logic for ToothShapeControl.xaml
    /// </summary>
    public partial class ToothShapeControl : UserControl
    {
        public ToothShapeControl()
        {
            InitializeComponent();
            ShapesComboBox.ItemsSource = Enum.GetValues(typeof(ToothShapeEnum));
        }
    }
}
