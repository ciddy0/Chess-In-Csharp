using Cecs475.BoardGames.Chess.Model;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;

namespace Cecs475.BoardGames.Chess.AvaloniaView
{
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
                        new Uri($"avares://Cecs475.BoardGames.Chess.AvaloniaView/imagesPNG/{pieceImage}.png")));
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
                    ChessPieceType.Pawn => "white-pawn",
                    ChessPieceType.King => "white-king",
                    ChessPieceType.Queen => "white-queen",
                    ChessPieceType.Rook => "white-rook",
                    ChessPieceType.Bishop => "white-bishop",
                    ChessPieceType.Knight => "white-knight",
                };

            case 2: // Black pieces
                return pieceType switch {
                    ChessPieceType.Pawn => "black-pawn",
                    ChessPieceType.King => "black-king",
                    ChessPieceType.Queen => "black-queen",
                    ChessPieceType.Rook => "black-rook",
                    ChessPieceType.Bishop => "black-bishop",
                    ChessPieceType.Knight => "black-knight",
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
        
}
