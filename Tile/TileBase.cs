using SadConsole;

namespace Archizoo.Tile
{
    public abstract class TileBase
    {
        public bool IsBlocking;

        public Cell Cell;

        
        public TileBase(Cell cell)
        {
            Cell = cell;
        }
    }
}