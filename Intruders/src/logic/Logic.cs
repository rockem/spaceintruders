using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public abstract class Logic : ILogic
    {
        private readonly IViewFactory r_ViewFactory;
        private IComponent r_View;

        protected Logic(IViewFactory i_Factory)
        {
            r_ViewFactory = i_Factory;
        }

        protected IViewFactory ViewFactory
        {
            get { return r_ViewFactory; }
        }

        protected IComponent View
        {
            get { return r_View; }
        }

        #region ILogic Members

        public virtual void Update(GameTime i_GameTime)
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void AnimationEnded()
        {
        }

        #endregion

        protected void CreateView(IComponent m_View)
        {
            r_View = m_View;
            r_View.Logic = this;
        }
    }
}