using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    class Sprite : Logic 
    {
        public Sprite(IViewFactory i_Factory) : base(i_Factory)
        {
        }

        public Color Color
        {
            get { return ((ISprite)View).Color; }
            set { ((ISprite)View).Color = value; }
        }

        public Vector2 Position
        {
            get { return ((ISprite)View).Position; }
            set { ((ISprite)View).Position = value; }
        }

        public void Update(GameTime i_GameTime)
        {
            
        }

        public void Initialize()
        {
            
        }
    }
}