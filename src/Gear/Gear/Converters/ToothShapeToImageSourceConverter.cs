using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Core;

namespace Gear.Converters
{
    /// <summary>
    /// Класс для преобразования формы зуба в соответствующую картинку
    /// </summary>
    public class ToothShapeToImageSourceConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ToothShapeEnum.Trapezoid:
                {
                    return "Resources/Trapezoid.png";
                }
                case ToothShapeEnum.Triangle:
                {
                    return "Resources/Triangle.png";
                }
                case ToothShapeEnum.TrapezoidRectangle:
                {
                    return "Resources/TrapezoidRectangle.png";
                }
                default:
                {
                    return "";
                }
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}