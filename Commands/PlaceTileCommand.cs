using System;
using System.Collections.Generic;
using System.Linq;
using Archizoo.Tile;
using Microsoft.Xna.Framework;
using SadConsole;

namespace Archizoo.Commands
{
    public class PlaceTileCommand<T> : Command where T : TileBase, new()
    {
        private readonly Point _startPoint, _endPoint;
        private TileBase[] _oldTiles;
        private Point[] _points;

        public PlaceTileCommand(CommandManager manager, Point startPoint, Point endPoint) : base(manager)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public override bool Execute()
        {
            // Find the length of each potential line
            var xLength = Math.Abs(_startPoint.X - _endPoint.X);
            var yLength = Math.Abs(_startPoint.Y - _endPoint.Y);
            
            // Use the longest line or horizontal
            if (xLength < yLength) // Vertical
            {
                _oldTiles = new TileBase[yLength + 1];
                _points = PointsAlongLine(_startPoint.X, _startPoint.Y, _startPoint.X, _endPoint.Y).ToArray();
            }
            else // Horizontal
            {
                _oldTiles = new TileBase[xLength + 1];
                _points = PointsAlongLine(_startPoint.X, _startPoint.Y, _endPoint.X, _startPoint.Y).ToArray();
            }
            
            var changed = false;
            
            // Clone and set each cell
            /*for (var i = 0; i < _indexes.Length; i++)
            {
                _oldTiles[i] = Manager.Map[_indexes[i]];
                if (!ReferenceEquals(Manager.Map[_indexes[i]], _tile))
                {
                    Manager.Map.Cells[_indexes[i]] = _tile.Clone();
                    changed = true;
                }
            }*/

            for (var i = 0; i < _points.Length; i++)
            {
                _oldTiles[i] = Manager.Map[_points[i]];
                Manager.Map[_points[i]] = new TileWall(Manager.Map.Cells[Helpers.GetIndexFromPoint(_points[i].X, _points[i].Y, Manager.Map.Width)]);
                changed = true;
            }
            
            /*if (changed)*/ Manager.Map.IsDirty = true;
            return changed;
        }

        public override bool Undo()
        {
            var changed = false;
            
            var i = 0;
            /*foreach (var cell in _oldCells)
            {
                if (!ReferenceEquals(cell, Manager.Map[_indexes[i]]))
                {
                    changed = true;
                    Manager.Map.Cells[_indexes[i]] = cell;
                }
                i++;
            }*/

            Manager.Map.SetFont();
            if (changed) Manager.Map.IsDirty = true;
            return changed;
        }
        
        private IEnumerable<Point> PointsAlongLine (int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            var dx = Math.Abs(xDestination - xOrigin);
            var dy = Math.Abs(yDestination - yOrigin);

            var sx = xOrigin < xDestination ? 1 : -1;
            var sy = yOrigin < yDestination ? 1 : -1;

            var err = dx - dy;

            while (true)
            {
                yield return new Point(xOrigin, yOrigin);
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