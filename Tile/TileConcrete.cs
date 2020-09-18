using Microsoft.Xna.Framework;
using SadConsole;

namespace Archizoo.Tile
{
    public class TileConcrete : TileBase
    {

        private static readonly Cell DefaultCell = new Cell(Color.White, Color.Gray);
        public TileConcrete()
        {
            IsBlocking = true;
        }


        public override Cell GenerateCell()
        {
            return DefaultCell;
        }
    }
}