using Archizoo.Tile;
using Microsoft.Xna.Framework;

namespace Archizoo
{
    public class GridTile
    {
        private readonly Map _map;
        private readonly Point _point;

        private TileBase _tile;
        public TileBase Tile
        {
            get => _tile;
            set
            {
                _tile = value;
                SetCell();
            }
        }

        public GridTile(Map map, Point point)
        {
            _map = map;
            _point = point;
        }

        private void SetCell()
        {
            _map.SetCellAppearance(_point.X, _point.Y, Tile.GenerateCell());
        }
    }
}