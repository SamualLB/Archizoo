using System;
using Microsoft.Xna.Framework;
using SadConsole;

namespace Archizoo.Tile
{
    public class TileGrass : TileBase
    {
        private static readonly Random Random = new Random();

        private static readonly Cell DefaultNormalCell = new Cell(Color.White, Color.ForestGreen);
        private static readonly Cell DefaultDarkCell = new Cell(Color.White, Color.Green);

        public TileGrass(Cell cell) : base(cell)
        {
            Cell.CopyAppearanceFrom(Random.NextDouble() < 0.5 ? DefaultDarkCell : DefaultNormalCell);
        }
    }
}