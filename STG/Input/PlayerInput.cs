using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace STG.Input
{
    static class PlayerInput
    {
        static KeyboardState oldState;

        static int playerSpeed = 10;
        static int cooldown = 4;
        static int cooldownTimer = 0;

        public static void SetCooldown(int value)
        {
            cooldown = value;
        }

        public static void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

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
                    Entity.Player.Instance.Shoot(Status.Power);
                }
            }

            if (keyboardState.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
            {
                if (Status.Bombs > 0)
                {
                    Entity.Player.Instance.Bomb();
                }
            }

            cooldownTimer--;
            oldState = keyboardState;
        }
    }
}
