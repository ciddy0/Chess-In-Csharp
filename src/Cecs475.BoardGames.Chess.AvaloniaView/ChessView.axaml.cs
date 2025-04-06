using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cecs475.BoardGames.AvaloniaView;
using Cecs475.BoardGames.Chess.Model;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cecs475.BoardGames.Chess.AvaloniaView;

public partial class ChessView : UserControl, IAvaloniaGameView {
	public ChessView() {
		InitializeComponent();
	}

	public ChessViewModel ChessViewModel => (ChessViewModel)Resources["vm"];
	public Control ViewControl => this;
	public IGameViewModel ViewModel => ChessViewModel;
	
	private void Panel_PointerEntered(object? sender, Avalonia.Input.PointerEventArgs e) {
		if (sender is not Control b) {
			throw new ArgumentException(nameof(sender));
		}
		var square = (ChessSquare)b.DataContext!;
		var vm = (ChessViewModel)Resources["vm"];
		if (vm.PossibleMoves.Contains(square.Position)) {
			square.IsHighlighted = true;
		}
	}

	private void Panel_PointerExited(object? sender, Avalonia.Input.PointerEventArgs e) {
		if (sender is not Control b) { throw new ArgumentException(nameof(sender)); }
		var square = (ChessSquare)b.DataContext!;
		square.IsHighlighted = false;
	}
	
	private void Panel_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e) {
		if (sender is not Control b) {
			throw new ArgumentException(nameof(sender));
		}
		var square = (ChessSquare)b.DataContext!;
		var vm = (ChessViewModel)Resources["vm"]!;
		if (vm.PossibleMoves.Contains(square.Position)) {
			vm.ApplyMove(square.Position);
			square.IsHighlighted = false;
		}

		if (!vm.PossibleMoves.Any()) {
			//MessageBoxManager
		}
	}
}