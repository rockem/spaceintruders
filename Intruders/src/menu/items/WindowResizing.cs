using Intruders.screen;

namespace Intruders.menu.items
{
    class WindowResizing : MenuItem
    {
        public WindowResizing(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Allow Window Resizing: " + (GameListener().AllowWindowResizing ? "On" : "Off");
        }

        public override void LowerValue()
        {
            GameListener().AllowWindowResizing = !GameListener().AllowWindowResizing;
        }

        public override void RiseValue()
        {
            GameListener().AllowWindowResizing = !GameListener().AllowWindowResizing;
        }
    }
}