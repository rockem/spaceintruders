using System.Collections.Generic;
using Intruders.menu.items;
using Intruders.screen;

namespace Intruders.menu
{
    internal class SoundOptionsMenu : IMenu
    {
        #region IMenu Members

        public List<IMenuItem> CreateMenuList(GameEventListener i_MenuManager)
        {
            List<IMenuItem> menuItems = new List<IMenuItem>();
            menuItems.Add(new EffectsVolume(i_MenuManager));
            menuItems.Add(new MusicVolume(i_MenuManager));
            menuItems.Add(new ToggleSound(i_MenuManager));
            menuItems.Add(new Done(i_MenuManager));
            return menuItems;
        }

        public string GetMenuTitle()
        {
            return "Sound Options";
        }

        #endregion
    }
}