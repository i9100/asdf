using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace STG.Entity
{
    partial class EnemyBullet : Entity
    {
        private List<IEnumerator<int>> movements = new List<IEnumerator<int>>();

        public EnemyBullet(Texture2D image, Vector2 position, float radiusDivider = 4f)
        {
            this.image = image;
            Position = position;
            Radius = image.Height / radiusDivider;
        }

        public override void Update()
        {
            ApplyMovements();
            Position += Velocity;
            Velocity *= 0.8f;
            Orientation = Velocity.ToAngle();
        }

        public void Kill()
        {
            IsExpired = true;
        }

    }

    partial class EnemyBullet
    {
        IEnumerable<int> MoveToPlayer(float acceleration)
        {
            Vector2 dp = Player.Instance.Position - Position;
            dp.Normalize();

            while (true)
            {
                Velocity += dp.ScaleTo(acceleration);

                yield return 0;
            }
        }

        IEnumerable<int> MoveToPlayerConstantly(float acceleration)
        {
            while (true)
            {
                Velocity += (Player.Instance.Position - Position).ScaleTo(acceleration);
                if (Velocity != Vector2.Zero)
                    Orientation = Velocity.ToAngle();

                yield return 0;
            }
        }

        IEnumerable<int> MoveStraight(float angle, float acceleration)
        {
            float rad = angle.ToRadian();
            Vector2 velocity = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            while (true)
            {
                Velocity = velocity.ScaleTo(acceleration);

                yield return 0;                
            }
        }

        IEnumerable<int> MoveAfterAngleChanges(float angle1, float angle2, float acceleration)
        {
            float rad1 = angle1.ToRadian();
            float rad2 = angle2.ToRadian();
            Vector2 velocity = new Vector2((float)Math.Cos(rad1), (float)Math.Sin(rad1));
            Vector2 velocity2 = new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2));
            while (true)
            {

                for (int i = 0; i < 120; i++)
                {
                    Velocity = velocity.ScaleTo(acceleration);

                    yield return 0;
                }

                for (int i = 0; i < 2000; i++)
                {
                    Velocity = velocity2.ScaleTo(acceleration);

                    yield return 0;
                }

            }
        }

        IEnumerable<int> Untitled(float angle, float acceleration)
        {
            float rad = angle.ToRadian();
            Vector2 velocity1 = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            while (true)
            {

                for (int i = 0; i < 180; i++)
                {
                    Vector2 velocity = new Vector2((float)Math.Cos(MathUtility.ToRadian(i / 1.2f)), (float)Math.Sin(rad));

                    Velocity = velocity.ScaleTo(2.5f);

                    yield return 0;
                }

                for (int i = 0; i < 360; i++)
                {
                    
                    Velocity = velocity1.ScaleTo(3f);

                    yield return 0;
                }

            }
        }

        IEnumerable<int> Untitled2(float angle, float acceleration)
        {
            float rad = angle.ToRadian();
            float rad1 = (angle + 90).ToRadian();
            Vector2 velocity = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            Vector2 velocity1 = new Vector2((float)Math.Cos(rad1), (float)Math.Sin(rad1));
            while (true)
            {

                for (int i = 0; i < 30; i++)
                {
                    Velocity = velocity.ScaleTo(2f);

                    yield return 0;
                }

                for (int i = 0; i < 900; i++)
                {

                    Velocity = velocity1.ScaleTo(5f);

                    yield return 0;
                }

            }
        }
        IEnumerable<int> Untitled3(float angle, float acceleration)
        {
            float rad = angle.ToRadian();
            float rad1 = (angle - 90).ToRadian();
            Vector2 velocity = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            Vector2 velocity1 = new Vector2((float)Math.Cos(rad1), (float)Math.Sin(rad1));
            while (true)
            {

                for (int i = 0; i < 30; i++)
                {
                    Velocity = velocity.ScaleTo(2f);

                    yield return 0;
                }

                for (int i = 0; i < 900; i++)
                {

                    Velocity = velocity1.ScaleTo(5f);

                    yield return 0;
                }

            }
        }


        private void AddMovement(IEnumerable<int> movement)
        {
            movements.Add(movement.GetEnumerator());
        }


        private void ApplyMovements()
        {
            for (int i = 0; i < movements.Count; i++)
            {
                if (!movements[i].MoveNext())
                    movements.RemoveAt(i--);
            }
        }

    }

    partial class EnemyBullet
    {
        public static EnemyBullet LinearBullet(Texture2D image, Vector2 position, float acceleration, float angle)
        {
            var bullet = new EnemyBullet(image, position);
            bullet.AddMovement(bullet.Untitled2(angle, acceleration));

            return bullet;
        }

        public static EnemyBullet LinearBullet2(Texture2D image, Vector2 position, float acceleration, float angle)
        {
            var bullet = new EnemyBullet(image, position);
            bullet.AddMovement(bullet.Untitled3(angle, acceleration));

            return bullet;
        }

        public static EnemyBullet SeekingBullet(Texture2D image, Vector2 position, float acceleration)
        {
            var bullet = new EnemyBullet(image, position);
            bullet.AddMovement(bullet.MoveToPlayer(acceleration));

            return bullet;
        }
    }
}
