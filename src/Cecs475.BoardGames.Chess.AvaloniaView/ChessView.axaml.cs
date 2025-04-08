using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Cecs475.BoardGames.AvaloniaView;
using Cecs475.BoardGames.Chess.Model;
using System;
using System.Linq;

namespace Cecs475.BoardGames.Chess.AvaloniaView {
    public partial class ChessView : UserControl, IAvaloniaGameView {
        // Member variable to track the currently selected square.
        private ChessSquare? _selectedSquare;
        
        public ChessView() {
            InitializeComponent();
        }

        public ChessViewModel ChessViewModel => (ChessViewModel)Resources["vm"];
        public Control ViewControl => this;
        public IGameViewModel ViewModel => ChessViewModel;

        private void Panel_PointerEntered(object? sender, PointerEventArgs e) {
            if (sender is not Control b)
                throw new ArgumentException(nameof(sender));
            var square = (ChessSquare)b.DataContext!;
            var vm = (ChessViewModel)Resources["vm"];

            // Use this event for highlighting moves as before.
            if (vm.PossibleMoves.Contains(square.Position)) {
                square.IsHighlighted = true;
            }
        }

        private void Panel_PointerExited(object? sender, PointerEventArgs e) {
            if (sender is not Control b)
                throw new ArgumentException(nameof(sender));
            var square = (ChessSquare)b.DataContext!;
            square.IsHighlighted = false;
        }
        
        private void Panel_PointerReleased(object? sender, PointerReleasedEventArgs e) {
            if (sender is not Control b)
                throw new ArgumentException(nameof(sender));

            var square = (ChessSquare)b.DataContext!;
            var vm = (ChessViewModel)Resources["vm"]!;

            // If the square is one of the possible moves for the currently selected piece, perform the move.
            if (vm.PossibleMoves.Contains(square.Position)) {
                vm.ApplyMove(square.Position);
                
                // Clear the selection since a move has been made.
                if (_selectedSquare != null) {
                    _selectedSquare.IsSelected = false;
                    _selectedSquare = null;
                }
                square.IsHighlighted = false;
                return;
            }

            // If the clicked square has a piece belonging to the current player, select it.
            if (square.PieceType != ChessPieceType.Empty && square.Player == vm.CurrentPlayer) {
                // Deselect the previously selected square if needed.
                if (_selectedSquare != null) {
                    _selectedSquare.IsSelected = false;
                }
                square.IsSelected = true;
                _selectedSquare = square;
                // You might also update vm.PossibleMoves here based on the new selection.
            }
            else {
                // If clicking on an invalid square, clear the selection.
                if (_selectedSquare != null) {
                    _selectedSquare.IsSelected = false;
                    _selectedSquare = null;
                }
            }
        }
    }
}
