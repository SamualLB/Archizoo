using System;
using System.Collections.Generic;
using System.Linq;
using Archizoo.Tile;
using Microsoft.Xna.Framework;

namespace Archizoo.Commands
{
    public class PlaceTileCommand : Command
    {
        private readonly Point _startPoint, _endPoint;
        private readonly TileBase _tile;
        private GridTile[] _tiles;
        
        private TileBase[] _oldTiles;

        public PlaceTileCommand(CommandManager manager, Point startPoint, Point endPoint, TileBase tile) : base(manager)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _tile = tile;
        }

        public override bool Execute()
        {
            // Find the length of each potential line
            var xLength = Math.Abs(_startPoint.X - _endPoint.X);
            var yLength = Math.Abs(_startPoint.Y - _endPoint.Y);
            
            // Use the longest line or horizontal
            if (xLength < yLength) // Vertical
                _tiles = TilesAlongLine(_startPoint.X, _startPoint.Y, _startPoint.X, _endPoint.Y).ToArray();
            else // Horizontal
                _tiles = TilesAlongLine(_startPoint.X, _startPoint.Y, _endPoint.X, _startPoint.Y).ToArray();
            
            _oldTiles = new TileBase[_tiles.Length];
            var changed = false;
            
            for (var i = 0; i < _tiles.Length; i++)
            {
                _oldTiles[i] = _tiles[i].Tile;
                _tiles[i].Tile = _tile;
                if (_oldTiles[i].GetType() != _tile.GetType())
                    changed = true;
            }
            
            if (changed) Manager.Map.IsDirty = true;
            return changed;
        }

        public override bool Undo()
        {
            var changed = false;

            for (var i = 0; i < _tiles.Length; i++)
            {
                _tiles[i].Tile = _oldTiles[i];
                if (_oldTiles[i].GetType() != _tiles[i].Tile.GetType())
                    changed = true;
            }

            if (changed) Manager.Map.IsDirty = true;
            return changed;
        }
        
        private IEnumerable<GridTile> TilesAlongLine (int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            var dx = Math.Abs(xDestination - xOrigin);
            var dy = Math.Abs(yDestination - yOrigin);

            var sx = xOrigin < xDestination ? 1 : -1;
            var sy = yOrigin < yDestination ? 1 : -1;

            var err = dx - dy;

            while (true)
            {
                yield return Manager.Map[new Point(xOrigin, yOrigin)];
                if (xOrigin == xDestination && yOrigin == yDestination)
                    break;
                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    xOrigin += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    yOrigin += sy;
                }
            }
        }
    }
}