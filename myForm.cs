using System.Windows.Forms;

namespace Chinese_Checkers_
{
    public partial class myForm : Form
    {
        Game myGameBoard;
        bool AlfphBeta;
        public myForm(bool isTowPlayers)
        {        
            if (isTowPlayers)
                AlfphBeta = false;
            else
                AlfphBeta = true;
            this.myGameBoard = new Game(AlfphBeta);
            this.myGameBoard.on_move += MyGameBoard_on_move;
            InitializeComponent();
        }

        private void MyGameBoard_on_move(object sender, System.EventArgs e)
        {
            Game g = (Game)sender;
            if (g.turn == Typecell.GREEN)
                this.torn_label.Text = "Torn: Green";
            else
            {
                this.torn_label.Text = "Torn: Pink";
                if (AlfphBeta)
                    this.label1.Text = "computer thinks...";
            }
        }

        public void set_Status(string str)
        {
            this.torn_label.Text = str;
        }

    }
}
