using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI
{
    public partial class game_Board : UserControl
    {
        public event EventHandler OnTurnChanged;
        public event EventHandler<whoWon> OnWin;
        Player pink_player = new Player(CellType.PINK);
        Player green_player = new Player(CellType.GREEN);
        Turn turn = new Turn(CellType.GREEN);
        Move m = new Move();
        public bool? against_the_computer;
        bool firstClick = true;
        Cell[,] board = new Cell[number_of_rows, number_of_cols];
        const int number_of_rows = 8;
        const int number_of_cols = 8;
    
        public game_Board()
        {
            for (int i = 0; i < number_of_rows; i++)
            {
                for (int j = 0; j < number_of_cols; j++)
                {
                    board[i, j] = new Cell(i, j);
                    board[i, j].On_Click += One_of_cells_was_Clicked;
                    this.Controls.Add(board[i, j]);
                }
            }
            InitializeComponent();
        }

        public void do_the_move()
        {
            board[m.DEST.ROW_NUM, m.DEST.COL_NUM].SUG = m.HOWS;
            board[m.SOURCE.ROW_NUM, m.SOURCE.COL_NUM].SUG = CellType.EMPTY;
            board[m.SOURCE.ROW_NUM, m.SOURCE.COL_NUM].Image = AI.Resource1.emptyP;
            if (m.HOWS == CellType.GREEN)
            {
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].Image = AI.Resource1.greenP;
            }
            else
            {
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].Image = AI.Resource1.pinkP;
            }
        }
        private bool IsLeagalPress_2(Spot s)
        {
            if (this.board[s.ROW_NUM, s.COL_NUM].SUG == CellType.YELLOW)
            {
                m.DEST = s;
                m.HOWS = turn.Current;
                return true;
            }
            else
                return false;
        }

        private bool IsLeagalPress_1(Spot s)
        {
            if (this.board[s.ROW_NUM, s.COL_NUM].SUG != CellType.EMPTY &&
                this.board[s.ROW_NUM, s.COL_NUM].SUG == turn.Current)
            {
                m.SOURCE = s;
                return true;
            }
            else
                return false;
        }
        public void One_of_cells_was_Clicked(object sender, System.EventArgs e)
        {
            Cell cell = (Cell)sender;
            Spot s = new Spot(cell);
            if (firstClick)
            {
                firstClick = false;
                if (!IsLeagalPress_1(s))
                {
                    //illegal press
                }
                else
                {
                    List<Move> l_moves = new List<Move>();
                    Destinations.give_possible_destinations1(this, cell, l_moves);
                    Destinations.give_possible_destinations2(this, cell, l_moves);
                    show_the_possible_dest_on_screen(l_moves);
                    this.Refresh();

                }
            }
            else
            {
                firstClick = true;
                if (!IsLeagalPress_2(s))
                {
                    // illegal press
                }
                else
                {
                    delete_the_yellow();
                    do_the_move();
                    turn.SwitchTurn();
                    this.Refresh();

                    if (win())
                        return;
                    
                    if (this.against_the_computer == true)
                    {
                        m = AlphaBeta.generate_step_alpha_beta(pink_player, green_player, this);
                        do_the_move();
                        turn.SwitchTurn();
                        this.Refresh();
                        if (win())
                            return;
                    }

                }
                OnTurnChanged?.Invoke(this, EventArgs.Empty);
            }
            
        }
        private bool win()
        {
            if(turn.Current == CellType.GREEN)
            {
                if (green_player.IsWin())
                {
                    OnWin?.Invoke(this, new whoWon(CellType.GREEN));
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (pink_player.IsWin())
                {
                    OnWin?.Invoke(this, new whoWon(CellType.PINK));
                    return true;
                }
                else
                    return false;
            }

        }
        public void delete_the_yellow()
        {
            foreach (Cell c in board)
            {
                if (c.SUG == CellType.YELLOW)
                {
                    c.SUG = CellType.EMPTY;
                    c.Image = AI.Resource1.emptyP;
                }
            }
        }
        public void show_the_possible_dest_on_screen(List<Move> l)
        {
            foreach (Move m in l)
            {
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].SUG = CellType.YELLOW;
                board[m.DEST.ROW_NUM, m.DEST.COL_NUM].Image = AI.Resource1.yellowP;
            }
        }

        public Cell Get_I_J_cell(int i, int j)
        {
            return board[i, j];
        }

    }
}
