using System.Collections.Generic;
using GameCommon.screen;
using Intruders.comp;
using Intruders.menu;
using Intruders.menu.items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intruders.screen
{
    class MenuScreen : GameScreen, GameEventListener
    {
        private readonly MenuManager r_MenuManager;
        private readonly List<IFontComponent> r_MenuEntries = new List<IFontComponent>();
        private readonly XNAViewFactory r_Factory;
        private bool m_FullScreenEnabled;
        private GraphicsDeviceManager r_DeviceManager;


        public MenuScreen(Game i_Game, GraphicsDeviceManager i_Manager)
            : base(i_Game)
        {
            r_DeviceManager = i_Manager;
            r_Factory = new XNAViewFactory(i_Game, this);
            Add(new Background(i_Game));

            r_MenuManager = new MenuManager(this);
            r_MenuManager.SetCurrentMenu(eMenu.MainMenu);
        }

        public override void Initialize()
        {
            base.Initialize();
            updateMenuDisplay();
        }

        private void updateMenuDisplay()
        {
            removeAllMenuEntries();
            IFontComponent f = r_Factory.CreateFontComponent(@"Fonts\PauseFont");
            f.Text = r_MenuManager.GetMenuTitle();
            r_MenuEntries.Add(f);
            f.TintColor = Color.Red;
            int itemTop = 150;
            f.PositionOfOrigin = new Vector2(CenterOfViewPort.X - f.Width / 2, itemTop);
            foreach(MenuItem item in r_MenuManager.GetAllMenuItems())
            {
                IFontComponent itemFont = r_Factory.CreateFontComponent(@"Fonts\PauseFont");
                itemFont.TintColor = Color.Yellow;
                itemFont.Text = item.ItemText;
                itemTop += itemFont.Height + 5;
                itemFont.PositionOfOrigin = new Vector2(CenterOfViewPort.X - itemFont.Width / 2, itemTop);
                if(r_MenuManager.GetCurrentItem() == item)
                {
                    itemFont.TintColor = Color.Bisque;
                }
                r_MenuEntries.Add(itemFont);
            }
        }

        private void removeAllMenuEntries()
        {
            foreach(IFontComponent entry in r_MenuEntries)
            {
                Remove((IGameComponent)entry);
            }
            r_MenuEntries.Clear();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(InputManager.KeyPressed(Keys.PageUp))
            {
                r_MenuManager.RiseCurrentValue();
            }
            if(InputManager.KeyPressed(Keys.PageDown))
            {
                r_MenuManager.LowerCurrentValue();
            }
            if(InputManager.KeyPressed(Keys.Enter))
            {
                r_MenuManager.SelectCurrent();
                updateMenuDisplay();
            }
            if(InputManager.KeyPressed(Keys.Up))
            {
                r_MenuManager.SelectPrevious();  
                updateMenuDisplay();
            }
            if(InputManager.KeyPressed(Keys.Down))
            {
                r_MenuManager.SelectNext();
                updateMenuDisplay();
            }
        }

        public void ExitCurrentScreen()
        {
            ExitScreen();
        }

        public void ExitGame()
        {
            Game.Exit();
        }

        public void GoToMenu(eMenu i_Menu)
        {
            r_MenuManager.SetCurrentMenu(i_Menu);
            updateMenuDisplay();
        }

        public void GoToPreviousMenu()
        {
            r_MenuManager.SetPreviousMenuAsCurrent();
        }

        public int SoundVolume
        {
            get { return ((SoundManager)Game.Services.GetService(typeof(SoundManager))).SoundVolume; }
            set
            {
                ((SoundManager)Game.Services.GetService(typeof(SoundManager))).SoundVolume = value;
                updateMenuDisplay();
            }
        }

        public int MusicVolume
        {
            get { return ((SoundManager)Game.Services.GetService(typeof(SoundManager))).MusicVolume; }
            set
            {
                ((SoundManager)Game.Services.GetService(typeof(SoundManager))).MusicVolume = value;
                updateMenuDisplay();
            }
        }

        public bool SoundEnabled
        {
            get { return ((SoundManager)Game.Services.GetService(typeof(SoundManager))).SoundEnabled; }
        }

        public bool FullScreenMode
        {
            get { return m_FullScreenEnabled; }
        }

        public bool AllowWindowResizing
        {
            get { return Game.Window.AllowUserResizing; }
            set
            {
                Game.Window.AllowUserResizing = value;
                updateMenuDisplay();
            }
        }

        public bool MouseVisibilty
        {
            get { return Game.IsMouseVisible; }
            set 
            { 
                Game.IsMouseVisible = value;
                updateMenuDisplay();
            }
        }

        public int NumberOfPlayers
        {
            get { return GameOptions.GetInstance().NumberOfPlayers; }
            set
            {
                GameOptions.GetInstance().NumberOfPlayers = value;
                updateMenuDisplay();
            }
        }

        public void ToggleSound()
        {
            ((SoundManager)Game.Services.GetService(typeof(SoundManager))).ToggleSound();
            updateMenuDisplay();
        }

        public void ToggleFullScreen()
        {
            m_FullScreenEnabled = !m_FullScreenEnabled;
            r_DeviceManager.ToggleFullScreen();
        }

    }
}