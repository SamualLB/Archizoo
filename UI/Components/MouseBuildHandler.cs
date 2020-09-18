using System;
using Microsoft.Xna.Framework;
using SadConsole.Components;
using SadConsole.Input;
using Console = SadConsole.Console;

namespace Archizoo.UI.Components
{
    class MouseBuildHandler : MouseConsoleComponent
    {
        private bool _isDragging;
        private MouseConsoleState _previousMouseInfo;
        private Point _cellAtDragPosition;

        public override void ProcessMouse(Console console, MouseConsoleState state, out bool handled)
        {
            handled = false;
            if (state.Mouse.LeftClicked)
            {
                handled = Game.Instance.CurrentTool switch
                {
                    "animal" => Game.Instance.CommandManager.PlaceAnimal(state.CellPosition),
                    "wall" => false,
                    "concrete" => false,
                    null => false,
                    _ => throw new NotImplementedException(
                        $"Tool {Game.Instance.CurrentTool} not implemented in {GetType()} click")
                };
            }
            
            if (_isDragging && !state.Mouse.LeftButtonDown)
            {
                _isDragging = false;
                handled = Game.Instance.CurrentTool switch
                {
                    "wall" => Game.Instance.CommandManager.PlaceWall(_cellAtDragPosition, state.CellPosition),
                    "concrete" => Game.Instance.CommandManager.PlaceConcrete(_cellAtDragPosition, state.CellPosition),
                    "animal" => false,
                    null => false,
                    _ => throw new NotImplementedException(
                        $"Tool {Game.Instance.CurrentTool} not implemented in {GetType()} dragging")
                };
            }
            
            if (!_isDragging && state.Mouse.LeftButtonDown && !_previousMouseInfo.Mouse.LeftButtonDown)
            {
                _isDragging = true;
                _cellAtDragPosition = state.ConsoleCellPosition;
            }

            _previousMouseInfo = state;
        }
    }
}
