using Intruders.logic;
using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    public class Component : LoadableDrawableComponent, IComponent
    {
        private ILogic m_Logic;


        public Component(Game game, int i_UpdateOrder) : base(game, i_UpdateOrder)
        {
            
        }

        public ILogic Logic
        {
            get { return m_Logic; }
            set { m_Logic = value; }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Logic.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            m_Logic.Update(i_GameTime);
            base.Update(i_GameTime);
        }

        
    }
}