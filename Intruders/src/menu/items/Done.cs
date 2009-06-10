using Intruders.screen;

namespace Intruders.menu.items
{
    class Done : MenuItem
    {
        public Done(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Done";
        }

        public override void Select()
        {
            GameListener().GoToPreviousMenu();
        }
    }
}