using System;
using Cecs475.BoardGames.Model;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Collections.Generic;

namespace Cecs475.BoardGames.Chess.AvaloniaView {
    public class ChessSquareBackgroundConverter : IMultiValueConverter {
        private static readonly IBrush DARK_BRUSH = Brushes.OliveDrab;
        private static readonly IBrush LIGHT_BRUSH = Brushes.Linen;
        private static readonly IBrush HIGHLIGHT_BRUSH = Brushes.Khaki;
        private static readonly IBrush SELECTED_BRUSH = Brushes.DarkKhaki;
        private static readonly IBrush INCHECK_BRUSH = Brushes.DarkRed;
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) {
            if (values.Count < 4 
                || values[0] is not BoardPosition pos 
                || values[1] is not bool isHighlighted
                || values[2] is not bool isSelected
                || values[3] is not bool isInCheck) 
                return LIGHT_BRUSH;
            
            if (isHighlighted)
                return HIGHLIGHT_BRUSH;
            if (isSelected)
                return SELECTED_BRUSH;
            if (isInCheck)
                return INCHECK_BRUSH;
            
            int sum = pos.Row + pos.Col;
            return (sum % 2 == 0) ? LIGHT_BRUSH : DARK_BRUSH;
        }
    }
}

