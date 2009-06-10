using Intruders.screen;

namespace Intruders.menu.items
{
    internal class EffectsVolume : MenuItem
    {
        public EffectsVolume(GameEventListener i_Listener) : base(i_Listener)
        {
        }

        protected override string GetItemText()
        {
            return "Sound Effect Volume: " + GameListener().SoundVolume;
        }

        public override void RiseValue()
        {
            GameListener().SoundVolume += 10;
        }

        public override void LowerValue()
        {
            GameListener().SoundVolume -= 10;
        }
    }
}