using System;
using SadConsole;

namespace Archizoo.UI
{
    public class UiManager : ContainerConsole
    {
        public SadConsole.Console CurrentScreen;

        public UiManager()
        {
            IsFocused = true;

            CurrentScreen = new MainMenu(Game.DefaultWidth, Game.DefaultHeight);
            Children.Add(CurrentScreen);
            CurrentScreen.IsFocused = true;

            Settings.ResizeMode = Settings.WindowResizeOptions.None;
            ((Game)SadConsole.Game.Instance).WindowResized += UIManager_WindowResized;
        }

        /// <summary>
        /// Start button was pressed on the menu, start the game
        /// </summary>
        public void SwitchToGame()
        {
            Children.Remove(CurrentScreen);

            var gameScreen = new GameScreen(Game.Instance.Map) { IsVisible = true };

            Children.Add(gameScreen);
            CurrentScreen = gameScreen;
        }

        private void UIManager_WindowResized(object sender, EventArgs e)
        {
            switch (CurrentScreen)
            {
                case MainMenu menu:
                    menu.Resize(Global.WindowWidth / menu.Font.Size.X, Global.WindowHeight / menu.Font.Size.Y, true);
                    break;
                case GameScreen game:
                    game.Resize(Global.WindowWidth / CurrentScreen.Font.Size.X, Global.WindowHeight / CurrentScreen.Font.Size.Y, true);
                    break;
                default:
                    throw new Exception($"Screen is not resizable ({CurrentScreen.GetType()})");
            }
        }
    }
}
