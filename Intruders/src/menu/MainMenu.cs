using System.Collections.Generic;
using Intruders.menu.items;
using Intruders.screen;

namespace Intruders.menu
{
    class MainMenu : IMenu
    {
        public List<IMenuItem> CreateMenuList(GameEventListener i_MenuManager)
        {
            List<IMenuItem> menuItems = new List<IMenuItem>();
            menuItems.Add(new NumberOfPlayers(i_MenuManager));
            menuItems.Add(new ScreenOptions(i_MenuManager));
            menuItems.Add(new SoundOptions(i_MenuManager));
            menuItems.Add(new Play(i_MenuManager));
            menuItems.Add(new Quit(i_MenuManager));
            return menuItems;
        }

        public string GetMenuTitle()
        {
            return "Main Menu";
        }
    }
}