namespace AI
{
    public class Move
    {
        CellType hows_move_is_it;
        Spot Source, Dest;
        public Move()
        {

        }
      
        public Move(Cell Source, Cell Dest)
        {
            this.Source = new Spot(Source.ROW, Source.COL);
            this.Dest = new Spot(Dest.ROW, Dest.COL);
            this.hows_move_is_it = Source.SUG;
        }

        public Move(Cell Source, Spot Dest)
        {
            this.Source = new Spot(Source.ROW, Source.COL);
            this.Dest = new Spot(Dest.ROW_NUM, Dest.COL_NUM);
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

        public CellType HOWS
        {
            get { return this.hows_move_is_it; }
            set { this.hows_move_is_it = value; }
        }
        #endregion

    }

}
