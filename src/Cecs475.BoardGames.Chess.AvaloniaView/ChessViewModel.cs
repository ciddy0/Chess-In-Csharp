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

		public int Player {
			get { return mPlayer; }
			set {
				if (value != mPlayer) {
					mPlayer = value;
					OnPropertyChanged();
				}
			}
		}
		private ChessPieceType mPieceType;
		public ChessPieceType PieceType
		{
			get { return mPieceType; }
			set
			{
				if (value != mPieceType)
				{
					mPieceType = value;
					OnPropertyChanged();
				}
			}
		}
		public BoardPosition Position {
			get; set;
		}
		
		private bool mIsHighlighted;
		private bool mIsSelected;
		private bool mIsInCheck;

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

		public bool IsSelected 
		{
			get 
			{
				return mIsSelected; 
			}
			set 
			{
				if (value != mIsSelected) 
				{
					mIsSelected = value;
					OnPropertyChanged();
				}
			}
		}
		
		public bool IsInCheck
		{
			get
			{
				return mIsInCheck;
			}
			set
			{
				if (mIsInCheck != value)
				{
					mIsInCheck = value;
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
	
	public class ChessViewModel : INotifyPropertyChanged, IGameViewModel{
		private readonly ChessBoard mBoard;
		private readonly ObservableCollection<ChessSquare> mSquares;
		private BoardPosition? mSelectedSquare;
		public event EventHandler? GameFinished;
		
		public ChessViewModel() {
			mBoard = new ChessBoard();
			mSquares = new ObservableCollection<ChessSquare>();
			
			// Populate the board with ChessSquare objects and bind the property change listeners
			for (int row = 0; row < 8; row++) {
				for (int col = 0; col < 8; col++) {
					var pos = new BoardPosition(row, col);
					var piece = mBoard.GetPieceAtPosition(pos);
					var square = new ChessSquare {
						Position = pos,
						Player = mBoard.GetPlayerAtPosition(new BoardPosition(row, col)),
						PieceType = piece.PieceType,
						IsHighlighted = false
					};
					square.PropertyChanged += ChessSquare_PropertyChanged;
					mSquares.Add(square);
				}
			}
			PossibleMoves = new HashSet<BoardPosition>(
				from ChessMove m in mBoard.GetPossibleMoves()
				select m.StartPosition
			);
		}

		public ObservableCollection<ChessSquare> Squares {
			get { return mSquares; }
		}
		
		public int CurrentPlayer {
			get {return mBoard.CurrentPlayer; }
		}
		
		public bool CanUndo => mBoard.MoveHistory.Any();

		public GameAdvantage BoardAdvantage => mBoard.CurrentAdvantage;
		
		public HashSet<BoardPosition> PossibleMoves { get; private set; }
		public void UndoMove() {
			if (CanUndo) {
				mBoard.UndoLastMove();
				RebindState();
			}
		}

		private void RebindState() 
		{
			bool kingInCheck = mBoard.IsCheck;
			
			foreach (var square in mSquares) {
				square.Player = mBoard.GetPlayerAtPosition(square.Position);
				square.PieceType = mBoard.GetPieceAtPosition(square.Position).PieceType;
				square.IsHighlighted = false;
				square.IsSelected = false;

				if (kingInCheck
				    && square.PieceType == ChessPieceType.King
				    && square.Player == mBoard.CurrentPlayer)
				{
					square.IsSelected = true;
				}
				else
				{
					square.IsInCheck = false;
				}

			}
    
			OnPropertyChanged(nameof(BoardAdvantage));
			OnPropertyChanged(nameof(CurrentPlayer));
			OnPropertyChanged(nameof(CanUndo));
		}

		public void UpdatePossibleMovesForSelectedSquare(BoardPosition? selected)
		{
			if (selected == null)
			{
				PossibleMoves = new HashSet<BoardPosition>(
					from ChessMove m in mBoard.GetPossibleMoves()
					select m.StartPosition
				);
			} else {
				PossibleMoves = new HashSet<BoardPosition>(
					from ChessMove m in mBoard.GetPossibleMoves().Cast<ChessMove>()
					where m.StartPosition.Equals(selected)
					select m.EndPosition
				);
			}
			OnPropertyChanged(nameof(PossibleMoves));

		}
		
		public void ApplyMove(BoardPosition position) {
			var possMoves = mBoard.GetPossibleMoves().Cast<ChessMove>();
			foreach (var move in possMoves) {
				if (SelectedSquare != null &&
				    move.StartPosition.Equals(SelectedSquare) &&
				    move.EndPosition.Equals(position)) {
					mBoard.ApplyMove(move);
					break;
				}
			}

			SelectedSquare = null;
			RebindState();

			if (mBoard.IsFinished) {
				GameFinished?.Invoke(this, EventArgs.Empty);
			}
		}

		public BoardPosition? SelectedSquare
		{
			get => mSelectedSquare;
			set
			{
				if (mSelectedSquare != value)
				{
					mSelectedSquare = value;
					OnPropertyChanged();
					UpdatePossibleMovesForSelectedSquare(mSelectedSquare);
				}
			}
		}
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		// Event handler for ChessSquare changes
		private void ChessSquare_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
			OnPropertyChanged(nameof(Squares)); 
		}

	}
}
