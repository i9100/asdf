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
        public static Texture2D Enemy2 { get; private set; }
        public static Texture2D EllipseBullet_W { get; private set; }
        public static Texture2D EllipseBullet_R { get; private set; }
        public static Texture2D EllipseBullet_Y { get; private set; }
        public static Texture2D EllipseBullet_G { get; private set; }
        public static Texture2D EllipseBullet_B { get; private set; }
        public static Texture2D EllipseBullet_V { get; private set; }
        public static Texture2D SmallBullet_W { get; private set; }
        public static Texture2D SmallBullet_R { get; private set; }
        public static Texture2D SmallBullet_Y { get; private set; }
        public static Texture2D SmallBullet_G { get; private set; }
        public static Texture2D SmallBullet_B { get; private set; }
        public static Texture2D SmallBullet_V { get; private set; }
        public static Texture2D MediumBullet_R { get; private set; }
        public static Texture2D MediumBullet_Y { get; private set; }
        public static Texture2D MediumBullet_G { get; private set; }
        public static Texture2D MediumBullet_B { get; private set; }
        public static Texture2D MediumBullet_V { get; private set; }
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
            EllipseBullet_R = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_R");
            EllipseBullet_G = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_G");
            EllipseBullet_Y = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_Y");
            EllipseBullet_B = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_B");
            EllipseBullet_V = content.Load<Texture2D>("Asset/Sprite/Bullet/EllipseBullet_V");

            SmallBullet_W = content.Load<Texture2D>("Asset/Sprite/Bullet/SmallBullet_W");
            SmallBullet_R = content.Load<Texture2D>("Asset/Sprite/Bullet/SmallBullet_R");
            SmallBullet_G = content.Load<Texture2D>("Asset/Sprite/Bullet/SmallBullet_G");
            SmallBullet_Y = content.Load<Texture2D>("Asset/Sprite/Bullet/SmallBullet_Y");
            SmallBullet_B = content.Load<Texture2D>("Asset/Sprite/Bullet/SmallBullet_B");
            SmallBullet_V = content.Load<Texture2D>("Asset/Sprite/Bullet/SmallBullet_V");

            MediumBullet_R = content.Load<Texture2D>("Asset/Sprite/Bullet/MediumBullet_R");
            MediumBullet_G = content.Load<Texture2D>("Asset/Sprite/Bullet/MediumBullet_G");
            MediumBullet_Y = content.Load<Texture2D>("Asset/Sprite/Bullet/MediumBullet_Y");
            MediumBullet_B = content.Load<Texture2D>("Asset/Sprite/Bullet/MediumBullet_B");
            MediumBullet_V = content.Load<Texture2D>("Asset/Sprite/Bullet/MediumBullet_V");

            TitleMenuBackground = content.Load<Texture2D>("Asset/Background/bg");

            Enemy1 = content.Load<Texture2D>("Asset/Sprite/Enemy1");
            Enemy2 = content.Load<Texture2D>("Asset/Sprite/Enemy2");

            TitleMenuWrapper = content.Load<Texture2D>("Asset/Background/TitleMenuWrapper");

            PlayingSideBar = content.Load<Texture2D>("Asset/Background/PlayingSideBar");

            LineParticle = content.Load<Texture2D>("Asset/Sprite/Particle/Line");
        }
    }
}
