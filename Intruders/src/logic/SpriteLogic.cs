using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    [Serializable()]
    public abstract class SpriteLogic : ISpriteLogic
    {
        private readonly IViewFactory r_Factory;
        private readonly ISprite r_Sprite;
        private int m_XVelocity;
        private int m_YVelocity;
        private bool m_Alive = true;
        protected string[] m_Assets;
        private int m_CurrentAsset;

        protected SpriteLogic(IViewFactory i_Factory)
        {
            r_Factory = i_Factory;
            CreateAssets();
            r_Sprite = r_Factory.CreateSpriteComponent(m_Assets);
            r_Sprite.SpriteLogic = this;
        }

        protected virtual void CreateAssets()
        {
        }

        protected IViewFactory ViewFactory
        {
            get { return r_Factory; }
        }

        protected ISprite getSprite()
        {
            return r_Sprite;
        }

        public Color Color
        {
            get { return r_Sprite.Color; }
            set { r_Sprite.Color = value; }
        }

        public int Width
        {
            get { return r_Sprite.Width; }
        }

        public int Height
        {
            get { return r_Sprite.Height; }
        }

        public Vector2 Position
        {
            get { return r_Sprite.Position; }
            set { r_Sprite.Position = value; }
        }


        public virtual void Update(GameTime i_GameTime)
        {
        }

        public virtual void Initialize()
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

        public virtual void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
        }

        public bool Alive
        {
            get { return m_Alive; }
            set
            {
                m_Alive = value;
                getSprite().Enabled = value;
                getSprite().Visible = value;
            }
        }

        public string[] Assets
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
                r_Sprite.CurrentAsset = m_Assets[m_CurrentAsset];
            }
        }
    }
}