using Microsoft.Xna.Framework;
using SadConsole;

namespace Archizoo.Tile
{
    public class TileConcrete : TileBase
    {

        private static readonly Cell DefaultCell = new Cell(Color.White, Color.Gray);
        public TileConcrete(Cell cell): base(cell)
        {
            IsBlocking = true;
            Cell.CopyAppearanceFrom(DefaultCell);
        }
    }
}