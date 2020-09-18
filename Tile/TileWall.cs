using Microsoft.Xna.Framework;
using SadConsole;

namespace Archizoo.Tile
{
    public class TileWall : TileBase
    {
        private static readonly Cell DefaultCell = new Cell(Color.White, Color.DarkGray, 206);

        public TileWall(Cell cell): base(cell)
        {
            IsBlocking = true;
            Cell.CopyAppearanceFrom(DefaultCell);
        }
    }
}