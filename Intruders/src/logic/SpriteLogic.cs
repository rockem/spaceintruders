using System;
using GameCommon.manager;
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

        protected SpriteLogic(IViewFactory i_Factory, string i_AssetName) : base(i_Factory)
        {
            CreateView(ViewFactory.CreateSpriteComponent(i_AssetName));
        }

        public Color Color
        {
            get { return ((ISprite)View).TintColor; }
            set { ((ISprite)View).TintColor = value; }
        }

        public float Width
        {
            get { return ((ISprite)View).WidthAfterScale; }
        }

        public float Height
        {
            get { return ((ISprite)View).HeightAfterScale; }
        }

        public Vector2 Position
        {
            get { return ((ISprite)View).PositionOfOrigin; }
            set { ((ISprite)View).PositionOfOrigin = value; }
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

        public Rectangle Bounds
        {
            get { return (View as ICollidable2D).Bounds; }
        }

        #endregion
    }
}