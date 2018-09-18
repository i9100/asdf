using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STG.Entity
{
    class PlayerBullet : Entity
    {
        public PlayerBullet(Texture2D image, Vector2 position, Vector2 velocity)
        {
            this.image = image;
            Position = position;
            Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8;
        }

        public override void Update()
        {
            Position += Velocity;
            if (!Main.Viewport.Bounds.Contains(Position.ToPoint()))
                IsExpired = true;
        }
    }
}
