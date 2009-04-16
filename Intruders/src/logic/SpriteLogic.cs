using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    public abstract class SpriteLogic : ISpriteLogic
    {
        private readonly IViewFactory r_Factory;
        private readonly ISprite r_Sprite;

        protected SpriteLogic(IViewFactory i_Factory)
        {
            r_Factory = i_Factory;
            r_Sprite = r_Factory.CreateSpriteComponent();
            r_Sprite.setComponentLogic(this);
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

        public virtual string GetImagePath()
        {
            return "";
        }
    }
}