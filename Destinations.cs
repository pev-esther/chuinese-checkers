using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public static class Destinations
    {
        private static int[,] Dir = {
                         { -2,  0 }, // Up
                         {  0, -2 }, // Left
                         {  2,  0 }, // Down
                         {  0,  2 }  // Right
                            };
        private static int[,] Dir2 = {
                         { -1,  0 }, // Up
                         {  0, -1 }, // Left
                         {  1,  0 }, // Down
                         {  0,  1 }  // Right
                             };
        public static bool IsLegal(int row, int col) => row >= 0 && row <= 7 && col >= 0 && col <= 7;

        public static void give_possible_destinations1(game_Board board, Cell src, List<Move> possible_moves)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                int row = src.ROW + Dir2[dir, 0];
                int col = src.COL + Dir2[dir, 1];
                if (IsLegal(row, col))
                {
                    if (board.Get_I_J_cell(row, col).SUG == (int)CellType.EMPTY)
                    {
                        Move a_move = new Move(src, new Spot(row, col));
                        possible_moves.Add(a_move);
                    }
                }
            }
        }
        public static void give_possible_destinations2_Aux(game_Board board, Cell src, Cell RealSrc, int i, int j, List<Move> possible_moves, int wayN)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                if (dir == wayN)
                    continue;
                int row = i + Dir[dir, 0];
                int col = j + Dir[dir, 1];
                if (IsLegal(row, col))
                {
                    if (board.Get_I_J_cell(row, col).SUG == CellType.EMPTY &&
                        board.Get_I_J_cell(i + Dir2[dir, 0], j + Dir2[dir, 1]).SUG != CellType.EMPTY)
                    {
                        Move a_move = new Move(RealSrc, board.Get_I_J_cell(row, col));
                        possible_moves.Add(a_move);
                        give_possible_destinations2_Aux(board, board.Get_I_J_cell(row, col), RealSrc, row, col, possible_moves, (dir + 2) % 4);
                    }
                }
            }
        }
        public static void give_possible_destinations2(game_Board board, Cell src, List<Move> possible_moves)
        {
            give_possible_destinations2_Aux(board, src, src, src.ROW, src.COL, possible_moves, -1);
        }

    }
}
