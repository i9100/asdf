using Microsoft.Xna.Framework;

namespace STG.Entity
{
    class PlayerBullet : Entity
    {
        public PlayerBullet(Vector2 position, Vector2 velocity)
        {
            image = Content.Loader.PlayerBullet;
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
