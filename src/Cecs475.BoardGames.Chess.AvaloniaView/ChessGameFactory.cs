using Avalonia.Data.Converters;
using Cecs475.BoardGames.AvaloniaView;
using System;

namespace Cecs475.BoardGames.Chess.AvaloniaView {
	public class ChessGameFactory : IAvaloniaGameFactory {
		public string GameName {
			get {
				return "Chess";
			}
		}

		public IValueConverter CreateBoardAdvantageConverter() {
			
			return new ChessAdvantageConverter();

			// TODO: after creating a ChessAdvantageConverter, construct and return
			// an object of that class.
			//return null;

		}

		public IValueConverter CreateCurrentPlayerConverter() {
			
			return new ChessCurrentPlayerConverter();

			// TODO: after creating a ChessCurrentPlayerConverter, construct and return
			// an object of that class.
			//return null;
		}

		public IAvaloniaGameView CreateGameView() {
			return new ChessView();
		}
	}
}
