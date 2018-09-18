using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace STG.Entity
{
    partial class Enemy : SubEntity
    {
        public static void Stage1Type1(Vector2 position)
        {
            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(0, 1f, 1f));
            enemy.AddShootingPattern(enemy.ShootSeekingBullet(Content.Sprite.SmallBullet_R, 4f, 10, 10000, 50));

            Manager.Add(enemy);
        }

        public static void Stage1Type2(Vector2 position)
        {
            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(180, 1f, 1f));
            enemy.AddShootingPattern(enemy.ShootSeekingBullet(Content.Sprite.SmallBullet_B, 4f, 10, 10000, 50));

            Manager.Add(enemy);
        }

        public static void Stage1Type3(Vector2 position)
        {
            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(90, 1f, 1f));
            enemy.AddShootingPattern(enemy.ShootSeekingBullet(Content.Sprite.SmallBullet_Y, 4f, 10, 10000, 50));

            Manager.Add(enemy);
        }

        public static void Stage1Type4(Vector2 position)
        {
            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(270, 1f, 1f));
            enemy.AddShootingPattern(enemy.ShootSeekingBullet(Content.Sprite.SmallBullet_G, 4f, 10, 10000, 50));

            Manager.Add(enemy);
        }

        public static void Stage1Type5(Vector2 position)
        {
            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(0, 1f, 1f));
            enemy.AddMovement(enemy.MoveInAngle(90, 1f, 0.995f));

            enemy.AddShootingPattern(enemy.ShootMultiple(Content.Sprite.EllipseBullet_R, 4f, 40, 10000, 50, 16));

            Manager.Add(enemy);
        }

        public static void Stage1Type6(Vector2 position)
        {
            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(180, 1f, 1f));
            enemy.AddMovement(enemy.MoveInAngle(90, 1f, 0.995f));

            enemy.AddShootingPattern(enemy.ShootMultiple(Content.Sprite.EllipseBullet_R, 4f, 40, 10000, 50, 16));

            Manager.Add(enemy);
        }
    }

    partial class Enemy
    {
        public static Random rand = new Random();

        private const int hitDuration = 5;

        private int hitTimer = 0;

        private int hp;

        private void AddMovement(IEnumerable<int> movement)
        {
            AddBehavior(movements, movement);
        }

        private void AddShootingPattern(IEnumerable<int> shootingPattern)
        {
            AddBehavior(shootingPatterns, shootingPattern);
        }

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
                    Main.ParticleManager.CreateParticle(Content.Sprite.LineParticle, new Vector2(Position.X, Position.Y + image.Width / 2), Color.Orange, 40, 0.5f,
                        new Particle.State()
                        {
                            Velocity = rand.NextVector2(0, 6),
                            //Type = Particle.ParticleType.Bullet,
                            LengthMultiplier = 1,
                            Gravity = 0.75f
                        });
                
            }
        }

        public override void Kill()
        {
            Shoot(Item.Bomb(Position));

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
                    //Type = Particle.ParticleType.Enemy,
                    LengthMultiplier = 0.6f
                };

                Color color = Color.Lerp(color2, color1, rand.NextFloat(0, 1));
                Main.ParticleManager.CreateParticle(Content.Sprite.LineParticle, Position, color, 125, 1.2f, state);
            }

            IsExpired = true;
        }

        public override void Update()
        {
            ApplyBehaviors(movements);
            ApplyBehaviors(shootingPatterns);

            if (hitTimer <= 0)
            {
                color = Color.White;
            }
            else
            {
                hitTimer--;
                color = Color.Red;
            }

            base.Update();

            Orientation = Velocity.ToAngle();
        }
    }

    //Movement Methods
    partial class Enemy
    {
        private List<IEnumerator<int>> movements = new List<IEnumerator<int>>();

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
    partial class Enemy
    {
        private List<IEnumerator<int>> shootingPatterns = new List<IEnumerator<int>>();

        private void Shoot(Entity entity)
        {
            Manager.Add(entity);
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

/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace STG.Entity
{
    partial class Enemy : SubEntity
    {
        public static void Stage1Type1(Vector2 position)
        {
            Action<EnemyBullet> single = (EnemyBullet enemyBullet) =>
            {
                Manager.Add(enemyBullet);
            };

            var enemy = new Enemy(Content.Sprite.Enemy1, position, 2);

            enemy.AddMovement(enemy.MoveInAngle(0, 1f, 1f));
            enemy.AddShootingPattern(enemy.Shoot(single, 10, 500, 50));

            Manager.Add(enemy);
        }
    }

    partial class Enemy
    {
        public static Random rand = new Random();

        private const int hitDuration = 5;

        private int hitTimer = 0;

        private int hp;

        private void AddMovement(IEnumerable<int> movement)
        {
            AddBehavior(movements, movement);
        }

        private void AddShootingPattern(IEnumerable<int> shootingPattern)
        {
            AddBehavior(shootingPatterns, shootingPattern);
        }

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
                    Main.ParticleManager.CreateParticle(Content.Sprite.LineParticle, new Vector2(Position.X, Position.Y + image.Width / 2), Color.Orange, 40, 0.5f,
                        new Particle.State()
                        {
                            Velocity = rand.NextVector2(0, 6),
                            //Type = Particle.ParticleType.Bullet,
                            LengthMultiplier = 1,
                            Gravity = 0.75f
                        });
                
            }
        }

        public override void Kill()
        {
            Manager.Add(Item.Bomb(Position));

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
                    //Type = Particle.ParticleType.Enemy,
                    LengthMultiplier = 0.6f
                };

                Color color = Color.Lerp(color2, color1, rand.NextFloat(0, 1));
                Main.ParticleManager.CreateParticle(Content.Sprite.LineParticle, Position, color, 125, 1.2f, state);
            }

            IsExpired = true;
        }

        public override void Update()
        {
            ApplyBehaviors(movements);
            ApplyBehaviors(shootingPatterns);

            if (hitTimer <= 0)
            {
                color = Color.White;
            }
            else
            {
                hitTimer--;
                color = Color.Red;
            }

            base.Update();

            Orientation = Velocity.ToAngle();
        }
    }

    //Movement Methods
    partial class Enemy
    {
        private List<IEnumerator<int>> movements = new List<IEnumerator<int>>();

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
    partial class Enemy
    {
        private List<IEnumerator<int>> shootingPatterns = new List<IEnumerator<int>>();

        IEnumerable<int> Shoot(EnemyBullet enemyBullet, Action<EnemyBullet> action, int cooldown, int duration, int delay)
        {
            int cooldownTimer = 0;

            int durationTimer = duration;

            while (true)
            {
                if (durationTimer >= 0)
                {
                    if (cooldownTimer <= 0)
                    {
                        action(enemyBullet);

                        cooldownTimer = cooldown;
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
*/
