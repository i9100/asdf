using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace STG.Entity
{
    partial class Item : SubEntity
    {
        public static Item Power(Vector2 position)
        {
            var item = new Item(Content.Sprite.CircleParticle, position, ItemType.Power);

            return item;
        }

        public static Item Life(Vector2 position)
        {
            var item = new Item(Content.Sprite.CircleParticle, position, ItemType.Life);

            return item;
        }

        public static Item Bomb(Vector2 position)
        {
            var item = new Item(Content.Sprite.CircleParticle, position, ItemType.Bomb);
            item.AddBehavior(item.movements, item.MoveToPlayerConstantly(3f));

            return item;
        }
    }

    partial class Item
    {
        private List<IEnumerator<int>> movements = new List<IEnumerator<int>>();

        public enum ItemType
        {
            Power,
            Life,
            Bomb
        }

        public ItemType Type { get; private set; }

        private Item(Texture2D image, Vector2 position, ItemType itemType)
        {
            this.image = image;
            Position = position;
            Radius = image.Height;
            Type = itemType;
        }

        public override void Update()
        {
            ApplyBehaviors(movements);
            base.Update();
        }
    }

    partial class Item
    {
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
    }
}
