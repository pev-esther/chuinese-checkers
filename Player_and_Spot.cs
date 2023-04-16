using System.Collections.Generic;

namespace Chinese_Checkers_
{
    public class Spot
    {
        int row_number, col_number;
        public Spot(int r, int c)
        {
            this.row_number = r;
            this.col_number = c;
        }

        public Spot(Spot s1)
        {
            this.row_number = s1.row_number;
            this.col_number = s1.col_number;
        } 
        public Spot(Cell c)
        {
            this.row_number = c.ROW;
            this.col_number = c.COL;
        }
        public int ROW_NUM
        {
            get { return this.row_number; }
            set { this.row_number = value; }
        }
        public int COL_NUM
        {
            get { return this.col_number; }
            set { this.col_number = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Spot objAsPart = obj as Spot;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public bool Equals(Spot s)
        {
            if (this.ROW_NUM == s.ROW_NUM && this.COL_NUM == s.COL_NUM) return true;
            return false;
        }
    }

    public class Player
    {
        public Typecell howAmI;
        public List<Spot> my_pawns = new List<Spot>();

        public Player(Typecell howAmI)
        {
            this.howAmI = howAmI;
            switch (howAmI)
            {
                case Typecell.PINK:

                    my_pawns.Add(new Spot(5, 5));
                    my_pawns.Add(new Spot(5, 6));
                    my_pawns.Add(new Spot(5, 7));
                    my_pawns.Add(new Spot(6, 5));
                    my_pawns.Add(new Spot(6, 6));
                    my_pawns.Add(new Spot(6, 7));
                    my_pawns.Add(new Spot(7, 5));
                    my_pawns.Add(new Spot(7, 6));
                    my_pawns.Add(new Spot(7, 7));
                    break;
                case Typecell.GREEN:
                    my_pawns.Add(new Spot(0, 0));
                    my_pawns.Add(new Spot(0, 1));
                    my_pawns.Add(new Spot(0, 2));
                    my_pawns.Add(new Spot(1, 0));
                    my_pawns.Add(new Spot(1, 1));
                    my_pawns.Add(new Spot(1, 2));
                    my_pawns.Add(new Spot(2, 0));
                    my_pawns.Add(new Spot(2, 1));
                    my_pawns.Add(new Spot(2, 2));
                    break;
            }
        }

        public bool IsWin()
        {
            int mone = 0;
            if (howAmI == Typecell.PINK)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (this.my_pawns[i].ROW_NUM < 3 && this.my_pawns[i].COL_NUM < 3)
                        mone++;
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (this.my_pawns[i].ROW_NUM >= 5 && this.my_pawns[i].COL_NUM >= 5)
                        mone++;
                }
            }
            if (mone == 9)
                return true;
            else
                return false;
        }
    }
}
