using System.Collections.Generic;
using Intruders.menu.items;
using Intruders.screen;

namespace Intruders.menu
{
    interface IMenu
    {
        List<IMenuItem> CreateMenuList(GameEventListener i_MenuManager);

        string GetMenuTitle();
    }
}