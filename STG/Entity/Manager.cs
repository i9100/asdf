using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace STG.Entity
{
    static class Manager
    {
        static List<Entity> entities = new List<Entity>();

        static bool isUpdating;
        static List<Entity> addedEntities = new List<Entity>();

        static List<Enemy> enemies = new List<Enemy>();
        static List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
        static List<PlayerBullet> playerBullets = new List<PlayerBullet>();
        static List<Item> items = new List<Item>();

        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);
            if (entity is PlayerBullet)
                playerBullets.Add(entity as PlayerBullet);
            else if (entity is Enemy)
                enemies.Add(entity as Enemy);
            else if (entity is EnemyBullet)
                enemyBullets.Add(entity as EnemyBullet);
            else if (entity is Item)
                items.Add(entity as Item);
        }

        public static int Count
        {
            get
            {
                return entities.Count();
            }
        }

        public static void Add(Entity entity)
        {
            if (!isUpdating)
                AddEntity(entity);
            else
                addedEntities.Add(entity);
        }

        public static void Clear()
        {
            entities.Clear();
        }

        private static bool IsColliding(Entity a, Entity b)
        {
            float radius = a.Radius + b.Radius;
            return !a.IsExpired && !b.IsExpired && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        }

        private static bool IsOutofBound(Vector2 position)
        {
            return position.X < 120 || position.X > 1160 || position.Y < -200 || position.Y > 1160;
        }

        private static void HandleCollisions()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                for (int j = 0; j < playerBullets.Count; j++)
                {
                    if (IsColliding(enemies[i], playerBullets[j]))
                    {
                        enemies[i].WasShot();
                        playerBullets[j].IsExpired = true;
                    }
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (IsColliding(enemies[i], Player.Instance) && !Player.Instance.IsInvincible)
                {
                    Player.Instance.Kill();
                    enemies.ForEach(e => e.Kill());
                    enemyBullets.ForEach(b => b.Kill());
                }
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (IsOutofBound(enemyBullets[i].Position))
                    enemyBullets[i].Kill();

                if (IsColliding(enemyBullets[i], Player.Instance) && !Player.Instance.IsInvincible)
                {
                   Player.Instance.Kill();
                   enemies.ForEach(e => e.Kill());
                   enemyBullets.ForEach(b => b.Kill());
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (IsOutofBound(items[i].Position))
                    items[i].Kill();

                if (IsColliding(items[i], Player.Instance))
                {
                    var type = items[i].Type;

                    switch (type)
                    {
                        case Item.ItemType.Power:
                            Status.AddPower();
                            break;
                        case Item.ItemType.Life:
                            Status.AddLife();
                            break;
                        case Item.ItemType.Bomb:
                            Status.AddBomb();
                            break;
                    }

                    items[i].Kill();
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            try
            {
                isUpdating = true;

                HandleCollisions();

                foreach (var entity in entities)
                    entity.Update();

                isUpdating = false;

                foreach (var entity in addedEntities)
                    AddEntity(entity);

                addedEntities.Clear();

                entities = entities.Where(x => !x.IsExpired).ToList();
                enemies = enemies.Where(x => !x.IsExpired).ToList();
                playerBullets = playerBullets.Where(x => !x.IsExpired).ToList();
                enemyBullets = enemyBullets.Where(x => !x.IsExpired).ToList();
                items = items.Where(x => !x.IsExpired).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public static IEnumerable<Entity> GetEntitiesInRange(Vector2 Position, float radius)
        {
            return entities.Where(e => Vector2.DistanceSquared(Position, e.Position) < radius * radius);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (!isUpdating)
                    foreach (var entity in entities)
                        entity.Draw(spriteBatch);
            }
            catch (Exception e)
            {

            }
        }
    }
}
