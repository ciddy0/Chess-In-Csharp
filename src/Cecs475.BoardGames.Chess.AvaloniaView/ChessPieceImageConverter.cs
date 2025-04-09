using Cecs475.BoardGames.Chess.Model;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace Cecs475.BoardGames.Chess.AvaloniaView
{
public class ChessPieceImageConverter: IMultiValueConverter {
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        Console.WriteLine(values);
        if (values.Count >= 2 &&
            values[0] is ChessPieceType pieceType &&
            values[1] is int player &&
            pieceType != ChessPieceType.Empty)
        {
            string pieceImage = GetPieceImage(player, pieceType);
            Console.WriteLine(pieceImage);
            
            if (!string.IsNullOrEmpty(pieceImage))
            {
                Console.WriteLine($"Converting: {pieceType} {player}");
                Console.WriteLine($"Trying to load: avares://Cecs475.BoardGames.Chess.AvaloniaView/Resources/{pieceImage}.png");
                return new Bitmap(
                    AssetLoader.Open(
                        new Uri($"avares://Cecs475.BoardGames.Chess.AvaloniaView/Resources/{pieceImage}.png")));
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
                    ChessPieceType.Pawn => "wp",
                    ChessPieceType.King => "wk",
                    ChessPieceType.Queen => "wq",
                    ChessPieceType.Rook => "wr",
                    ChessPieceType.Bishop => "wb",
                    ChessPieceType.Knight => "wn",
                };

            case 2: // Black pieces
                return pieceType switch {
                    ChessPieceType.Pawn => "bp",
                    ChessPieceType.King => "bk",
                    ChessPieceType.Queen => "bq",
                    ChessPieceType.Rook => "br",
                    ChessPieceType.Bishop => "bb",
                    ChessPieceType.Knight => "bn",
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
