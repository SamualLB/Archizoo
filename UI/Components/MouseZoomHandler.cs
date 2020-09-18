using SadConsole;
using SadConsole.Components;
using SadConsole.Input;

namespace Archizoo.UI.Components
{
    public class MouseZoomHandler : MouseConsoleComponent
    {
        private int _accumulated;
        private const int ChangeRequired = 120;
        
        private readonly Map _map;

        public MouseZoomHandler(Map map)
        {
            _map = map;
        }
        
        public override void ProcessMouse(Console console, MouseConsoleState state, out bool handled)
        {
            handled = false;

            _accumulated += state.Mouse.ScrollWheelValueChange;

            if (_accumulated >= ChangeRequired)
            {
                _map.ZoomOut();
                _accumulated -= ChangeRequired;
            }
            else if (_accumulated <= -ChangeRequired)
            {
                _map.ZoomIn();
                _accumulated += ChangeRequired;
            }
        }
    }
}