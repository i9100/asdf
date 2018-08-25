using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace STG.Entity
{
    class Player : Entity
    {
        public static Random rand = new Random();

        private static Vector2 screenBorder1 = new Vector2(Main.ScreenSize.X / 4 - 20, 0);
        private static Vector2 screenBorder2 = new Vector2(Main.ScreenSize.X * 3 / 4 + 20, Main.ScreenSize.Y);

        private Vector2 startPoint = new Vector2(Main.ScreenSize.X / 2, Main.ScreenSize.Y - 60);

        private int fadeTimer = 0;

        public bool IsDead { get { return fadeTimer > 0; } }

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
            Position = startPoint;
            Radius = image.Width / 24;
        }

        public void Kill()
        {
            Status.RemoveLife();

            fadeTimer = 120;

            float hue1 = rand.NextFloat(2.8f, 3f);
            float hue2 = (hue1 + rand.NextFloat(0, 1));
            Color color1 = ColorUtility.HSVToColor(hue1, 1f, 1);
            Color color2 = ColorUtility.HSVToColor(hue2, 0.8f, 1);

            for (int i = 0; i < 300; i++)
            {
                float speed = 15f * (1f - 1 / rand.NextFloat(0.4f, 1.1f));
                var state = new Particle.State()
                {
                    Velocity = rand.NextVector2(speed, speed),
                    //Type = Particle.ParticleType.Enemy,
                    LengthMultiplier = 0.8f
                };

                Color color = Color.Lerp(color2, color1, rand.NextFloat(0, 1));
                Main.ParticleManager.CreateParticle(Content.Loader.LineParticle, Position, color, 150, rand.NextFloat(0.4f, 2f), state);
            }
        }

        
        public override void Update()
        {
            if (!IsDead && !Status.IsGameOver)
            {
                Input.PlayerInput.Update();
                Position = Vector2.Clamp(Position, screenBorder1 + Size, screenBorder2 - Size);
            }
            else
            {
                if (Status.IsGameOver)
                    instance = null;
                else
                {
                    Position = new Vector2(Main.ScreenSize.X / 2, Main.ScreenSize.Y - 60 + fadeTimer);
                    fadeTimer--;
                    return;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Status.IsGameOver)
                base.Draw(spriteBatch);
        }
    }
}
