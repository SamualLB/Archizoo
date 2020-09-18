using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;

namespace Archizoo.UI.Components
{
    public class MouseDragHandler : MouseConsoleComponent
    {
        private bool _isDragging;
        private MouseConsoleState _previousMouseInfo;
        private Point _cellAtDragPosition;


        public override void ProcessMouse(Console console, MouseConsoleState state, out bool handled)
        {
            handled = false;
            
            if (_isDragging && state.Mouse.MiddleButtonDown)
            {
                var oldPos = console.Position;
                console.Position = state.WorldCellPosition - _cellAtDragPosition;
                if (console.Position.X > (Global.WindowWidth / 2 / console.Font.Size.X) ||
                    console.Position.Y > (Global.WindowHeight / 2 / console.Font.Size.Y) ||
                    console.Position.X + console.Width < (Global.WindowWidth / 2 / console.Font.Size.X) ||
                    console.Position.Y + console.Height < (Global.WindowHeight / 2 / console.Font.Size.Y))
                    console.Position = oldPos;
                handled = true;
            }

            if (_isDragging && !state.Mouse.MiddleButtonDown)
            {
                _isDragging = false;
                handled = true;
            }

            if (!_isDragging && state.Mouse.MiddleButtonDown && !_previousMouseInfo.Mouse.MiddleButtonDown)
            {
                _isDragging = true;
                _cellAtDragPosition = state.ConsoleCellPosition;
            }

            _previousMouseInfo = state;
        }
    }
}