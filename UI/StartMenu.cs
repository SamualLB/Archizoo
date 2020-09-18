using System;
using SadConsole;
using SadConsole.Controls;
using Microsoft.Xna.Framework;

namespace Archizoo.UI
{
    class MainMenu : ControlsConsole
    {
        private readonly Button _startButton;

        public MainMenu(int width, int height): base(width, height)
        {
            IsVisible = true;
            _startButton = new Button(12) { Text = "Start Game" };
            _startButton.Click += MainMenu_Click;
            Add(_startButton);
            Reposition();
        }

        private void Reposition()
        {
            _startButton.Position = new Point(Width / 2 - _startButton.Width / 2, Height / 2);
        }

        public new void Resize(int width, int height, bool clear)
        {
            base.Resize(width, height, clear);
            ViewPort = new Rectangle(0, 0, Width, Height);
            Reposition();
            IsDirty = true;
        }

        private static void MainMenu_Click(object sender, EventArgs e)
        {
            Game.Instance.StartGame();
        }
    }
}