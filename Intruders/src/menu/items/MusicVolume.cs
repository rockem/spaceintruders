using Intruders.screen;

namespace Intruders.menu.items
{
    internal class MusicVolume : MenuItem
    {
        public MusicVolume(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Background Music Volume: " + GameListener().MusicVolume;
        }

        public override void RiseValue()
        {
            GameListener().MusicVolume += 10;
        }

        public override void LowerValue()
        {
            GameListener().MusicVolume -= 10;
        }
    }
}