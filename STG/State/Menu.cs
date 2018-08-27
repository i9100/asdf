using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace STG.State
{
    class Menu : State
    {

        private KeyboardState oldState;

        private const int positionY = 500;
        private const int space = 75;

        private readonly string[] titleMenus = { "Start", "Settings", "Exit" };

        private readonly string[] settingMenus = { "Fullscreen", "Back" };

        private readonly string[] difficultyMenus = { "Easy", "Normal", "Back" };

        string[] currentMenus;

        private int currentIndex;

        public Menu(Main main, GraphicsDevice graphics) : base(main, graphics)
        {
            this.main = main;

            this.graphics = graphics;
        }

        private int CurrentIndex
        {
            get
            {
                return currentIndex;
            }

            set
            {
                if (value < 0)
                    currentIndex = currentMenus.Length - 1;
                else if (value >= currentMenus.Length)
                    currentIndex = 0;
                else
                    currentIndex = value;
            }
        }

        private void HighlightSelected(SpriteBatch spriteBatch, string[] Menus)
        {
            int space = currentIndex * 75;

            string currentMenu = Menus[currentIndex];

            spriteBatch.DrawString(Content.Loader.MainFont, currentMenu, new Vector2(50, positionY + space), Color.White);
        }

        private void Select(string selectedMenu)
        {
            switch (selectedMenu)
            {
                case "Start":
                    //currentMenus = difficultyMenus;
                    main.ChangeState(new Playing(main, graphics));
                    break;
                /*case "Easy":
                    break;
                case "Normal":
                    break;*/
                case "Settings":
                    currentMenus = settingMenus;
                    break;
                case "Back":
                    currentMenus = titleMenus;
                    break;
                case "Exit":
                    main.Exit();
                    break;
            }

            currentIndex = 0;
        }

        public override void Initialize()
        {
            currentMenus = titleMenus;
            CurrentIndex = 0;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                CurrentIndex++;
            else if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                CurrentIndex--;
            else if (newState.IsKeyDown(Keys.Z) && oldState.IsKeyUp(Keys.Z))
                Select(currentMenus[CurrentIndex]);
            else if (newState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
                if (CurrentIndex == currentMenus.Length - 1)
                    Select(currentMenus[currentMenus.Length - 1]);
                else
                    CurrentIndex = currentMenus.Length - 1;

            oldState = newState;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            int menuPositionY = positionY;

            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(Content.Loader.TitleMenuBackground, new Rectangle(0, 0, 1280, 960), Color.White);
            spriteBatch.Draw(Content.Loader.TitleMenuWrapper, new Rectangle(0, 0, 300, 960), new Color(0, 0, 0, 200));

            foreach (string name in currentMenus)
            {
                spriteBatch.DrawString(Content.Loader.MainFont, name, new Vector2(50, menuPositionY), Color.Gray);
                menuPositionY += space;
            }

            HighlightSelected(spriteBatch, currentMenus);

            spriteBatch.End();
        }
    }
}
