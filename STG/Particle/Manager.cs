using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STG.Particle
{
    public class Manager<T>
    {
        private Action<Particle> updateParticle;
        private CircularParticleArray particleList;

        public Manager(int capacity, Action<Particle> updateParticle)
        {
            this.updateParticle = updateParticle;
            particleList = new CircularParticleArray(capacity);

            for (int i = 0; i < capacity; i++)
                particleList[i] = new Particle();
        }

        public void CreateParticle(Texture2D texture, Vector2 position, Color color, float longevity, float scale, T state, float theta = 0)
        {
            Particle particle;
            if (particleList.Count == particleList.Capacity)
            {
                particle = particleList[0];
                particleList.Start++;
            }
            else
            {
                particle = particleList[particleList.Count];
                particleList.Count++;
            }

            particle.Texture = texture;
            particle.Position = position;
            particle.Color = color;
            particle.Longevity = longevity;
            particle.Life = 0.6f;
            particle.Scale = new Vector2(scale);
            particle.Orientation = theta;
            particle.State = state;
        }

        public void Update()
        {
            int removalCount = 0;
            for (int i = 0; i < particleList.Count; i++)
            {
                var particle = particleList[i];
                updateParticle(particle);
                particle.Life -= 1f / particle.Longevity;

                Swap(particleList, i - removalCount, i);

                if (particle.Life < 0)
                    removalCount++;
            }
            particleList.Count -= removalCount;
        }

        private static void Swap(CircularParticleArray list, int a, int b)
        {
            var tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                var particle = particleList[i];

                Vector2 origin = new Vector2(particle.Texture.Width / 2, particle.Texture.Height / 2);
                spriteBatch.Draw(particle.Texture, particle.Position, null, particle.Color, particle.Orientation, origin, particle.Scale, 0, 1f);
            }
        }

        public class Particle
        {
            public Texture2D Texture;
            public Color Color;
            public float Orientation;

            public Vector2 Scale = Vector2.One;

            public Vector2 Position;
            public float Life = 1f;
            public float Longevity;
            public T State;
        }

        private class CircularParticleArray
        {
            private int start;
            public int Start
            {
                get { return start; }
                set { start = value % particleList.Length; }
            }

            public int Count { get; set; }
            public int Capacity { get { return particleList.Length; } }
            private Particle[] particleList;

            public CircularParticleArray(int capacity)
            {
                particleList = new Particle[capacity];
            }

            public Particle this[int i]
            {
                get { return particleList[(start + i) % particleList.Length]; }
                set { particleList[(start + i) % particleList.Length] = value; }
            }

        }
    }
}
