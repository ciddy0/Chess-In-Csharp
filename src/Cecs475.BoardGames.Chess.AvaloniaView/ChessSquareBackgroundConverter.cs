using System;
using Cecs475.BoardGames.Model;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Cecs475.BoardGames.Chess.AvaloniaView 
{
    class ChessSquareBackgroundConverter : IValueConverter
    {
        private static readonly IBrush HIGHLIGHT_BRUSH = Brushes.Red;
        private static readonly IBrush DARK_BRUSH = Brushes.DarkGreen;
        private static readonly IBrush LIGHT_BRUSH = Brushes.LightGreen;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChessSquare square)
            {
                if (square.IsHighlighted)
                {
                    return HIGHLIGHT_BRUSH;
                }

                int sum = square.Position.Row + square.Position.Col;
                return (sum % 2 == 0) ? LIGHT_BRUSH : DARK_BRUSH;
            }

            return LIGHT_BRUSH;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}

