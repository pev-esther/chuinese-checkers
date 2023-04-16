using System;
using System.Windows.Forms;
using System.Drawing;

namespace Chinese_Checkers_
{
    public enum Typecell {EMPTY, GREEN, PINK, YELLOW};
    public class Cell : PictureBox
    {
        int row, col;
        Typecell sug;
        public const int CellSize = 77;

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.Size = new Size(CellSize, CellSize);
            this.Location = new Point(col * CellSize + 3, row * CellSize + 3);
            this.Click += new EventHandler(Cell_Click);

            if (row < 3 && col < 3)
            {
                this.sug = Typecell.GREEN;
                this.Image = Chinese_Checkers_.Resource1.green;
            }
            else
            {
                if (row > 4 && col > 4)
                {
                    this.sug = Typecell.PINK;
                    this.Image = Chinese_Checkers_.Resource1.pink;
                }
                else
                {
                    this.sug = Typecell.EMPTY;
                    this.Image = Chinese_Checkers_.Resource1.empty;
                }
            }
        }

        public event EventHandler codeYellow;

        void Cell_Click(object sender, EventArgs e)
        {
            Cell clicked_cell = (Cell)sender;
            codeYellow?.Invoke(this, EventArgs.Empty);
        }
       
        /// <summary>
        /// SET and GET functions
        /// </summary>
        #region Properties
        public int ROW
        {
            get { return this.row; }
            set { this.row = value; }
        }
        public int COL
        {
            get { return this.col; }
            set { this.col = value; }
        }
        public Typecell SUG
        {
            get { return this.sug; }
            set { this.sug = value; }
        }
        #endregion
    }
}
