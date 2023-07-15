namespace AI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            game_Board1.OnTurnChanged += Turn_Update;
            game_Board1.OnWin += End;
        }

        private void End(object? sender, whoWon e)
        {
            string txt = "player" + e.get_who_won() + " you won!";
            MessageBox.Show(txt);
        }

        private void Turn_Update(object? sender, EventArgs e)
        {
            string green_turn = "green turn";
            string pink_turn = "pink turn";
            if (turn_label.Text == pink_turn)
            {
                turn_label.Text = green_turn;
            }
            else
            {
                turn_label.Text = pink_turn;
            }
        }

        private void r_twoPlayers_CheckedChanged(object sender, EventArgs e)
        {
            this.game_Board1.against_the_computer = false;
        }

        private void r_againstComputer_CheckedChanged(object sender, EventArgs e)
        {
            this.game_Board1.against_the_computer = true;
        }

        private void b_NewGame_Click(object sender, EventArgs e)
        {
            if (this.game_Board1.against_the_computer != null)
            {
                MessageBox.Show("start now your game");
            }
            else
                MessageBox.Show("please choose the game type first!");
        }

        private void b_ViewInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("the instructions are....");
        }
    }
}
