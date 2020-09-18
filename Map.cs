using System;
using Archizoo.Tile;
using SadConsole;
using Microsoft.Xna.Framework;
using FontSizes = SadConsole.Font.FontSizes;

namespace Archizoo
{
    public class Map : ScrollingConsole
    {
        public static readonly FontSizes[] ZoomValues = {FontSizes.Quarter, FontSizes.Half, FontSizes.One, FontSizes.Two, FontSizes.Three, FontSizes.Four};

        public readonly GridTile[] Tiles;
        private int _currentZoom = 2;
        
        public Map(int width, int height): base(width, height)
        {
            Tiles = new GridTile[width * height];
            for (var i = 0; i < Cells.Length; i++)
                Tiles[i] = new GridTile(this, GetPointFromIndex(i));
        }

        public GridTile this[Point point] => Tiles[Helpers.GetIndexFromPoint(point.X, point.Y, Width)];

        /// <summary>
        /// Zoom out 1 step, returns immediately if cannot zoom in more
        /// </summary>
        public void ZoomIn()
        {
            if (_currentZoom >= ZoomValues.Length - 1)
                return;
            _currentZoom++;
            UpdateFont();
        }

        /// <summary>
        /// Zoom in 1 step, returns immediately if cannot zoom out more
        /// </summary>
        public void ZoomOut()
        {
            if (_currentZoom <= 0)
                return;
            _currentZoom--;
            UpdateFont();
        }

        /// <summary>
        /// Reset zoom level to original
        /// </summary>
        public void ResetZoom()
        {
            _currentZoom = 2;
            UpdateFont();
        }

        /// <summary>
        /// Update the font depending on the current value of _currentZoom
        /// </summary>
        public void UpdateFont()
        {
            Font = Font.Master.GetFont(ZoomValues[_currentZoom]);
            
            // Pass font through to children
            foreach (var child in Children)
            {
                child.Font = Font;
            }

            // Update position to update Entity positions and centre in screen
            Position = new Point(Math.Clamp(Position.X, Global.WindowWidth / Font.Size.X / 2 - Width, Global.WindowWidth / Font.Size.X / 2), Math.Clamp(Position.Y, (Global.WindowHeight / Font.Size.Y / 2) - Height, Global.WindowHeight / Font.Size.Y / 2));

            
            // Make sure to clean up entities
            IsDirty = true;
        }

        public static Map Generate(int w, int h)
        {
            var map = new Map(w, h);
            for (var i = 0; i < map.Cells.Length; i++)
                map[Helpers.GetPointFromIndex(i, map.Width)].Tile = new TileGrass();
            return map;
        }
    }
}