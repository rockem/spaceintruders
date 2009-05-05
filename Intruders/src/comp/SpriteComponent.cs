using System.Collections.Generic;
using GameCommon.manager;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class SpriteComponent : Component, ISprite, ICollidable2D
    {
        private readonly Dictionary<string, Texture2D> r_Textures = new Dictionary<string, Texture2D>();
        private Vector2 m_Position;
        private Color m_Color = Color.White;
        private float m_Scale = 1;
        private string m_CurrentAsset;
        private readonly string[] r_Assets;

        public SpriteComponent(string[] i_Assets, Game game, int i_UpdateOrder) : base(game, i_UpdateOrder)
        {
            m_CurrentAsset = i_Assets[0];
            r_Assets = i_Assets;
        }

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
            foreach(string asset in r_Assets)
            {
                r_Textures.Add(asset, Game.Content.Load<Texture2D>(asset));
            }
            // m_TextureShip = Game.Content.Load<Texture2D>(r_AssetPath);
            base.LoadContent();
            Logic.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Logic.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = (SpriteBatch) Game.Services.GetService(typeof(SpriteBatch));
            sb.Draw(r_Textures[m_CurrentAsset], Position, null, m_Color, 0, Vector2.Zero, m_Scale, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }


        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public float Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }

        public string CurrentAsset
        {
            set { m_CurrentAsset = value; }
        }

        public int Width
        {
            get { return (int) (r_Textures[m_CurrentAsset].Width * m_Scale); }
        }

        public int Height
        {
            get { return (int) (r_Textures[m_CurrentAsset].Height * m_Scale); }
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

        public Rectangle Bounds
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, (int) (Width * Scale), (int) (Height * Scale)); }
        }
    }
}