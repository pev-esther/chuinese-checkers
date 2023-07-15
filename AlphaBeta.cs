using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    internal static class AlphaBeta
    {
        private static int depth = 2;

        public static Move generate_step_alpha_beta(Player pink_player, Player green_player, game_Board board)
        {
            int beta = int.MaxValue;
            int best = int.MinValue;
            int grade;
            List<Move> possibleMoves = get_possible_moves(pink_player, board);
            Move BestMove = possibleMoves[0];
            for (int i = 0; i < possibleMoves.Count; i++)
            {
                make_move(possibleMoves[i], true, pink_player, green_player, board);
                grade = min_move(depth, best, beta, pink_player, green_player, board);
                if (grade > best)
                {
                    best = grade;
                    BestMove = possibleMoves[i];
                }
                undo_move(possibleMoves[i], true, pink_player, green_player, board);
            }
            return BestMove;
        }

     
        public static List<Move> get_possible_moves(Player x, game_Board board)
        {
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < x.my_pawns.Count; i++)
            {
                Cell cell = board.Get_I_J_cell(x.my_pawns[i].ROW_NUM, x.my_pawns[i].COL_NUM);
                Destinations.give_possible_destinations1(board, cell, possibleMoves);
                Destinations.give_possible_destinations2(board, cell, possibleMoves);
            }
            return possibleMoves;
        }

      
        private static int max_move(int depth, int alpha, int beta, Player pink_player, Player green_player, game_Board board)
        {
            bool IsPink = true;
            if (depth == 0 || is_end(pink_player, green_player))
                return evaluate(pink_player, green_player);
            List<Move> possibleMoves = get_possible_moves(pink_player, board);

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                make_move(possibleMoves[i], IsPink, pink_player, green_player, board);
                int grade = min_move(depth - 1, alpha, beta, pink_player, green_player, board);
                undo_move(possibleMoves[i], IsPink, pink_player, green_player, board);
                if (alpha > beta)
                    return beta;
                if (grade > alpha)
                    alpha = grade;
            }
            return alpha;
        }
      
        private static int min_move(int depth, int alpha, int beta, Player pink_player, Player green_player, game_Board board)
        {
            bool IsPink = false;
            if (depth == 0 || is_end(pink_player, green_player))
                return evaluate(pink_player, green_player);
            List<Move> possibleMoves = get_possible_moves(green_player, board);

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                make_move(possibleMoves[i], IsPink, pink_player, green_player, board);
                int grade = max_move(depth - 1, alpha, beta, pink_player, green_player, board);
                undo_move(possibleMoves[i], IsPink, pink_player, green_player, board);
                if (alpha > beta)
                    return alpha;
                if (grade < beta)
                    beta = grade;
            }
            return beta;
        }

       
        private static int evaluate(Player pink_player, Player green_player)
        {
            byte grade = 0;
            byte[,] pink_target = {  // Grades matrix for pink player
                             {100, 99, 98, 89, 40, 30, 20 , 0},
                             {99, 97, 96, 90, 80, 40 ,30 , 20},
                             {98, 96, 95, 91, 80 ,70 , 40, 30},
                             {89, 90, 91, 84 ,80 , 70, 60, 40},
                             {40, 80, 80 ,80 , 75, 70, 50, 50},
                             {30 ,40 , 70, 70, 70, 65, 60, 50},
                             {20 , 30, 40, 60, 60, 60, 55, 50},
                             {0 , 20, 30 ,40 , 50, 50, 50, 45}
                          };
            byte[,] green_target = {  // Grades matrix for green player
                             {45, 50, 50, 50, 40, 30, 20 , 0},
                             {50, 55, 60, 60, 60, 40 ,30 , 20},
                             {50, 60, 65, 70, 70 ,70 ,40, 30},
                             {50, 50, 70, 75 ,80, 80, 80, 40},
                             {40, 60, 70 ,80 ,84, 91, 90, 89},
                             {30, 40, 70, 80, 91, 95, 96, 98},
                             {20, 30, 40, 80, 90, 96, 97, 99},
                             {0 , 20, 30 ,40 ,89, 98, 99, 100}
                          };

            for (int i = 0; i < 9; i++)
                grade += pink_target[pink_player.my_pawns[i].ROW_NUM, pink_player.my_pawns[i].COL_NUM];

            for (int i = 0; i < 9; i++)
                grade -= green_target[green_player.my_pawns[i].ROW_NUM, green_player.my_pawns[i].COL_NUM];

            return grade;
        }
      
        private static void undo_move(Move move, bool IsPink, Player pink_player, Player green_player, game_Board board)
        {
            board.Get_I_J_cell(move.SOURCE.ROW_NUM, move.SOURCE.COL_NUM).SUG = move.HOWS;
            board.Get_I_J_cell(move.DEST.ROW_NUM, move.DEST.COL_NUM).SUG = CellType.EMPTY;
            Player player = IsPink ? pink_player : green_player;
            player.my_pawns.Remove(move.DEST);
            player.my_pawns.Add(move.SOURCE);
        }
        private static void make_move(Move move, bool IsPink, Player pink_player, Player green_player, game_Board board)
        {
            board.Get_I_J_cell(move.DEST.ROW_NUM, move.DEST.COL_NUM).SUG = move.HOWS;
            board.Get_I_J_cell(move.SOURCE.ROW_NUM, move.SOURCE.COL_NUM).SUG = CellType.EMPTY;
            Player player = IsPink ? pink_player : green_player;
            player.my_pawns.Remove(move.SOURCE);
            player.my_pawns.Add(move.DEST);
        }

        private static bool is_end(Player pink_player, Player green_player)
        {
            if (green_player.IsWin() || pink_player.IsWin())
                return true;
            else
                return false;
        }

    }
}
