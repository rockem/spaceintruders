using Intruders.screen;

namespace Intruders.menu.items
{
    abstract class MenuItem : IMenuItem 
    {
        private readonly GameEventListener r_GameListener;

        protected MenuItem(GameEventListener i_Listener)
        {
            r_GameListener = i_Listener;
        }

        protected GameEventListener GameListener()
        {
            return r_GameListener;
        }

        public string Text
        {
            get { return ""; }
        }

        public virtual void Select()
        {
            
        }

        public virtual void RiseValue()
        {
        }

        public virtual void LowerValue()
        {
        }

        public string ItemText
        {
            get { return GetItemText(); }
        }

        protected abstract string GetItemText();
    }
}