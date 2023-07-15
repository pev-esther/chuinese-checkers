using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public enum CellType { EMPTY, GREEN, PINK, YELLOW };
    public class Cell : PictureBox
    {
        public event EventHandler On_Click;

        int row, col;
        CellType sug;
        public const int CellSize = 73;

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.Size = new Size(CellSize, CellSize);
            this.Location = new Point(col * CellSize + 3, row * CellSize + 3);
            this.Click += new EventHandler(Cell_Click);

            if (row < 3 && col < 3)
            {
                this.sug = CellType.GREEN;
                this.Image = AI.Resource1.greenP;
            }
            else
            {
                if (row > 4 && col > 4)
                {
                    this.sug = CellType.PINK;
                    this.Image = AI.Resource1.pinkP;
                }
                else
                {
                    this.sug = CellType.EMPTY;
                    this.Image = AI.Resource1.emptyP;
                }
            }
        }

        void Cell_Click(object sender, EventArgs e)
        {
            On_Click?.Invoke(this, EventArgs.Empty);
        }

        
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
        public CellType SUG
        {
            get { return this.sug; }
            set { this.sug = value; }
        }
        #endregion
    }
}
