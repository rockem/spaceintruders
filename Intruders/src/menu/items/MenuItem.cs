using Intruders.screen;

namespace Intruders.menu.items
{
    internal abstract class MenuItem : IMenuItem
    {
        private readonly GameEventListener r_GameListener;

        protected MenuItem(GameEventListener i_Listener)
        {
            r_GameListener = i_Listener;
        }

        public string Text
        {
            get { return ""; }
        }

        #region IMenuItem Members

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

        #endregion

        protected GameEventListener GameListener()
        {
            return r_GameListener;
        }

        protected abstract string GetItemText();
    }
}