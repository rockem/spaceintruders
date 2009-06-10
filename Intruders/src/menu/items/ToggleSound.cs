using Intruders.screen;

namespace Intruders.menu.items
{
    internal class ToggleSound : MenuItem
    {
        public ToggleSound(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Toggle Sound: " + (GameListener().SoundEnabled ? "On" : "Off");
        }

        public override void LowerValue()
        {
            GameListener().ToggleSound();
        }

        public override void RiseValue()
        {
            GameListener().ToggleSound();
        }
    }
}