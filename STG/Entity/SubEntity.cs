using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG.Entity
{
    abstract class SubEntity : Entity
    {
        protected void AddBehavior(List<IEnumerator<int>> behaviors, IEnumerable<int> behavior)
        {
            behaviors.Add(behavior.GetEnumerator());
        }

        protected void ApplyBehaviors(List<IEnumerator<int>> behaviors)
        {
            for (int i = 0; i < behaviors.Count; i++)
                if (!behaviors[i].MoveNext())
                    behaviors.RemoveAt(i--);
        }

        public override void Update()
        {
            Position += Velocity;
            Velocity *= 0.8f;
        }
    }
}
