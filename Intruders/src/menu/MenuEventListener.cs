namespace Intruders.menu
{
    interface MenuEventListener
    {
        void ExitGame();

        void ExitScreen();

        void GoToMenu(eMenu i_Menu);
    }
}