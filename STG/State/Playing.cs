using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace STG.State
{
    class Playing : State
    {
        int alpha = 0;
        readonly int lives = Status.Lives;

        public Playing(Main main, GraphicsDevice graphics) : base(main, graphics)
        {
            this.main = main;

            this.graphics = graphics;
        }

        public override void Initialize()
        {
            Status.Reset();
            Entity.Manager.Clear();
            Entity.Manager.Add(Entity.Player.Instance);
            Stage.Spawner.Update(1);
        }

        public override void Update(GameTime gameTime)
        {
            Entity.Manager.Update(gameTime);

            if (Status.IsGameOver)
            {
                alpha += 2;

                if (alpha > 200)
                    main.ChangeState(new Menu(main, graphics));
            }    
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            graphics.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            Entity.Manager.Draw(spriteBatch);
            Main.ParticleManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            spriteBatch.Draw(Content.Sprite.PlayingSideBar, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, 0, 0);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            spriteBatch.DrawString(Content.Sprite.MainFont, "Score: " + Status.Score, new Vector2(25, 50), Color.White);

            if (Status.Lives < 0)
                spriteBatch.DrawString(Content.Sprite.MainFont, "Lives: " + 0, new Vector2(25, 150), Color.White);
            else
                spriteBatch.DrawString(Content.Sprite.MainFont, "Lives: " + Status.Lives, new Vector2(25, 150), Color.White);

            spriteBatch.DrawString(Content.Sprite.MainFont, "Bombs: " + Status.Bombs, new Vector2(25, 200), Color.White);
            spriteBatch.DrawStringWithStroke(Content.Sprite.MainFont, "Power: " + Status.Power, new Vector2(25, 250), Color.White, Color.Black, 1f);
            spriteBatch.End();

            if (Status.IsGameOver)
            {
                Texture2D blackTexture = new Texture2D(graphics, 1, 1);
                blackTexture.SetData(new[] { Color.White });

                spriteBatch.Begin();
                spriteBatch.Draw(blackTexture, new Rectangle(0, 0, 1280, 960), new Color(0, 0, 0, alpha));
                spriteBatch.End();
            }
        }
    }
}
