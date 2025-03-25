using Cecs475.BoardGames;
using Cecs475.BoardGames.AvaloniaView;
using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cecs475.BoardGames.Chess.AvaloniaView {
	
	/// <summary>
	/// Composes a 
	/// </summary>
	public class ChessViewModel : IGameViewModel {

		public ChessViewModel() {
		}

		public int CurrentPlayer {
			get {
				throw new NotImplementedException();
			}
		}

		public bool CanUndo {
			get {
				throw new NotImplementedException();
			}
		}

		public GameAdvantage BoardAdvantage => throw new NotImplementedException();


		public void UndoMove() {
			throw new NotImplementedException();
		}

		// Invoke this event after applying a move if the game is now finished.
		public event EventHandler? GameFinished;
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}
