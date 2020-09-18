using System;
using SadConsole;
using SadConsole.Controls;
using Microsoft.Xna.Framework;

namespace Archizoo.UI
{
    public class Toolbar : Window
    {
        private const int DefaultWidth = 16;
        private static readonly int DefaultHeight = 20;

        private static readonly string[] ToolNames = {"wall", "concrete", "animal"};

        public Toolbar(): base(DefaultWidth, DefaultHeight)
        {
            Title = "Build";
            Position = new Point(1, 1);
            CanDrag = true;
            TitleAlignment = HorizontalAlignment.Center;

            var offset = 2;
            foreach (var label in ToolNames)
            {
                var button = new RadioButton(Width - 2, 1) { Position = new Point(1, offset) };
                offset++;
                button.GroupName = "ToolSelectGroup";
                button.Text = label;
                button.IsSelectedChanged += Toolbar_IsSelectedChanged;
                Add(button);
            }

            IsVisible = true;
        }

        private static void Toolbar_IsSelectedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton) sender;
            if (!button.IsSelected)
                return;
            Game.Instance.CurrentTool = button.Text;
        }
    }
}
