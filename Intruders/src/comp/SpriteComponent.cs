using System.Collections.Generic;
using GameCommon.manager;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class SpriteComponent : Component, ISprite, ICollidable2D
    {
        private readonly Asset r_Assets;
        private int m_CurrentAsset;
        private Vector2 m_Position;
        private float m_Scale = 1;
        private Texture2D r_Texture;
        private Color[] m_Pixels;
        private bool m_ShouldUpdateData;

        public SpriteComponent(Asset i_Assets, Game game, int i_UpdateOrder) : base(game, i_UpdateOrder)
        {
            m_CurrentAsset = 0;
            r_Assets = i_Assets;
        }

        #region ICollidable2D Members

        public event PositionChangedEventHandler PositionChanged;

        public bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            ICollidable2D source = i_Source as ICollidable2D;

            if(source != null && source.Visible)
            {
                collided = source.Bounds.Intersects(Bounds);
            }

            return collided;
        }

        public void Collided(ICollidable i_Collidable)
        {
            ((ISpriteLogic)Logic).CollidedWith((ISpriteLogic)((IComponent)i_Collidable).Logic);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(Width * Scale), (int)(Height * Scale)); }
        }

        public Color[] GetPixelArray()
        {
            return m_Pixels;
        }

        private Rectangle currentAssetBounds()
        {
            return r_Assets.GetBoundsAt(m_CurrentAsset);
        }

        #endregion

        #region ISprite Members

        public float Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }

        public int CurrentAsset
        {
            set { m_CurrentAsset = value; }
        }

        public int Width
        {
            get { return (int)(currentAssetBounds().Width * m_Scale); }
        }

        public int Height
        {
            get { return (int)(currentAssetBounds().Height * m_Scale); }
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                m_Position = value;
                OnPositionChanged();
            }
        }

        #endregion

        protected virtual void OnPositionChanged()
        {
            if(PositionChanged != null)
            {
                PositionChanged(this);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            ICollisionsManager collisionMgr =
                Game.Services.GetService(typeof(ICollisionsManager))
                as ICollisionsManager;

            if(collisionMgr != null)
            {
                collisionMgr.AddObjectToMonitor(this);
            }
        }

        protected override void LoadContent()
        {
            r_Texture = Game.Content.Load<Texture2D>(r_Assets.GetAssetName());
            r_Assets.addBounds(new Rectangle(0, 0, r_Texture.Width, r_Texture.Height));
            m_Pixels = new Color[r_Texture.Width * r_Texture.Height];
            r_Texture.GetData(m_Pixels);

            base.LoadContent();
            Logic.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(m_ShouldUpdateData)
            {
                r_Texture.SetData(m_Pixels);
                m_ShouldUpdateData = false;
            }
            Logic.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sb.Draw(
                r_Texture, 
                Position, 
                r_Assets.GetBoundsAt(m_CurrentAsset), 
                Color, 
                0, 
                Vector2.Zero, 
                m_Scale,
                SpriteEffects.None, 
                0);
            base.Draw(gameTime);
        }

        public Color[] Pixels
        {
            get { return m_Pixels; }
            set
            {
                m_Pixels = value;
                m_ShouldUpdateData = true;
            }
        }
    }
}