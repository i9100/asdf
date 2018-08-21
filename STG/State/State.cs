using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace STG.State
{
    public abstract class State
    {
        protected Main main;

        protected GraphicsDevice graphics;

        public State(Main main, GraphicsDevice graphics)
        {
            this.main = main;

            this.graphics = graphics;
        }

        abstract public void Initialize();

        abstract public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics);

        abstract public void Update(GameTime gameTime);
    }
}
