using Microsoft.Xna.Framework;
using System;

namespace STG.Entity
{
    class Player : Entity
    {
        private static Vector2 screenBorder1 = new Vector2(Main.ScreenSize.X / 4 - 20, 0);
        private static Vector2 screenBorder2 = new Vector2(Main.ScreenSize.X * 3 / 4 + 20, Main.ScreenSize.Y);

        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }
        }

        private Player()
        {
            image = Content.Loader.Player;
            Position = Main.ScreenSize / 2;
            Radius = image.Width / 24;
        }

        public override void Update()
        {
            Input.PlayerInput.Update();
            Position = Vector2.Clamp(Position, screenBorder1 + Size, screenBorder2 - Size);
        }
    }
}
