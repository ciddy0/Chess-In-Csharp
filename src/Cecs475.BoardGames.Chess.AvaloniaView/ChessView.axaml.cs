using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cecs475.BoardGames.AvaloniaView;
using Cecs475.BoardGames.Chess.Model;
using System;
using System.Linq;

namespace Cecs475.BoardGames.Chess.AvaloniaView;

public partial class ChessView : UserControl, IAvaloniaGameView {
	public ChessView() {
		InitializeComponent();
	}

	public Control ViewControl => this;

	public IGameViewModel ViewModel => (IGameViewModel)this.FindResource("vm")!;
	public ChessViewModel ChessViewModel => (ChessViewModel)ViewModel;
}