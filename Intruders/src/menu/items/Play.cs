using Intruders.screen;

namespace Intruders.menu.items
{
    internal class Play : MenuItem
    {
        public Play(GameEventListener i_MenuManager)
            : base(i_MenuManager)
        {
        }

        protected override string GetItemText()
        {
            return "Play";
        }

        public override void Select()
        {
            GameListener().GoToPlayScreen();
        }
    }
}