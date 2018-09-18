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

        private int invincibilityTimer = 0;

        public bool IsDead { get { return fadeTimer > 0; } }

        public bool IsInvincible { get { return invincibilityTimer > 0; } }

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
            image = Content.Sprite.Player;
            Position = startPoint;
            Radius = image.Width / 24;
        }

        public void Bomb()
        {
            Status.RemoveBomb();
            invincibilityTimer = 300;

            for (int i = 0; i < 100; i++)
            {
                float speed = 14f * (1f - 1 / rand.NextFloat(0.5f, 1f));
                var state = new Particle.State()
                {
                    Velocity = rand.NextVector2(speed, speed),
                    LengthMultiplier = 0.5f
                };

                Main.ParticleManager.CreateParticle(Content.Sprite.CircleParticle, Position, Color.White, 150, rand.NextFloat(0.4f, 1f), state);
            }
        }

        public void Shoot(int power)
        {
            Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet, new Vector2(Instance.Position.X - 8, Instance.Position.Y - 13), new Vector2(0, -25)));
            Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet, new Vector2(Instance.Position.X + 8, Instance.Position.Y - 13), new Vector2(0, -25)));

            if (power > 40)
            {
                Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet2, new Vector2(Instance.Position.X - 16, Instance.Position.Y - 13), new Vector2(-1, -20)));
                Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet2, new Vector2(Instance.Position.X + 16, Instance.Position.Y - 13), new Vector2(1, -20)));
            }

            if (power > 80)
            {
                Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet2, new Vector2(Instance.Position.X - 16, Instance.Position.Y - 13), new Vector2(-3, -20)));
                Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet2, new Vector2(Instance.Position.X + 16, Instance.Position.Y - 13), new Vector2(3, -20)));
            }

            if (power == 100)
            {
                Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet2, new Vector2(Instance.Position.X + 16, Instance.Position.Y - 13), new Vector2(-4, -30)));
                Manager.Add(new PlayerBullet(Content.Sprite.PlayerBullet2, new Vector2(Instance.Position.X - 16, Instance.Position.Y - 13), new Vector2(4, -30)));
            }
        }

        public override void Kill()
        {
            Status.RemoveLife();
            Status.ResetBomb();
            Status.RemovePower();

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
                    LengthMultiplier = 0.8f
                };

                Color color = Color.Lerp(color2, color1, rand.NextFloat(0, 1));
                Main.ParticleManager.CreateParticle(Content.Sprite.LineParticle, Position, color, 150, rand.NextFloat(0.4f, 2f), state);
            }

            invincibilityTimer = 300;
        }
        
        public override void Update()
        {
            if (!IsDead && !Status.IsGameOver)
            {
                Input.PlayerInput.Update();
                Position = Vector2.Clamp(Position, screenBorder1 + Size, screenBorder2 - Size);
                invincibilityTimer--;
            }
            else
            {
                if (Status.IsGameOver)
                {
                    Position = new Vector2(-1000, -1000);
                    instance = null;
                }
                else
                {
                    Position = new Vector2(Main.ScreenSize.X / 2, Main.ScreenSize.Y - 60 + fadeTimer);
                    fadeTimer--;
                    return;
                }
            }

            if (invincibilityTimer == 10)
            {
                for (int i = 0; i < 200; i++)
                {
                    float speed = 14f * (1f - 1 / rand.NextFloat(0.5f, 1f));
                    var state = new Particle.State()
                    {
                        Velocity = rand.NextVector2(speed, speed),
                        LengthMultiplier = 0.5f
                    };

                    Main.ParticleManager.CreateParticle(Content.Sprite.CircleParticle, Position, Color.Gray, 100, rand.NextFloat(0.4f, 1f), state);
                }
            }

            if (!IsInvincible)
                Input.PlayerInput.SetCooldown(4);
            else
                Input.PlayerInput.SetCooldown(1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Status.IsGameOver)
            {
                if (!IsInvincible)
                    base.Draw(spriteBatch);
                else
                    spriteBatch.Draw(Content.Sprite.Player_Bomb, Position, null, Color.White, Orientation, new Vector2(40, 40), 1f, 0, 0.9f);
            }
                
        }
    }
}
