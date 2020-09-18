using SadConsole;

namespace Archizoo.Tile
{
    public abstract class TileBase
    {
        public bool IsBlocking;

        public abstract Cell GenerateCell();
    }
}