﻿using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;

namespace Cecs475.BoardGames.Othello.AvaloniaView {
	public class OthelloAdvantageConverter : IValueConverter {
		public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
			if (value is not GameAdvantage v) {
				throw new ArgumentException();
			}
			if (v.Player == 0)
				return "Tie game";
			if (v.Player == 1)
				return $"Black is winning by {v.Advantage} pieces";
			return $"White is winning by {v.Advantage} pieces";
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
