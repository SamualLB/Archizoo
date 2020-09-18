using Archizoo.Entities;
using Microsoft.Xna.Framework;

namespace Archizoo.Commands
{
    public class PlaceAnimalCommand : Command
    {
        private readonly Point _position;
        private readonly Entity _entity;
        
        public PlaceAnimalCommand(CommandManager manager, Point position, Entity entity) : base(manager)
        {
            _entity = entity;
            _position = position;
        }

        public override bool Execute()
        {
            _entity.Font = Manager.Map.Font;
            Game.Instance.Map.Children.Add(_entity);
            _entity.Position = _position;
            return true;
        }

        public override bool Undo()
        {
            Game.Instance.Map.Children.Remove(_entity);
            return true;
        }
    }
}