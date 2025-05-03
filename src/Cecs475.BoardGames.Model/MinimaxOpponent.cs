using System;
using Cecs475.BoardGames;
using Cecs475.BoardGames.Model;

namespace Cecs475.BoardGames.Model
{
    public static class MinimaxOpponent {
        
        private const int DefaultDepth = 4;
        
        public static IGameMove? FindBestMove(IGameBoard board) {
            bool isMaximizing = board.CurrentPlayer == 1;
            var (weight, move) = FindBestMove(board, DefaultDepth, isMaximizing);
            return move;
        }

        private static (long bestWeight, IGameMove? bestMove) FindBestMove(IGameBoard board, int depth, bool isMaximizing) {
            if (depth == 0 || board.IsFinished)
                return (GetWeight(board), null);

            long bestWeight = isMaximizing ? long.MinValue : long.MaxValue;
            IGameMove? bestMove = null;

            foreach (IGameMove move in board.GetPossibleMoves()) {
                board.ApplyMove(move);
                var (childWeight, _) = FindBestMove(board, depth - 1, !isMaximizing);
                board.UndoLastMove();

                if (isMaximizing) {
                    if (childWeight > bestWeight) {
                        bestWeight = childWeight;
                        bestMove = move;
                    }
                }
                else {
                    if (childWeight < bestWeight) {
                        bestWeight = childWeight;
                        bestMove = move;
                    }
                }
            }

            return (bestWeight, bestMove);
        }
        private static long GetWeight(IGameBoard board) {
            dynamic b = board;
            return (long)b.BoardWeight;
        }
    }
}