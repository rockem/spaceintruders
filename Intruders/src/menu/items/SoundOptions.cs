using Intruders.screen;

namespace Intruders.menu.items
{
    internal class SoundOptions : MenuItem
    {
        public SoundOptions(GameEventListener i_MenuManager)
            : base(i_MenuManager)
        {
        }

        protected override string GetItemText()
        {
            return "Sound Options";
        }

        public override void Select()
        {
            GameListener().GoToMenu(eMenu.SoundOptions);
        }
    }
}