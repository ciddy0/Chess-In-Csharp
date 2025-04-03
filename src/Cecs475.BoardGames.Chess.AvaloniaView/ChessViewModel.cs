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
	
	public class ChessSquare : INotifyPropertyChanged
	{
		public ChessSquare Self => this;
		private int mPlayer;

		public int Player
		{
			get { return mPlayer; }
			set
			{
				if (value != mPlayer)
				{
					mPlayer = value;
					OnPropertyChanged();
				}
			}
		}
		
		public BoardPosition Position
		{
			get; set;
		}
		
		private bool mIsHighlighted;

		public bool IsHighlighted 
		{
			get
			{
				return mIsHighlighted; 
				
			}
			set {
				if (value != mIsHighlighted) 
				{
					mIsHighlighted = value;
					OnPropertyChanged();
				}
			}
		}
		
		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string? name = null) 
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public override string ToString() 
		{
			return $"Square {Position}";
		}
		
	}
	
	public class ChessViewModel : INotifyPropertyChanged, IGameViewModel{
		private readonly ChessBoard mBoard;
		private readonly ObservableCollection<ChessSquare> mSquares;
		private BoardPosition? mSelectedSquare;
		
		public ChessViewModel() {
		}

		public ObservableCollection<ChessSquare> Squares {
			get { return mSquares; }
		}
		
		public int CurrentPlayer 
		{
			get {return mBoard.CurrentPlayer; }
		}
		
		public bool CanUndo => mBoard.MoveHistory.Any();

		public GameAdvantage BoardAdvantage => mBoard.CurrentAdvantage;
		
		public void UndoMove()
		{
			if (CanUndo)
			{
				mBoard.UndoLastMove();
				RebindState();
			}
		}

		private void RebindState()
		{
			foreach (var square in mSquares)
			{
				square.Player = mBoard.GetPlayerAtPosition(square.Position);
				square.IsHighlighted = false;
			}
			
			PossibleMoves = new HashSet<BoardPosition>(
				mBoard.GetPossibleMoves().Cast<ChessMove>().Select(move => move.StartPosition)
				);
			
			OnPropertyChanged(nameof(PossibleMoves));
			OnPropertyChanged(nameof(BoardAdvantage));
			OnPropertyChanged(nameof(CurrentPlayer));
			OnPropertyChanged(nameof(CanUndo));
		}

		public void ApplyMove(BoardPosition position)
		{
			IEnumerable<ChessMove> possMoves = mBoard.GetPossibleMoves().Cast<ChessMove>();
			foreach (var move in possMoves)
			{
				if (move.EndPosition.Equals(position))
				{
					mBoard.ApplyMove(move);
					break;
				}
			}

			RebindState();

			if (mBoard.IsFinished)
			{
				GameFinished?.Invoke(this, new EventArgs());
			}
		}

		public HashSet<BoardPosition> PossibleMoves {
			get; private set;
		}
		
		public event EventHandler? GameFinished;
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}
