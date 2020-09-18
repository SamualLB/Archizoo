using System.Collections.Generic;
using Archizoo.Entities;
using Microsoft.Xna.Framework;
using SadConsole;
using Archizoo.Tile;

namespace Archizoo.Commands
{
    public class CommandManager
    {
        public readonly Map Map;

        private readonly Stack<Command> _history = new Stack<Command>();
        private readonly Stack<Command> _redos = new Stack<Command>();
        
        public CommandManager(Map map)
        {
            Map = map;
        }

        public bool Undo()
        {
            if (_history.Count <= 0)
                return false;
            var cmd = _history.Pop();
            var result = cmd.Undo();
            if (result) _redos.Push(cmd);
            return result;
        }

        public bool Redo()
        {
            if (_redos.Count <= 0)
                return false;
            return InnerExecute(_redos.Pop());
        }

        private bool InnerExecute(Command cmd)
        {
            var result = cmd.Execute();
            if (result) _history.Push(cmd);
            return result;
        }

        public bool PlaceAnimal(Point position)
        {
            return InnerExecute(new PlaceAnimalCommand(this, position, new Lion()));
        }
        
        public bool PlaceWall(Point start, Point end)
        {
            return InnerExecute(new PlaceTileCommand<TileWall>(this, start, end));
        }

        public bool PlaceConcrete(Point start, Point end)
        {
            return InnerExecute(new PlaceTileCommand<TileConcrete>(this, start, end));
        }
    }
}