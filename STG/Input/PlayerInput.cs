using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace STG.Input
{
    static class PlayerInput
    {
        static KeyboardState keyboardState;

        static int playerSpeed = 10;
        static Vector2 playerBulletVelocity = new Vector2(0, -40);
        const int cooldown = 3;
        static int cooldownTimer = 0;

        public static void Update()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                Entity.Player.Instance.Position.X -= playerSpeed;

            if (keyboardState.IsKeyDown(Keys.Right))
                Entity.Player.Instance.Position.X += playerSpeed;

            if (keyboardState.IsKeyDown(Keys.Up))
                Entity.Player.Instance.Position.Y -= playerSpeed;

            if (keyboardState.IsKeyDown(Keys.Down))
                Entity.Player.Instance.Position.Y += playerSpeed;

            if (keyboardState.IsKeyDown(Keys.LeftShift))
                playerSpeed = 4;
            else
                playerSpeed = 10;

            if (keyboardState.IsKeyDown(Keys.Z))
            {
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = cooldown;
                    Entity.Manager.Add(new Entity.PlayerBullet(new Vector2(Entity.Player.Instance.Position.X - 17, Entity.Player.Instance.Position.Y - 13), playerBulletVelocity));
                    Entity.Manager.Add(new Entity.PlayerBullet(new Vector2(Entity.Player.Instance.Position.X + 16, Entity.Player.Instance.Position.Y - 13), playerBulletVelocity));
                }
            }

            cooldownTimer--;
        }
    }
}
