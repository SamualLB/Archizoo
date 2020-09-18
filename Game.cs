using Archizoo.Commands;
using Archizoo.UI;

namespace Archizoo
{
    public class Game : SadConsole.Game
    {
        public const int DefaultWidth = 80;
        public const int DefaultHeight = 25;
        
        public new static Game Instance;

        public UiManager UiManager;
        public Map Map;
        public CommandManager CommandManager;
        public string CurrentTool;

        public static void Main()
        {
            Instance = new Game();

            Instance.Run();
            Instance.Dispose();
        }

        private Game(): base("", DefaultWidth, DefaultHeight, null)
        {
        }

        public void StartGame()
        {
            Map = Map.Generate(100, 50);
            CommandManager = new CommandManager(Map);
            UiManager.SwitchToGame();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Instance = this;
            UiManager = new UiManager();
            SadConsole.Global.CurrentScreen = UiManager;
        }
    }
}