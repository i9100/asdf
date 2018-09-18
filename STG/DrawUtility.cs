using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STG
{
    static class DrawUtility
    {
        public static void DrawStringWithStroke(this SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color, Color strokeColor, float scale)
        {
            Vector2 origin = Vector2.Zero;

            spriteBatch.DrawString(spriteFont, text, position + new Vector2(-scale, -scale), strokeColor, 0, origin, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(spriteFont, text, position + new Vector2(scale, -scale), strokeColor, 0, origin, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(spriteFont, text, position + new Vector2(-scale, scale), strokeColor, 0, origin, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(spriteFont, text, position + new Vector2(scale, scale), strokeColor, 0, origin, scale, SpriteEffects.None, 1f);

            spriteBatch.DrawString(spriteFont, text, position, color, 0, origin, 1f, SpriteEffects.None, 0.1f);
        }
    }
}
