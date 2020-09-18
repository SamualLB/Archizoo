using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Archizoo.UI.Components;

namespace Archizoo.UI
{
    public class GameScreen : ContainerConsole
    {
        public readonly ScrollingConsole MapScreen;

        public GameScreen(Map map)
        {
            MapScreen = map;
            MapScreen.Position = new Point((Global.WindowWidth / MapScreen.Font.Size.X - MapScreen.Width) / 2, (Global.WindowHeight / MapScreen.Font.Size.Y - MapScreen.Height) / 2);
            MapScreen.IsVisible = true;
            MapScreen.ViewPort = new Rectangle(MapScreen.ViewPort.X, MapScreen.ViewPort.Y, MapScreen.Width, MapScreen.Height);
            MapScreen.Components.Add(new MouseBuildHandler());
            MapScreen.Components.Add(new MouseDragHandler());
            MapScreen.Components.Add(new MouseZoomHandler(map));
            Components.Add(new KeyboardZoomHandler(map));
            Components.Add(new MapKeyboardHandler());
            Children.Add(MapScreen);
            
            
            var toolbar = new Toolbar();
            Children.Add(toolbar);
            
            IsFocused = true;
        }

        public new void Resize(int width, int height, bool clear = false)
        {
            base.Resize(width, height, clear);
            MapScreen.Position = new Point(Math.Clamp(MapScreen.Position.X, (Global.WindowWidth / MapScreen.Font.Size.X / 2) - MapScreen.Width, Global.WindowWidth / MapScreen.Font.Size.X / 2), Math.Clamp(MapScreen.Position.Y, (Global.WindowHeight / MapScreen.Font.Size.Y / 2) - MapScreen.Height, Global.WindowHeight / MapScreen.Font.Size.Y / 2));
            IsDirty = true;
        }
    }
}
