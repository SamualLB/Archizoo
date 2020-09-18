namespace Archizoo.Commands
{
    public abstract class Command
    {
        protected readonly CommandManager Manager;

        protected Command(CommandManager manager)
        {
            Manager = manager;
        }
        
        public abstract bool Execute();

        public abstract bool Undo();
    }
}