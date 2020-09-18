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
        public override Cell GenerateCell()
        {
            return Random.NextDouble() < 0.5 ? DefaultNormalCell : DefaultDarkCell;
        }
    }
}