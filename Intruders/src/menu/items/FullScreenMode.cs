using Intruders.screen;

namespace Intruders.menu.items
{
    class FullScreenMode : MenuItem
    {
        public FullScreenMode(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Full Screen Mode: " + (GameListener().FullScreenMode ? "Yes" : "No");
        }

        public override void LowerValue()
        {
            GameListener().ToggleFullScreen();
        }

        public override void RiseValue()
        {
            GameListener().ToggleFullScreen();
        }

    }
}