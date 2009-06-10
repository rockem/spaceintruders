using System.Collections.Generic;
using Intruders.menu.items;
using Intruders.screen;

namespace Intruders.menu
{
    internal class ScreenOptionsMenu : IMenu
    {
        #region IMenu Members

        public List<IMenuItem> CreateMenuList(GameEventListener i_MenuManager)
        {
            List<IMenuItem> menuItems = new List<IMenuItem>();
            menuItems.Add(new FullScreenMode(i_MenuManager));
            menuItems.Add(new WindowResizing(i_MenuManager));
            menuItems.Add(new MouseVisability(i_MenuManager));
            menuItems.Add(new Done(i_MenuManager));
            return menuItems;
        }

        public string GetMenuTitle()
        {
            return "Screen Options";
        }

        #endregion
    }
}