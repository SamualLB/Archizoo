using Microsoft.Xna.Framework;
using SadConsole.Components;

namespace Archizoo.Entities
{
    public abstract class Entity : SadConsole.Entities.Entity
    {
        protected Entity(Color fg, int glyph) : base(fg, Color.Transparent, glyph)
        {
            Components.Add(new EntityViewSyncComponent());
        }
    }
}