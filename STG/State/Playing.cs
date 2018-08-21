using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace STG.State
{
    class Playing : State
    {
        public Playing(Main main, GraphicsDevice graphics) : base(main, graphics)
        {
            this.main = main;

            this.graphics = graphics;
        }

        public override void Initialize()
        {
            Entity.Manager.Add(Entity.Player.Instance);
            Stage.Spawner.Update(1);
        }

        public override void Update(GameTime gameTime)
        {
            Entity.Manager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            graphics.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            Entity.Manager.Draw(spriteBatch);
            Main.ParticleManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            spriteBatch.Draw(Content.Loader.PlayingSideBar, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, 0, 0);
            spriteBatch.End();
        }
    }
}
