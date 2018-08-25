using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STG.State
{
    class GameOver : State
    {
        public GameOver(Main main, GraphicsDevice graphics) : base(main, graphics)
        {
            this.main = main;

            this.graphics = graphics;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
