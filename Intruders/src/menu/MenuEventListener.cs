namespace Intruders.menu
{
    internal interface MenuEventListener
    {
        void ExitGame();

        void ExitScreen();

        void GoToMenu(eMenu i_Menu);
    }
}