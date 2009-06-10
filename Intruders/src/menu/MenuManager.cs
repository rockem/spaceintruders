using System.Collections;
using System.Collections.Generic;
using GameCommon.screen;
using Intruders.menu.items;
using Intruders.screen;

namespace Intruders.menu
{
    class MenuManager
    {
        private readonly Dictionary<eMenu, IMenu> r_MenusMap = new Dictionary<eMenu, IMenu>();
        private List<IMenuItem> m_CurrentMenuItems;
        private string m_MenuTitle;
        private int m_SelectedItem;
        private readonly GameEventListener r_EventListener;
        private eMenu m_PreviousMenu = eMenu.MainMenu;
        private eMenu m_CurrentMenu = eMenu.MainMenu;

        public MenuManager(GameEventListener i_Listener)
        {
            r_EventListener = i_Listener;
            initMenuMap();
        }

        private void initMenuMap()
        {
            r_MenusMap.Add(eMenu.MainMenu, new MainMenu());
            r_MenusMap.Add(eMenu.SoundOptions, new SoundOptionsMenu());
            r_MenusMap.Add(eMenu.ScreenOptions, new ScreenOptionsMenu());
        }

        public void RiseCurrentValue()
        {
            m_CurrentMenuItems[m_SelectedItem].RiseValue();
        }

        public void LowerCurrentValue()
        {
            m_CurrentMenuItems[m_SelectedItem].LowerValue();
        }

        public void SelectCurrent()
        {
            m_CurrentMenuItems[m_SelectedItem].Select();
        }

        public void SelectPrevious()
        {
            m_SelectedItem--;
            if(m_SelectedItem < 0)
            {
                m_SelectedItem = m_CurrentMenuItems.Count - 1;
            }
        }

        public void SelectNext()
        {
            m_SelectedItem++;
            if (m_SelectedItem == m_CurrentMenuItems.Count)
            {
                m_SelectedItem = 0;
            }
        }

        public void SetCurrentMenu(eMenu i_Menu)
        {
            setMenu(i_Menu);
        }

        private void setMenu(eMenu i_Menu)
        {
            m_PreviousMenu = m_CurrentMenu;
            m_CurrentMenu = i_Menu;
            m_CurrentMenuItems = r_MenusMap[i_Menu].CreateMenuList(r_EventListener);
            m_MenuTitle = r_MenusMap[i_Menu].GetMenuTitle();
            m_SelectedItem = 0;
        }

        public string GetMenuTitle()
        {
            return m_MenuTitle;
        }

        public IEnumerable GetAllMenuItems()
        {
            return m_CurrentMenuItems;
        }

        public IMenuItem GetCurrentItem()
        {
            return m_CurrentMenuItems[m_SelectedItem];
        }

        public void SetPreviousMenuAsCurrent()
        {
            setMenu(m_PreviousMenu);
        }
    }
}