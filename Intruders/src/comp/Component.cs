using GameCommon.comp;
using Intruders.logic;
using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    public class Component : Sprite, IComponent
    {
        private ILogic m_Logic = new DummyLogic();

        public Component(string i_AssetName, Game i_Game, int i_CallsOrder)
            : base(i_AssetName, i_Game, i_CallsOrder)
        {
        }

        public Component(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game)
        {
        }

        #region IComponent Members

        public ILogic Logic
        {
            get { return m_Logic; }
            set { m_Logic = value; }
        }

        #endregion

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