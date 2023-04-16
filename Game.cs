using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chinese_Checkers_
{  
    public partial class Game : UserControl
    {
        const int number_of_rows = 8;
        const int number_of_cols = 8;
        
        private Cell[,] board = new Cell[number_of_rows, number_of_cols];
        public Typecell turn = Typecell.GREEN;
        Move m = new Move();
        Player player_Green = new Player(Typecell.GREEN);
        Player player_Pink = new Player(Typecell.PINK);
        
        bool solo_game;
        int depth = 2;
   
        public Game(bool alfh_beta)
        {
            InitializeComponent();
            this.solo_game = alfh_beta;
            for (int i = 0; i < number_of_rows; i++)
            {
                for (int j = 0; j < number_of_cols; j++)
                {
                    board[i, j] = new Cell(i, j);
                    board[i, j].codeYellow += Board_codeYellow;
                    this.Controls.Add(board[i, j]); 
                }
            }
        }
        /// <summary>
        /// here we will find all possiable moves to the cell "sender"
        /// </summary>
        private void Board_codeYellow(object sender, System.EventArgs e)
        {
            Cell cell = (Cell)sender;
            List<Move> l1 = new List<Move>();
            this.m.HOWS = turn;
            if (you_are_not_empty_or_yellow(cell))
            {
                if (is_it_your_turn_to_play(cell))
                {
                    this.m.SOURCE = new Spot(cell);
                    give_possible_destanations1(cell, l1);
                    give_possible_destanations2(cell, l1);
                    show_the_possible_dest_on_screen_aka_yellow_fish(l1);
                    this.Refresh();
                }
                else
                {
                    if (cell.SUG == Typecell.YELLOW)
                    {
                        this.m.DEST = new Spot(cell);
                        delete_the_yellow();
                        do_the_move();                       
                        this.Refresh();
                        take_care_on_win();
                        chang_turn();
                        if (this.solo_game)
                        {
                            Do_step_alpha_beta();
                            this.Refresh();
                            take_care_on_win();
                            chang_turn();
                        }
                    }
                }
            }
        }
      
        private void do_the_move()
        {
            board[m.DEST.ROW_NUM, m.DEST.COL_NUM].SUG = m.HOWS;
            board[m.SOURCE.ROW_NUM, m.SOURCE.COL_NUM].SUG = Typecell.EMPTY;
            board[m.SOURCE.ROW_NUM, m.SOURCE.COL_NUM].Image = Chinese_Checkers_.Resource1.empty;
            if (m.HOWS == Typecell.GREEN)
            {
                player_Green.my_pawns.Remove(m.SOURCE);
                player_Green.my_pawns.Add(m.DEST);
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].Image = Chinese_Checkers_.Resource1.green;
            }
            else
            {
                player_Pink.my_pawns.Remove(m.SOURCE);
                player_Pink.my_pawns.Add(m.DEST);
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].Image = Chinese_Checkers_.Resource1.pink;
            }
        }

        private bool IsLegal(int row, int col) => row >= 0 && row <= 7 && col >= 0 && col <= 7;
        

        private int[,] Dir = {
                         { -2,  0 }, // Up
                         {  0, -2 }, // Left
                         {  2,  0 }, // Down
                         {  0,  2 }   // Right
                            };
        private int[,] Dir2 = {
                         { -1,  0 }, // Up
                         {  0, -1 }, // Left
                         {  1,  0 }, // Down
                         {  0,  1 }   // Right
                             };


        private void give_possible_destanations1(Cell src, List<Move> possible_moves)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                int row = src.ROW + Dir2[dir, 0];
                int col = src.COL + Dir2[dir, 1];
                if (IsLegal(row, col))
                {
                    if (board[row, col].SUG == Typecell.EMPTY)
                    {
                        Move a_move = new Move(src, board[row, col]);
                        possible_moves.Add(a_move);
                    }
                }
            }
        }
        private void give_possible_destanations2_Aux(Cell src, Cell RealSrc, int i, int j, List<Move> possible_moves, int wayN)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                if (dir == wayN)
                    continue;
                int row = i + Dir[dir, 0];
                int col = j + Dir[dir, 1];
                if (IsLegal(row, col))
                {
                    if (board[row, col].SUG == Typecell.EMPTY &&
                        board[i + Dir2[dir, 0], j + Dir2[dir, 1]].SUG != Typecell.EMPTY)
                    {
                        Move a_move = new Move(RealSrc, board[row, col]);
                        possible_moves.Add(a_move);
                        give_possible_destanations2_Aux(board[row, col], RealSrc, row, col, possible_moves, (dir + 2) % 4);
                    }
                }
            }      
        }
        private void give_possible_destanations2(Cell src, List<Move> possible_moves)
        {
            give_possible_destanations2_Aux(src, src, src.ROW, src.COL, possible_moves, -1);        
        }

        /// <summary>
        /// This function is responsible for organizing all the possible moves in the list: possibleMoves
        /// </summary>
        /// <param name="x"> Board.Computer /OR/ Board.person </param>
        /// <returns></returns>
        private List<Move> get_possible_moves(Player x)
        {
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < x.my_pawns.Count; i++)
            {
                Cell cell = board[x.my_pawns[i].ROW_NUM, x.my_pawns[i].COL_NUM];
                give_possible_destanations1(cell, possibleMoves);
                give_possible_destanations2(cell, possibleMoves);
            }
            return possibleMoves;
        }


        /// <summary>
        /// strategic thinking of the computer by using AlphaBeta Pruming Alg
        /// </summary>
        private void Do_step_alpha_beta()
        {
            int beta = int.MaxValue;
            int best = int.MinValue;
            int grade;
            List<Move> possibleMoves = get_possible_moves(this.player_Pink);
            Move BestMove = possibleMoves[0];
            for (int i = 0; i < possibleMoves.Count; i++)
            {
                make_move(possibleMoves[i], true);
                grade = min_move(depth, best, beta) ;
                if (grade > best)
                {
                    best = grade;
                    BestMove = possibleMoves[i];
                }
                undo_move(possibleMoves[i], true);
            }
            m = BestMove;
            do_the_move();
        }
       

        /// <summary>
        /// Both these actions are responsible for checking if there was a victory
        /// </summary>
        private void take_care_on_win()
        {
            if (player_Green.IsWin())
            {
                MessageBox.Show("PLAYR GREEN YOU WON!!");
            }
            if(player_Pink.IsWin())
            {
                MessageBox.Show("PLAYR PINK YOU WON!!");
            }
        }   

        /// <summary>
        ///  Implements game move evaluation from the point of view of the MAX player.
        ///  The board that will be used as a starting point for generating the game movements.
        /// </summary>
        /// <param name="depth">Current depth in the Min-Max tree</param>
        /// <param name="alpha">Current alpha value for the alpha-beta cutoff.</param>
        /// <param name="beta">Current beta value for the alpha-beta cutoff.</param>
        /// <returns>max grade</returns>
        private int max_move(int depth, int alpha, int beta)
        {
            if (depth == 0 || is_end())
                return evaluate();
            List<Move> possibleMoves = get_possible_moves(this.player_Pink);

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                make_move(possibleMoves[i], true);
                int grade = min_move(depth - 1, alpha, beta);
                undo_move(possibleMoves[i], true);
                if (alpha > beta)
                    return beta;
                if (grade > alpha)
                    alpha = grade;
            }
            return alpha;
        }      
        
        /// <summary>
        ///  Implements game move evaluation from the point of view of the MIN player.
        ///  The board that will be used as a starting point for generating the game movements.
        /// </summary>
        /// <param name="depth">Current depth in the Min-Max tree</param>
        /// <param name="alpha">Current alpha value for the alpha-beta cutoff.</param>
        /// <param name="beta">Current beta value for the alpha-beta cutoff.</param>
        /// <returns>min grade</returns>
        private int min_move(int depth, int alpha, int beta)
        {
            if (depth == 0 || is_end())
                return evaluate();
            List<Move> possibleMoves = get_possible_moves(this.player_Green);

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                make_move(possibleMoves[i], false);
                int grade = max_move(depth - 1, alpha, beta);
                undo_move(possibleMoves[i], false);
                if (alpha > beta)
                    return alpha;
                if (grade < beta)
                    beta = grade;
            }
            return beta;
        }

        /// <summary>
        /// This function returns the board score 
        /// </summary>
        /// <returns></returns>
        private int evaluate()
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
                grade += pink_target[this.player_Pink.my_pawns[i].ROW_NUM, this.player_Pink.my_pawns[i].COL_NUM];
                      
            for (int i = 0; i < 9; i++)
                grade -= green_target[this.player_Green.my_pawns[i].ROW_NUM, this.player_Green.my_pawns[i].COL_NUM];

            return grade;
        }

        /// <summary>
        /// These two functions are responsible for making "virtual" move and to cancell it
        /// </summary>
        /// <param name="move">the move that we examine </param>
        /// <param name="Player">PINK OR GREEN </param>
        private void undo_move(Move move, bool Player)
        {
            board[move.SOURCE.ROW_NUM, move.SOURCE.COL_NUM].SUG = move.HOWS;
            board[move.DEST.ROW_NUM, move.DEST.COL_NUM].SUG = Typecell.EMPTY;
            Player player = Player ? this.player_Pink : this.player_Green;
            player.my_pawns.Remove(move.DEST);
            player.my_pawns.Add(move.SOURCE);
        }
        private void make_move(Move move, bool Player)
        {       
            board[move.DEST.ROW_NUM, move.DEST.COL_NUM].SUG = move.HOWS;      
            board[move.SOURCE.ROW_NUM, move.SOURCE.COL_NUM].SUG = Typecell.EMPTY;
            Player player = Player ? this.player_Pink : this.player_Green;
            player.my_pawns.Remove(move.SOURCE);
            player.my_pawns.Add(move.DEST);
        }

        /// <summary>
        /// This function checks if ALL the soldiers of a particular part reached the destination
        /// </summary>
        /// <returns></returns>
        private bool is_end()
        {
            if (this.player_Green.IsWin() || this.player_Pink.IsWin())
                return true;
            else
                return false;
        }
        private void show_the_possible_dest_on_screen_aka_yellow_fish(List<Move> l)
        {
            foreach (Move m in l)
            {
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].SUG = Typecell.YELLOW;
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].Image = Chinese_Checkers_.Resource1.yellow_fish;
            }
        }
        private void delete_the_yellow()
        {
            foreach (Cell c in board)
            {
                if (c.SUG == Typecell.YELLOW)
                {
                    c.SUG = Typecell.EMPTY;
                    c.Image = Chinese_Checkers_.Resource1.empty;
                }
            }
        }
        private bool you_are_not_empty_or_yellow(Cell a)
        {
            if (a.SUG != Typecell.EMPTY || a.SUG == Typecell.YELLOW)
                return true;
            else
                return false;
        }
        private bool is_it_your_turn_to_play(Cell a)
        {
            if (a.SUG == this.turn)
                return true;
            else
                return false;
        }
        private void chang_turn()
        {
            if (this.turn == Typecell.GREEN)
                this.turn = Typecell.PINK;
            else
                this.turn = Typecell.GREEN;
        }
    }
}
 
