using Intruders.screen;

namespace Intruders.menu.items
{
    class Quit : MenuItem
    {
        public Quit(GameEventListener i_MenuManager)
            : base(i_MenuManager)
        {
        }

        protected override string GetItemText()
        {
            return "Quit";
        }

        public override void Select()
        {
            GameListener().ExitGame();
        }
    }
}