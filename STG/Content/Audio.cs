using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG.Content
{
    static class Audio
    {
        public static Song Music { get; private set; }

        private static readonly Random rand = new Random();

        private static SoundEffect[] explosions;
        public static SoundEffect Explosion
        {
            get { return explosions[rand.Next(explosions.Length)]; }
        }

        private static SoundEffect[] shots;
        public static SoundEffect Shot
        {
            get { return shots[rand.Next(shots.Length)]; }
        }

        public static void Load(ContentManager content)
        {
            Music = content.Load<Song>("Audio/Music");
        }
    }
}
