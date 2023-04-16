

namespace Chinese_Checkers_
{
    public class Move  
    {
        Typecell hows_move_is_it;
        Spot Source, Dest;
        public Move()
        {

        }
        public Move(Move m1)
        {
            this.hows_move_is_it = m1.hows_move_is_it;
            this.SOURCE = m1.SOURCE;
            this.Dest = m1.DEST;
        }
        public Move(Cell Source, Cell Dest)
        {
            this.Source = new Spot(Source.ROW, Source.COL);
            this.Dest = new Spot(Dest.ROW, Dest.COL);
            this.hows_move_is_it = Source.SUG;
        }

        #region Properties
        public Spot SOURCE
        {
            get { return this.Source; }
            set { this.Source = value; }
        }
        public Spot DEST
        {
            get { return this.Dest; }
            set { this.Dest = value; }
        }
      
        public Typecell HOWS
        {
            get { return this.hows_move_is_it; }
            set { this.hows_move_is_it = value; }
        }
        #endregion

    }
}
