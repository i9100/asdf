using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace STG.Entity
{
    partial class Enemy : Entity
    {
        public static void Stage1Type1(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 2);

            enemy.AddBehavior(enemy.MoveToPlayer(1f), enemy.Movements);
            enemy.AddBehavior(enemy.ShootStraight(Content.Loader.EllipseBullet_B, 1.5f, 50, 10000, 50), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }

        public static void Stage1Type2(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 14);

            enemy.AddBehavior(enemy.MoveInAngle(90f, 0.6f, 1f), enemy.Movements);

            for (int i = 0; i < 3; i++)
            {
                enemy.AddBehavior(enemy.ShootInAngle(Content.Loader.EllipseBullet_Y, 1f, 150, 300, 50, 75 + 15 * i), enemy.ShootingPatterns);
            }

            Manager.Add(enemy);
        }

        public static void Stage1Type3(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 2);

            enemy.AddBehavior(enemy.MoveInAngle(30f, 1f, 1f), enemy.Movements);

            enemy.AddBehavior(enemy.ShootStraight(Content.Loader.EllipseBullet_B, 1f, 20, 10000, 50, 90), enemy.ShootingPatterns);
            enemy.AddBehavior(enemy.ShootStraight(Content.Loader.EllipseBullet_B, 1f, 20, 10000, 50, -90), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }

        public static void Stage1Type4(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 2);

            enemy.AddBehavior(enemy.MoveInAngle(150f, 1f, 1f), enemy.Movements);

            enemy.AddBehavior(enemy.ShootStraight(Content.Loader.EllipseBullet_B, 1f, 20, 10000, 50, 90), enemy.ShootingPatterns);
            enemy.AddBehavior(enemy.ShootStraight(Content.Loader.EllipseBullet_B, 1f, 20, 10000, 50, -90), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }

        public static void Stage1Type5(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 2);

            enemy.AddBehavior(enemy.MoveInAngle(90f, 0.7f, 1.005f), enemy.Movements);

            enemy.AddBehavior(enemy.ShootSeekingBullet(Content.Loader.EllipseBullet_B, 1f, 100, 10000, 50), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }

        public static void Stage1Type6(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 30);

            enemy.AddBehavior(enemy.MoveInAngle(90f, 2f, 0.95f), enemy.Movements);

            for (int i = 0; i < 6; i++)
            {
                enemy.AddBehavior(enemy.ShootMultiple(Content.Loader.EllipseBullet_B, 0.8f, 100, 10000, 50, 8, 0, 15), enemy.ShootingPatterns);
            }

            Manager.Add(enemy);
        }

        public static void Stage1Type7(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 45);

            enemy.AddBehavior(enemy.MoveInAngle(90f, 2f, 0.95f), enemy.Movements);

            enemy.AddBehavior(enemy.ShootMultiple(Content.Loader.EllipseBullet_B, 0.8f, 5, 20, 100, 64, 0, 7.5f), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }

        public static void Stage1Type8(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 45);

            enemy.AddBehavior(enemy.MoveInAngle(90f, 2f, 0.97f), enemy.Movements);

            enemy.AddBehavior(enemy.ShootIncludedAngle(Content.Loader.EllipseBullet_B, 2f, 20, 200, 20, -24, 60, 120), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }

        public static void Stage1Type9(Vector2 position)
        {
            var enemy = new Enemy(Content.Loader.Enemy1, position, 45);

            enemy.AddBehavior(enemy.MoveInAngle(90f, 2f, 0.97f), enemy.Movements);

            enemy.AddBehavior(enemy.SprayMultiple(Content.Loader.EllipseBullet_B, 0.5f, 5, 200, 20, 24, 10, 120), enemy.ShootingPatterns);
            enemy.AddBehavior(enemy.SprayMultiple(Content.Loader.EllipseBullet_Y, 1f, 2, 200, 20, 24, 5, 120), enemy.ShootingPatterns);
            enemy.AddBehavior(enemy.SprayMultiple(Content.Loader.EllipseBullet_G, 1f, 2, 200, 20, 24, 5, 120), enemy.ShootingPatterns);
            enemy.AddBehavior(enemy.SprayMultiple(Content.Loader.EllipseBullet_V, 2f, 2, 200, 20, 24, 5, 120), enemy.ShootingPatterns);
            enemy.AddBehavior(enemy.SprayMultiple(Content.Loader.EllipseBullet_W, 2f, 2, 200, 20, 24, 5, 120), enemy.ShootingPatterns);

            Manager.Add(enemy);
        }
    }

    partial class Enemy : Entity
    {
        public static Random rand = new Random();

        private const int hitDuration = 5;

        private int hitTimer = 0;

        private int hp;

        public Enemy(Texture2D image, Vector2 position, int hp)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2.5f;
            color = Color.White;
            this.hp = hp;
        }

        public void WasShot()
        {
            if (hp < 0)
                Kill();
            else {
                hp--;
                hitTimer = hitDuration;
                for (int i = 0; i < 8; i++)
                    Main.ParticleManager.CreateParticle(Content.Loader.LineParticle, new Vector2(Position.X, Position.Y + image.Width / 2), Color.Orange, 40, 0.5f,
                        new Particle.State()
                        {
                            Velocity = rand.NextVector2(0, 6),
                            Type = Particle.ParticleType.Bullet,
                            LengthMultiplier = 1,
                            Gravity = 0.75f
                        });
                
            }
        }

        public void Kill()
        {
            IsExpired = true;

            float hue1 = rand.NextFloat(0, 0.5f);
            float hue2 = (hue1 + rand.NextFloat(0, 1));
            Color color1 = ColorUtility.HSVToColor(hue1, 0.9f, 1);
            Color color2 = ColorUtility.HSVToColor(hue2, 0.8f, 1);

            for (int i = 0; i < 200; i++)
            {
                float speed = 12f * (1f - 1 / rand.NextFloat(0.5f, 2f));
                var state = new Particle.State()
                {
                    Velocity = rand.NextVector2(speed, speed),
                    Type = Particle.ParticleType.Enemy,
                    LengthMultiplier = 0.6f
                };

                Color color = Color.Lerp(color2, color1, rand.NextFloat(0, 1));
                Main.ParticleManager.CreateParticle(Content.Loader.LineParticle, Position, color, 125, 1.2f, state);
            }
        }

        public override void Update()
        {
            ApplyBehaviors(Movements);
            ApplyBehaviors(ShootingPatterns);

            if (hitTimer <= 0)
            {
                color = Color.White;
            }
            else
            {
                hitTimer--;
                color = Color.Red;
            }

            Position += Velocity;
            Velocity *= 0.8f;

            Orientation = Velocity.ToAngle();
        }

        private void AddBehavior(IEnumerable<int> behavior, List<IEnumerator<int>> behaviors)
        {
            behaviors.Add(behavior.GetEnumerator());
        }


        private void ApplyBehaviors(List<IEnumerator<int>> behaviors)
        {
            for (int i = 0; i < behaviors.Count; i++)
            {
                if (!behaviors[i].MoveNext())
                    behaviors.RemoveAt(i--);
            }
        }
    }

    //Movement Methods
    partial class Enemy : Entity
    {
        private List<IEnumerator<int>> Movements = new List<IEnumerator<int>>();

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

        IEnumerable<int> MoveInAngle(float angle, float acceleration, float multiplier)
        {
            float rad = angle.ToRadian();
            Vector2 velocity = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            while (true)
            {
                Velocity += velocity.ScaleTo(acceleration);
                acceleration = acceleration * multiplier;
                yield return 0;
            }
        }

        IEnumerable<int> MoveInASquare()
        {
            const int framesPerSide = 30;
            while (true)
            {

                for (int i = 0; i < framesPerSide; i++)
                {
                    Velocity = Vector2.UnitX * 2;
                    yield return 0;
                }
            }
        }
    }

    //Shooting Methods
    partial class Enemy : Entity
    {
        private List<IEnumerator<int>> ShootingPatterns = new List<IEnumerator<int>>();

        private void Shoot(EnemyBullet bullet)
        {
            Manager.Add(bullet);
        }

        IEnumerable<int> ShootStraight(Texture2D image, float acceleration, int cooldown, int duration, int delay, float angle = 0)
        {
            int cooldownTimer = cooldown;

            int durationTimer = duration;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        cooldownTimer = cooldown;
                        Shoot(EnemyBullet.LinearBullet(image, Position, acceleration, Velocity.ToAngle().ToDegree() + angle));
                    }

                    cooldownTimer--;
                }
                else if (durationTimer <= -delay)
                {
                    durationTimer = duration;
                }

                durationTimer--;

                yield return 0;
            }
        }

        IEnumerable<int> ShootInAngle(Texture2D image, float acceleration, int cooldown, int duration, int delay, float angle)
        {
            int cooldownTimer = 0;

            int durationTimer = duration;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        cooldownTimer = cooldown;
                        Shoot(EnemyBullet.LinearBullet(image, Position, acceleration, angle));
                    }

                    cooldownTimer--;
                }
                else if (durationTimer <= -delay)
                {
                    durationTimer = duration;
                }

                durationTimer--;

                yield return 0;
            }
        }

        IEnumerable<int> ShootMultiple(Texture2D image, float acceleration, int cooldown, int duration, int delay, int ways, float angle = 0, float offset = 0)
        {
            int cooldownTimer = 0;

            int durationTimer = duration;

            float _offset = offset;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        for (int i = 0; i < ways; i++)
                            Shoot(EnemyBullet.LinearBullet(image, Position, acceleration, (360 / ways * i) + angle + _offset));

                        cooldownTimer = cooldown;
                        _offset += offset;
                    }

                    cooldownTimer--;
                }
                else if (durationTimer <= -delay)
                {
                    durationTimer = duration;
                }

                durationTimer--;

                yield return 0;
            }
        }

        IEnumerable<int> SprayMultiple(Texture2D image, float acceleration, int cooldown, int duration, int delay, int ways, int minMax, float angle = 0)
        {
            int cooldownTimer = 0;

            int durationTimer = duration;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        Shoot(EnemyBullet.LinearBullet2(image, Position, acceleration, rand.NextFloat(0, 360)));
                        Shoot(EnemyBullet.LinearBullet(image, Position, acceleration, rand.NextFloat(0, 360)));

                        cooldownTimer = rand.Next(cooldown - minMax, cooldown + minMax);
                    }

                    cooldownTimer--;
                }
                else if (durationTimer <= -delay)
                {
                    durationTimer = duration;
                }

                durationTimer--;

                yield return 0;
            }
        }

        IEnumerable<int> ShootIncludedAngle(Texture2D image, float acceleration, int cooldown, int duration, int delay, int ways, float startAngle, float endAngle, float offset = 0)
        {
            int cooldownTimer = 0;

            int durationTimer = duration;

            float _offset = offset;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        if (ways < 0)
                            for (int i = 0; i < Math.Abs(ways); i++)
                                Shoot(EnemyBullet.LinearBullet(image, Position, acceleration, rand.NextFloat(startAngle, endAngle) + _offset));
                        else
                            for (int i = 0; i < ways; i++)
                                Shoot(EnemyBullet.LinearBullet(image, Position, acceleration, (endAngle - startAngle) / ways * i + startAngle + _offset));

                        cooldownTimer = cooldown;
                        _offset += offset;
                    }

                    cooldownTimer--;
                }
                else if (durationTimer <= -delay)
                {
                    durationTimer = duration;
                }

                durationTimer--;

                yield return 0;
            }
        }

        IEnumerable<int> ShootSeekingBullet(Texture2D image, float acceleration, int cooldown, int duration, int delay, float offset = 0)
        {
            int cooldownTimer = 0;

            int durationTimer = duration;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        cooldownTimer = cooldown;
                        Shoot(EnemyBullet.SeekingBullet(image, Position, acceleration));
                    }

                    cooldownTimer--;
                }
                else if (durationTimer <= -delay)
                {
                    durationTimer = duration;
                }

                durationTimer--;

                yield return 0;
            }
        }
    }
}
