using Microsoft.Xna.Framework.Input;
using SadConsole.Components;
using Console = SadConsole.Console;
using Keyboard = SadConsole.Input.Keyboard;

namespace Archizoo.UI.Components
{
    public class KeyboardZoomHandler : KeyboardConsoleComponent
    {
        private readonly Map _map;

        public KeyboardZoomHandler(Map map)
        {
            _map = map;
        }
        
        public override void ProcessKeyboard(Console console, Keyboard info, out bool handled)
        {
            handled = true;
            if (info.IsKeyPressed(Keys.OemMinus))
                _map.ZoomOut();
            else if (info.IsKeyPressed(Keys.OemPlus) && info.IsKeyDown(Keys.LeftShift))
                _map.ResetZoom();
            else if (info.IsKeyPressed(Keys.OemPlus))
                _map.ZoomIn();
            else handled = false;
        }

        
    }
}