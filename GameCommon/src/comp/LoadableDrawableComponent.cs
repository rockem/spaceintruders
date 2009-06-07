using GameCommon.manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameCommon.comp
{
    public abstract class LoadableDrawableComponent : DrawableGameComponent
    {
        private SpriteBatch m_SpriteBatch;
        private bool m_UseSharedBatch = true;

        public SpriteBatch SpriteBatch
        {
            set
            {
                m_SpriteBatch = value;
                m_UseSharedBatch = true;
            }
            get
            {
                return m_SpriteBatch;
            }
        }

        public event PositionChangedEventHandler PositionChanged;
        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this);
            }
        }

        protected ContentManager ContentManager
        {
            get { return Game.Content; }
        }

        protected LoadableDrawableComponent(Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_Game)
        {
            UpdateOrder = i_UpdateOrder;
            DrawOrder = i_DrawOrder;
        }

        protected LoadableDrawableComponent(Game i_Game, int i_CallsOrder)
            : this(i_Game, i_CallsOrder, i_CallsOrder)
        { }

        public override void Initialize()
        {
            base.Initialize();

            if (this is ICollidable)
            {
                ICollisionsManager collisionMgr =
                    this.Game.Services.GetService(typeof(ICollisionsManager))
                    as ICollisionsManager;

                if (collisionMgr != null)
                {
                    collisionMgr.AddObjectToMonitor(this as ICollidable);
                }
            }

            /*if (m_SpriteBatch == null)
            {
                m_SpriteBatch =
                    Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(GraphicsDevice);
                }
            }*/

            // After everything is loaded and initialzied,
            // lets init graphical aspects:
            InitBounds();   // a call to an abstract method;
        }

#if DEBUG
        protected bool m_ShowBoundingBox = true;
        
#else
        protected bool m_ShowBoundingBox = false;
#endif

        public bool ShowBoundingBox
        {
            get { return m_ShowBoundingBox; }
            set { m_ShowBoundingBox = value; }
        }

        protected abstract void InitBounds();

        public override void Draw(GameTime gameTime)
        {
            DrawBoundingBox();
            base.Draw(gameTime);
        }

        protected virtual void DrawBoundingBox()
        {
            
        }
    }
}