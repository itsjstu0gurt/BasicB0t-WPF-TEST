using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BasicB0t.Enums;

namespace BasicB0t.Converters
{
    public class ChatColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChatColorPresets color)
            {
                return new SolidColorBrush(GetColorFromEnum(color));
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color GetColorFromEnum(ChatColorPresets color)
        {
            switch (color)
            {
                case ChatColorPresets.Blue:
                    return Colors.Blue;
                case ChatColorPresets.Coral:
                    return Color.FromRgb(255, 127, 80); // RGB(255, 127, 80)
                case ChatColorPresets.DodgerBlue:
                    return Color.FromRgb(30, 144, 255); // RGB(30, 144, 255)
                case ChatColorPresets.SpringGreen:
                    return Color.FromRgb(0, 255, 127); // RGB(0, 255, 127)
                case ChatColorPresets.YellowGreen:
                    return Color.FromRgb(154, 205, 50); // RGB(154, 205, 50)
                case ChatColorPresets.Green:
                    return Colors.Green;
                case ChatColorPresets.OrangeRed:
                    return Color.FromRgb(255, 69, 0); // RGB(255, 69, 0)
                case ChatColorPresets.Red:
                    return Colors.Red;
                case ChatColorPresets.GoldenRod:
                    return Color.FromRgb(218, 165, 32); // RGB(218, 165, 32)
                case ChatColorPresets.HotPink:
                    return Color.FromRgb(255, 105, 180); // RGB(255, 105, 180)
                case ChatColorPresets.CadetBlue:
                    return Color.FromRgb(95, 158, 160); // RGB(95, 158, 160)
                case ChatColorPresets.SeaGreen:
                    return Color.FromRgb(46, 139, 87); // RGB(46, 139, 87)
                case ChatColorPresets.Chocolate:
                    return Color.FromRgb(210, 105, 30); // RGB(210, 105, 30)
                case ChatColorPresets.BlueViolet:
                    return Color.FromRgb(138, 43, 226); // RGB(138, 43, 226)
                case ChatColorPresets.Firebrick:
                    return Color.FromRgb(178, 34, 34); // RGB(178, 34, 34)
                default:
                    return Colors.Transparent;
            }
        }
    }
}
