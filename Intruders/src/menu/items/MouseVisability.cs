using Intruders.screen;

namespace Intruders.menu.items
{
    internal class MouseVisability : MenuItem
    {
        public MouseVisability(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Mouse Visability: " + (GameListener().MouseVisibilty ? "Visible" : "Invisible");
        }

        public override void LowerValue()
        {
            GameListener().MouseVisibilty = !GameListener().MouseVisibilty;
        }

        public override void RiseValue()
        {
            GameListener().MouseVisibilty = !GameListener().MouseVisibilty;
        }
    }
}