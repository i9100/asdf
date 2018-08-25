using Microsoft.Xna.Framework;
using System;

namespace STG.Particle
{
    //public enum ParticleType { None, Enemy, Bullet, IgnoreGravity }

    public struct State
    {
        public Vector2 Velocity;
        //public ParticleType Type;
        public float LengthMultiplier;
        public float Gravity;

        public static void UpdateParticle(Manager<State>.Particle particle)
        {
            var velocity = particle.State.Velocity;
            
            particle.Position += velocity;
            particle.Orientation = velocity.ToAngle();

            float speed = velocity.Length();
            float alpha = Math.Min(1, Math.Min(particle.Life * 2, speed * 1f));
            alpha *= alpha;

            particle.Color.A = (byte)(255 * alpha);
            particle.Scale.X = particle.State.LengthMultiplier * Math.Min(Math.Min(1f, 0.2f * speed + 0.1f), alpha);

            if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < 0.00000000001f)
                velocity = Vector2.Zero;

            velocity += particle.State.Gravity * new Vector2(0, 1);
            velocity *= 0.9f;
            particle.State.Velocity = velocity;
        }
    }
}
