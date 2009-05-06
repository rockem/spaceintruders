using System;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    [Serializable]
    public abstract class SpriteLogic : Logic, ISpriteLogic
    {
        private bool m_Alive = true;
        protected Asset m_Assets;
        private int m_CurrentAsset;
        private int m_Score;
        private eSpriteType m_SpriteType;
        private int m_XVelocity;
        private int m_YVelocity;

        protected SpriteLogic(IViewFactory i_Factory) : base(i_Factory)
        {
            CreateAssets();
            CreateView(ViewFactory.CreateSpriteComponent(m_Assets));
        }

        public Color Color
        {
            get { return ((ISprite)View).Color; }
            set { ((ISprite)View).Color = value; }
        }

        public int Width
        {
            get { return ((ISprite)View).Width; }
        }

        public int Height
        {
            get { return ((ISprite)View).Height; }
        }

        public Vector2 Position
        {
            get { return ((ISprite)View).Position; }
            set { ((ISprite)View).Position = value; }
        }

        public bool Alive
        {
            get { return m_Alive; }
            set
            {
                m_Alive = value;
                ((ISprite)View).Enabled = value;
                ((ISprite)View).Visible = value;
            }
        }

        public Asset Assets
        {
            get { return m_Assets; }
            set { m_Assets = value; }
        }

        protected int CurrentAsset
        {
            get { return m_CurrentAsset; }
            set
            {
                m_CurrentAsset = value;
                ((ISprite)View).CurrentAsset = m_CurrentAsset;
            }
        }

        #region ISpriteLogic Members

        public override void Update(GameTime i_GameTime)
        {
        }

        public override void Initialize()
        {
        }

        public int XVelocity
        {
            get { return m_XVelocity; }
            set { m_XVelocity = value; }
        }

        public int YVelocity
        {
            get { return m_YVelocity; }
            set { m_YVelocity = value; }
        }

        public eSpriteType Type
        {
            get { return m_SpriteType; }
            set { m_SpriteType = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public virtual void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
        }

        #endregion

        protected virtual void CreateAssets()
        {
        }
    }
}