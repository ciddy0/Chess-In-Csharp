using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Cecs475.BoardGames.AvaloniaView;
using Cecs475.BoardGames.Model;
using Avalonia.VisualTree;
using Cecs475.BoardGames.Chess.Model;
using System;
using System.Linq;

namespace Cecs475.BoardGames.Chess.AvaloniaView 
{
    public partial class ChessView : UserControl, IAvaloniaGameView 
    {
        public ChessView() 
        {
            InitializeComponent();
        }

        public ChessViewModel ChessViewModel => (ChessViewModel)Resources["vm"];
        public Control ViewControl => this;
        public IGameViewModel ViewModel => ChessViewModel;

        private void Panel_PointerEntered(object? sender, PointerEventArgs e) 
        {
            if (sender is not Control b)
            {             
                throw new ArgumentException(nameof(sender));
            }
            
            var square = (ChessSquare)b.DataContext!;
            var vm = (ChessViewModel)Resources["vm"];

            if (vm.PossibleMoves.Contains(square.Position)) 
            {
                square.IsHighlighted = true;
            }
        }

        private void Panel_PointerExited(object? sender, PointerEventArgs e)
        {
            if (sender is not Control b)
            {
                throw new ArgumentException(nameof(sender));
            }

            if (b.DataContext is not ChessSquare chessSquare)
                return;
            
            var square = (ChessSquare)b.DataContext!;
            square.IsHighlighted = false;
        }

        private void Panel_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (sender is not Control squareControl)
            {
                throw new ArgumentException(nameof(sender));
            } 
            
            var square = (ChessSquare)squareControl.DataContext!;
            var vm = (ChessViewModel)Resources["vm"]!;

            if (vm.SelectedSquare == null)
            {
                if (square.Player == vm.CurrentPlayer && vm.PossibleMoves.Contains(square.Position))
                {
                    vm.SelectedSquare = square.Position;  
                    square.IsSelected = true;            
                }
            }
            else
            {
                if (vm.PossibleMoves.Contains(square.Position)) 
                {
                    var selectedSquare = vm.Squares.FirstOrDefault(s => s.Position.Equals(vm.SelectedSquare));
                    if (selectedSquare != null && selectedSquare.PieceType == ChessPieceType.Pawn)
                    {
                        bool isPromotion = false;
                        
                        if (selectedSquare.Player == 1 && square.Position.Row == 0)
                            isPromotion = true;
                        else if (selectedSquare.Player == 2 && square.Position.Row == 7)
                            isPromotion = true;
                        
                        if (isPromotion)
                        {
                            HandlePawnPromo(selectedSquare.Position, square.Position);
                            return;
                        }
                    }
                    vm.ApplyMove(square.Position);
                }
                else 
                {
                    foreach (var s in vm.Squares)
                    {
                        if (s.Position.Equals(vm.SelectedSquare)) 
                        {
                            s.IsSelected = false;
                            break;
                        }
                    }
                    vm.SelectedSquare = null;
                }
            }
        }
        
        private async void HandlePawnPromo(BoardPosition start, BoardPosition end)
        {
            Window owner = this.GetVisualRoot() as Window;
            var promotionWindow = new PawnPromotionWindow(ChessViewModel, start, end);
            await promotionWindow.ShowDialog(owner);
        }

    }
}
