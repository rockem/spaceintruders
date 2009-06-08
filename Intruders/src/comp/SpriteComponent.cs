using System;
using GameCommon.comp;
using GameCommon.manager;
using Intruders.comp.animation;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class SpriteComponent : Component, ISprite, ICollidable2D
    {
        private int m_CurrentAsset;
        private FadeOutAnimation m_FadingAnimation;
        private Color[] m_Pixels;
        private RotateAnimation m_RotatingAnimation;
        private bool m_ShouldUpdateData;

        public SpriteComponent(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {
        }

        #region ICollidable2D Members

        public override void Collided(ICollidable i_Collidable)
        {
            ((ISpriteLogic)Logic).CollidedWith((ISpriteLogic)((IComponent)i_Collidable).Logic);
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)PositionOfOrigin.X, (int)PositionOfOrigin.Y,
                                     (int)(((Sprite)this).WidthAfterScale), (int)(((Sprite)this).HeightAfterScale));
            }
        }

        public Color[] GetPixelArray()
        {
            return m_Pixels;
        }

        #endregion

        #region ISprite Members

        public float WidthAfterScale
        {
            get { return ((Sprite)this).WidthAfterScale; }
        }

        public float HeightAfterScale
        {
            get { return ((Sprite)this).HeightAfterScale; }
        }

        public int CurrentAsset
        {
            set { m_CurrentAsset = value; }
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

        public void StartAnimation()
        {
            m_FadingAnimation.Enabled = true;
            m_RotatingAnimation.Enabled = true;
        }

        #endregion

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

            m_FadingAnimation = new FadeOutAnimation(this);
            m_FadingAnimation.AnimationFinished += FadingAnimation_AnimationFinished;

            m_RotatingAnimation = new RotateAnimation(this);
            m_RotatingAnimation.AnimationFinished += RotatingAnimation_AnimationFinished;
            Logic.Initialize();
        }

        private void RotatingAnimation_AnimationFinished(object sender, EventArgs e)
        {
        }

        private void FadingAnimation_AnimationFinished(object sender, EventArgs e)
        {
            Logic.AnimationEnded();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            // r_Texture = Game.Content.Load<Texture2D>(m_Asset.GetAssetName());
            // m_Asset.addBounds(new Rectangle(0, 0, r_Texture.WidthAfterScale, r_Texture.HeightAfterScale));
            m_Pixels = new Color[Texture.Width * Texture.Height];
            Texture.GetData(m_Pixels);
        }

        public override void Update(GameTime gameTime)
        {
            if(m_ShouldUpdateData)
            {
                Texture.SetData(m_Pixels);
                m_ShouldUpdateData = false;
            }

            m_FadingAnimation.Animate(gameTime);
            m_RotatingAnimation.Animate(gameTime);

            Logic.Update(gameTime);
            base.Update(gameTime);
        }

        /*public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sb.Draw(
                r_Texture, 
                PositionOfOrigin, 
                m_Asset.GetBoundsAt(m_CurrentAsset), 
                TintColor, 
                Rotation, 
                Vector2.Zero, 
                m_Scale,
                SpriteEffects.None, 
                0);
            base.Draw(gameTime);
        }*/
    }
}