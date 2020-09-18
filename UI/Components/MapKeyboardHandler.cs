using Archizoo.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole.Components;
using Console = SadConsole.Console;
using Keyboard = SadConsole.Input.Keyboard;

namespace Archizoo.UI.Components
{
    public class MapKeyboardHandler : KeyboardConsoleComponent
    {
        public override void ProcessKeyboard(Console console, Keyboard info, out bool handled)
        {
            handled = false;
            if (info.IsKeyPressed(Keys.U))
                if (Game.Instance.CommandManager.Undo())
                    handled = true;
            if (info.IsKeyPressed(Keys.R))
                if (Game.Instance.CommandManager.Redo())
                    handled = true;
            if (info.IsKeyPressed(Keys.A))
            {
                var entity = new Lion { Font = console.Font };
                Game.Instance.Map.Children.Add(entity);
                entity.Position = new Point(1, 1);
                entity.Sleep();
                entity.Ambient();
                entity.Die();
            }
        }
    }
}