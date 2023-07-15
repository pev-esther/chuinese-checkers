namespace AI
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

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
