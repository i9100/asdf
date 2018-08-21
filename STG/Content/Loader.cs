using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace STG.Content
{
    //Import Fonts
    static partial class Loader
    {
        public static SpriteFont MainFont { get; private set; }
    }

    //Import Backgrounds
    static partial class Loader
    {
        public static Texture2D TitleMenuBackground { get; private set; }
        public static Texture2D TitleMenuWrapper { get; private set; }
        public static Texture2D PlayingSideBar { get; private set; }
    }

    //Import In-Game Textures
    static partial class Loader
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D PlayerBullet { get; private set; }
        public static Texture2D Enemy1 { get; private set; }
        public static Texture2D EllipseBullet_W { get; private set; }
        public static Texture2D EllipseBullet_R { get; private set; }
        public static Texture2D EllipseBullet_Y { get; private set; }
        public static Texture2D EllipseBullet_G { get; private set; }
        public static Texture2D EllipseBullet_B { get; private set; }
        public static Texture2D EllipseBullet_V { get; private set; }
        public static Texture2D LineParticle { get; private set; }

    }
    static partial class Loader
    {
        public static void Load(ContentManager content)
        {
            MainFont = content.Load<SpriteFont>("Asset/Font/MainFont");

            Player = content.Load<Texture2D>("Asset/Sprite/Player");

            PlayerBullet = content.Load<Texture2D>("Asset/Sprite/Bullet/PlayerBullet");

            EllipseBullet_W = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_W");
            EllipseBullet_G = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_G");
            EllipseBullet_Y = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_Y");
            EllipseBullet_B = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_B");
            EllipseBullet_V = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_V");

            TitleMenuBackground = content.Load<Texture2D>("Asset/Background/bg");

            Enemy1 = content.Load<Texture2D>("Asset/Sprite/Enemy1");

            TitleMenuWrapper = content.Load<Texture2D>("Asset/Background/TitleMenuWrapper");

            PlayingSideBar = content.Load<Texture2D>("Asset/Background/PlayingSideBar");

            LineParticle = content.Load<Texture2D>("Asset/Sprite/Particle/Line");
        }
    }
}
