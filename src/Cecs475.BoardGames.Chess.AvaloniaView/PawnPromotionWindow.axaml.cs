using Cecs475.BoardGames;
using Cecs475.BoardGames.AvaloniaView;
using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace Cecs475.BoardGames.Chess.AvaloniaView {

    public partial class PawnPromotionWindow : Window {
        private readonly ChessViewModel mViewModel;
        private readonly BoardPosition mStart;
        private readonly BoardPosition mEnd;
        
        public PawnPromotionWindow(ChessViewModel viewModel, BoardPosition start, BoardPosition end) {
            InitializeComponent();
            mViewModel = viewModel;
            mStart = start;
            mEnd = end;
            DataContext = viewModel;
        }

        public ChessViewModel ChessViewModel => (ChessViewModel)Resources["vm"];

        private void mKnightBtn_Click(object sender, RoutedEventArgs e) {
            mViewModel.ApplyPawnPromo(mStart, mEnd, ChessPieceType.Knight);
            Close();
        }

        private void mBishopBtn_Click(object sender, RoutedEventArgs e) {
            mViewModel.ApplyPawnPromo(mStart, mEnd, ChessPieceType.Bishop);
            Close();
        }

        private void mRookBtn_Click(object sender, RoutedEventArgs e) {
            mViewModel.ApplyPawnPromo(mStart, mEnd, ChessPieceType.Rook);
            Close();
        }
        private void mQueenBtn_Click(object sender, RoutedEventArgs e) {
            mViewModel.ApplyPawnPromo(mStart, mEnd, ChessPieceType.Queen);
            Close();
        }
        
        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }

    }
}