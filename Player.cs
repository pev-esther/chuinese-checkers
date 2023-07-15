namespace AI
{
    public class Player
    {
        public CellType howAmI;
        public List<Spot> my_pawns = new List<Spot>();

        public Player(CellType howAmI)
        {
            this.howAmI = howAmI;
            switch (howAmI)
            {
                case CellType.PINK:

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
                case CellType.GREEN:
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
            if (howAmI == CellType.PINK)
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
