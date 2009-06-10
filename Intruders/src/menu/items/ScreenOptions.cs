using Intruders.screen;

namespace Intruders.menu.items
{
    internal class ScreenOptions : MenuItem
    {
        public ScreenOptions(GameEventListener i_MenuManager)
            : base(i_MenuManager)
        {
        }

        protected override string GetItemText()
        {
            return "Screen Options";
        }

        public override void Select()
        {
            GameListener().GoToMenu(eMenu.ScreenOptions);
        }
    }
}