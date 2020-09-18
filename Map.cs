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

        public readonly TileBase[] Tiles;
        private int _currentZoom = 2;
        
        public Map(int width, int height): base(width, height)
        {
            Tiles = new TileBase[width * height];
        }

        public TileBase this[Point point]
        {
            get => Tiles[Helpers.GetIndexFromPoint(point.X, point.Y, Width)];
            set
            {
                Tiles[Helpers.GetIndexFromPoint(point.X, point.Y, Width)] = value;
                value.Cell = Cells[Helpers.GetIndexFromPoint(point.X, point.Y, Width)];
            }
        }

        /// <summary>
        /// Zoom out 1 step, returns immediately if cannot zoom in more
        /// </summary>
        public void ZoomIn()
        {
            if (_currentZoom >= ZoomValues.Length - 1)
                return;
            _currentZoom++;
            SetFont();
        }

        /// <summary>
        /// Zoom in 1 step, returns immediately if cannot zoom out more
        /// </summary>
        public void ZoomOut()
        {
            if (_currentZoom <= 0)
                return;
            _currentZoom--;
            SetFont();
        }

        /// <summary>
        /// Reset zoom level to original
        /// </summary>
        public void ResetZoom()
        {
            _currentZoom = 2;
            SetFont();
        }

        /// <summary>
        /// Update the font depending on the current value of _currentZoom
        /// </summary>
        public void SetFont()
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
            var r = new Random();
            for (var i = 0; i < map.Cells.Length; i++)
                map[Helpers.GetPointFromIndex(i, map.Width)] = new TileGrass(map.Cells[i]);
            return map;
        }
    }
}