using GameCommon.animation;
using GameCommon.manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCommon.comp
{
    public class Sprite : LoadableDrawableComponent
    {

        protected string m_AssetName;

        public string AssetName
        {
            get { return m_AssetName; }
            set { m_AssetName = value; }
        }

        protected AnimationsManager m_Animations;

        public AnimationsManager Animations
        {
            get { return m_Animations; }
            set { m_Animations = value; }
        }

        private Texture2D m_Texture;
        public Texture2D Texture
        {
            get { return m_Texture; }
            set { m_Texture = value; }
        }

        public float WidthAfterScale
        {
            get { return m_WidthBeforeScale * m_Scale; }
            set { m_WidthBeforeScale = value / m_Scale; }
        }

        public float HeightAfterScale
        {
            get { return m_HeightBeforeScale * m_Scale; }
            set { m_HeightBeforeScale = value / m_Scale; }
        }

        protected float m_WidthBeforeScale;
        public float WidthBeforeScale
        {
            get { return m_WidthBeforeScale; }
            set { m_WidthBeforeScale = value; }
        }

        protected float m_HeightBeforeScale;
        public float HeightBeforeScale
        {
            get { return m_HeightBeforeScale; }
            set { m_HeightBeforeScale = value; }
        }

        protected Vector2 m_PositionOfOrigin = Vector2.Zero;
        /// <summary>
        /// Represents the location of the sprite's origin point in screen coorinates
        /// </summary>
        public Vector2 PositionOfOrigin
        {
            get { return m_PositionOfOrigin; }
            set
            {
                if (m_PositionOfOrigin != value)
                {
                    m_PositionOfOrigin = value;
                    OnPositionChanged();
                }
            }
        }

        public Vector2 m_PositionOrigin;
        public Vector2 PositionOrigin
        {
            get { return m_PositionOrigin; }
            set { m_PositionOrigin = value; }
        }

        public Vector2 m_RotationOrigin = Vector2.Zero;
        public Vector2 RotationOrigin
        {
            get { return m_RotationOrigin; }
            set { m_RotationOrigin = value; }
        }

        public Vector2 PositionForDraw
        {
            get { return PositionOfOrigin - PositionOrigin + RotationOrigin; }
        }

        public Vector2 TopLeftPosition
        {
            get { return PositionOfOrigin - PositionOrigin; }
            set { PositionOfOrigin = value + PositionOrigin; }
        }

        public Rectangle ScreenBoundsAfterScale
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)WidthAfterScale,
                    (int)HeightAfterScale);
            }
        }

        public Rectangle ScreenBoundsBeforeScale
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)WidthBeforeScale,
                    (int)HeightBeforeScale);
            }
        }

        public virtual Rectangle ColliadbleBounds
        {
            get { return ScreenBoundsAfterScale; }
        }

        protected Rectangle m_SourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return m_SourceRectangle; }
            set
            {
                if (m_SourceRectangle != value)
                {
                    m_SourceRectangle = value;
                    WidthBeforeScale = m_SourceRectangle.Width;
                    HeightBeforeScale = m_SourceRectangle.Height;
                }
            }
        }

        public Vector2 TextureCenter
        {
            get
            {
                return new Vector2(m_Texture.Width / 2, m_Texture.Height / 2);
            }
        }

        public Vector2 SourceRectangleCenter
        {
            get { return new Vector2(m_SourceRectangle.Width / 2, m_SourceRectangle.Height / 2); }
        }

        protected float m_Rotation;
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        protected float m_Scale = 1;
        public float Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }

        protected Color m_TintColor = Color.White;
        public Color TintColor
        {
            get { return m_TintColor; }
            set
            {
                if (m_TintColor != value)
                {
                    m_TintColor = value;
                    UpdateOpacity();
                }
            }
        }

        private float m_Opacity = 100;
        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                if (m_Opacity != value)
                {
                    m_Opacity = value;
                    UpdateOpacity();
                }
            }
        }

        protected Vector2 m_Velocity = Vector2.Zero;
        /// <summary>
        /// Pixels per Second on 2 axis
        /// </summary>
        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        private float m_AngularVelocity = 0;
        /// <summary>
        /// Radians per Second on X Axis
        /// </summary>
        public float AngularVelocity
        {
            get { return m_AngularVelocity; }
            set { m_AngularVelocity = value; }
        }

        private void UpdateOpacity()
        {
            // The A component of the rgbA si a float between 0 and 1,
            // while 1 is not opacity at all

            Vector4 tintColor = m_TintColor.ToVector4();
            tintColor.W = m_Opacity / 100f;
            m_TintColor = new Color(tintColor);
        }

        public Sprite(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_Game, i_UpdateOrder, i_DrawOrder)
        {
            AssetName = i_AssetName;
        }

        public Sprite(string i_AssetName, Game i_Game, int i_CallsOrder)
            : base(i_Game, i_CallsOrder)
        {
            AssetName = i_AssetName;
        }

        public Sprite(string i_AssetName, Game i_Game)
           : base(i_Game, int.MinValue)
        {
            AssetName = i_AssetName;
        }

        protected override void LoadContent()
        {
            if (AssetName != null)
            {
                m_Texture = ContentManager.Load<Texture2D>(m_AssetName);
            }
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            m_Animations = new AnimationsManager(this);
        }

        protected override void InitBounds()
        {
            if (AssetName != null)
            {
                m_WidthBeforeScale = m_Texture.Width;
                m_HeightBeforeScale = m_Texture.Height;
                m_PositionOfOrigin = Vector2.Zero;

                InitSourceRectangle();

                InitOrigins();
            }
        }

        protected virtual void InitOrigins()
        {
        }

        protected virtual void InitSourceRectangle()
        {
            m_SourceRectangle = new Rectangle(0, 0, (int)m_WidthBeforeScale, (int)m_HeightBeforeScale);
        }

        public override void Update(GameTime gameTime)
        {
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            PositionOfOrigin += Velocity * totalSeconds;
            Rotation += AngularVelocity * totalSeconds;

            base.Update(gameTime);

            this.Animations.Animate(gameTime);
        }

        protected float m_LayerDepth;

        public float LayerDepth
        {
            get { return m_LayerDepth; }
            set { m_LayerDepth = value; }
        }

        public override void Draw(GameTime gameTime)
        {
            /*if (!m_UseSharedBatch)
            {
                SpriteBatch.Begin();
            }*/
            if (AssetName != null)
            {
                SpriteBatch.Draw(m_Texture, this.PositionForDraw,
                                 this.SourceRectangle, this.TintColor,
                                 this.Rotation, this.RotationOrigin, this.Scale,
                                 SpriteEffects.None, this.LayerDepth);
            }

            /*if (!m_UseSharedBatch)
            {
                SpriteBatch.End();
            }*/

            base.Draw(gameTime);
        }

        protected override void DrawBoundingBox()
        {
            // not implemented yet
        }

        public virtual bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;
            ICollidable2D source = i_Source as ICollidable2D;
            if (source != null)
            {
                collided = source.Bounds.Intersects(this.ScreenBoundsAfterScale);
            }

            return collided;
        }

        public virtual void Collided(ICollidable i_Collidable)
        {
            // defualt behavior - change direction:
            this.Velocity *= -1;
        }

        public Sprite ShallowClone()
        {
            return this.MemberwiseClone() as Sprite;
        }
    }
}