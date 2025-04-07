using Cecs475.BoardGames.Chess.Model;

namespace Cecs475.BoardGames.Chess.AvaloniaView;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;

public class ChessPieceImageConverter: IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ChessSquare square && square.PieceType != ChessPieceType.Empty)
        {
            string pieceImage = GetPieceImage(square.Player, square.PieceType);
            if (!string.IsNullOrEmpty(pieceImage))
            {
                return new Bitmap(
                    AssetLoader.Open(
                        new Uri($"$\"avares://Cecs475.BoardGames.Chess.AvaloniaView/Resources/{pieceImage}.svg")));
            }
        }
        return null;
    }

    private string GetPieceImage(int player, ChessPieceType pieceType)
    {
        
        switch (player)
        {
            case 1: // White pieces
                return pieceType switch {
                    ChessPieceType.Pawn => "pawn-w",
                    ChessPieceType.King => "king-w",
                    ChessPieceType.Queen => "queen-w",
                    ChessPieceType.Rook => "rook-w",
                    ChessPieceType.Bishop => "bishop-w",
                    ChessPieceType.Knight => "knight-w",
                };

            case 2: // Black pieces
                return pieceType switch {
                    ChessPieceType.Pawn => "pawn-b",
                    ChessPieceType.King => "king-b",
                    ChessPieceType.Queen => "queen-b",
                    ChessPieceType.Rook => "rook-b",
                    ChessPieceType.Bishop => "bishop-b",
                    ChessPieceType.Knight => "knight-b",
                };

            default:
                return string.Empty;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
    