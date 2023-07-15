using System.Xml.Linq;

namespace AI
{
    internal class Turn
    {
        private CellType turn;
        public Turn(CellType type)
        {
            this.turn = type;
        }
        public CellType Current   
        {
            get { return turn; }     
        }
        public void SwitchTurn()
        {
            this.turn = (this.turn == CellType.GREEN) ? CellType.PINK : CellType.GREEN;
        }
    }
}
