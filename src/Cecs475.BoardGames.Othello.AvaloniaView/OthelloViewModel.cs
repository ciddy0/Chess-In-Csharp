﻿using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Cecs475.BoardGames.AvaloniaView;
using System;

using Cecs475.BoardGames.Othello.Model;
using Cecs475.BoardGames.Model;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Cecs475.BoardGames.Othello.AvaloniaView {
	/// <summary>
	/// Represents one square on the Othello board grid.
	/// </summary>
	public class OthelloSquare : INotifyPropertyChanged {
		public OthelloSquare Self => this;

		private int mPlayer;
		/// <summary>
		/// The player that has a piece in the given square, or 0 if empty.
		/// </summary>
		public int Player {
			get { return mPlayer; }
			set {
				if (value != mPlayer) {
					mPlayer = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// The position of the square.
		/// </summary>
		public BoardPosition Position {
			get; set;
		}


		private bool mIsHighlighted;
		/// <summary>
		/// Whether the square should be highlighted because of a user action.
		/// </summary>
		public bool IsHighlighted {
			get { return mIsHighlighted; }
			set {
				if (value != mIsHighlighted) {
					mIsHighlighted = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public override string ToString() {
			return $"Square {Position}";
		}
	}

	/// <summary>
	/// Represents the game state of a single othello game.
	/// </summary>
	public class OthelloViewModel : INotifyPropertyChanged, IGameViewModel {
		private readonly OthelloBoard mBoard;
		private readonly ObservableCollection<OthelloSquare> mSquares;
		public event EventHandler? GameFinished;

		public OthelloViewModel() {
			mBoard = new OthelloBoard();

			// Initialize the squares objects based on the board's initial state.
			mSquares = new ObservableCollection<OthelloSquare>(
				BoardPosition.GetRectangularPositions(8, 8)
				.Select(pos => new OthelloSquare() {
					Position = pos,
					Player = mBoard.GetPlayerAtPosition(pos)
				})
			);

			PossibleMoves = new HashSet<BoardPosition>(
				from OthelloMove m in mBoard.GetPossibleMoves()
				select m.Position
			);
		}

		/// <summary>
		/// Applies a move for the current player at the given position.
		/// </summary>
		public void ApplyMove(BoardPosition position) {
			IEnumerable<OthelloMove> possMoves = mBoard.GetPossibleMoves();
			// Validate the move as possible.
			foreach (var move in possMoves) {
				if (move.Position.Equals(position)) {
					mBoard.ApplyMove(move);
					break;
				}
			}

			RebindState();

			if (mBoard.IsFinished) {
				GameFinished?.Invoke(this, new EventArgs());
			}
		}

		private void RebindState() {
			// Rebind the possible moves, now that the board has changed.
			PossibleMoves = new HashSet<BoardPosition>(
				from OthelloMove m in mBoard.GetPossibleMoves()
				select m.Position
			);

			// Update the collection of squares by examining the new board state.
			var newSquares = BoardPosition.GetRectangularPositions(8, 8);
			int i = 0;
			foreach (var pos in newSquares) {
				mSquares[i].Player = mBoard.GetPlayerAtPosition(pos);
				i++;
			}
			OnPropertyChanged(nameof(BoardAdvantage));
			OnPropertyChanged(nameof(CurrentPlayer));
			OnPropertyChanged(nameof(CanUndo));
		}

		/// <summary>
		/// A collection of 64 OthelloSquare objects representing the state of the 
		/// game board.
		/// </summary>
		public ObservableCollection<OthelloSquare> Squares {
			get { return mSquares; }
		}

		/// <summary>
		/// A set of board positions where the current player can move.
		/// </summary>
		public HashSet<BoardPosition> PossibleMoves {
			get; private set;
		}

		/// <summary>
		/// The player whose turn it currently is.
		/// </summary>
		public int CurrentPlayer {
			get { return mBoard.CurrentPlayer; }
		}

		/// <summary>
		/// The value of the othello board.
		/// </summary>

		public GameAdvantage BoardAdvantage => mBoard.CurrentAdvantage;

		public bool CanUndo => mBoard.MoveHistory.Any();

		public NumberOfPlayers Players { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public async Task MakeAIMoveAsync()
		{
			
		}

		public void UndoMove() {
			if (CanUndo) {
				mBoard.UndoLastMove();
				// In one-player mode, Undo has to remove an additional move to return to the
				// human player's turn.
				if (Players == NumberOfPlayers.One && CanUndo) {
					mBoard.UndoLastMove();
				}
				RebindState();
			}
		}
	}
}
