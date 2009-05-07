using GameCommon.manager;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class Wall : SpriteLogic
    {
        private const int k_WallWidth = 44;

        private int m_StartLeftPosision;
        private int m_Velocity = 50;

        public Wall(IViewFactory i_Factory) : base(i_Factory)
        {
            Type = eSpriteType.Wall;
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Barrier_374x32");
        }

        public override void Initialize()
        {
            m_StartLeftPosision = (ViewFactory.ViewWidth / 2) - (Width / 2);
            Position = new Vector2(m_StartLeftPosision, ViewFactory.ViewHeight - 64 - (Height * 2));
        }

        public override void Update(GameTime i_GameTime)
        {
            if (matrixTouchBounds())
            {
                m_Velocity *= -1;
            }

             Position = new Vector2(
                 Position.X + (m_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds),
                 Position.Y);
        }

        private bool matrixTouchBounds()
        {
            return Position.X >= m_StartLeftPosision + (k_WallWidth / 2) ||
                   Position.X <= m_StartLeftPosision - (k_WallWidth / 2);
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            if (i_SpriteLogic.Type == eSpriteType.Bullet ||
                i_SpriteLogic.Type == eSpriteType.Bomb)
            {
                ViewFactory.PlayCue("BarrierHit");
            }

            Color[] pixels = (View as ICollidable2D).GetPixelArray();
            Rectangle e = i_SpriteLogic.Bounds;
            clearPixelsOn(pixels, new Rectangle(e.Top - Bounds.Top, e.Left - Bounds.Left, e.Width, e.Height));
            (View as ISprite).Pixels = pixels;
        }

        private void clearPixelsOn(Color[] pixels, Rectangle rectangle)
        {
            for (int x = rectangle.Left; x < rectangle.Height + rectangle.Left; x++)
            {
                for (int y = rectangle.Top; y < rectangle.Width + rectangle.Top; y++)
                {
                    int offset = (x * Bounds.Width) + y;
                    if(offset < pixels.Length && offset >= 0)
                    {
                        Vector4 transparentColor = pixels[offset].ToVector4();
                        transparentColor.W = 0;
                        pixels[offset] = new Color(transparentColor);
                    }
                }
            }
        }
    }
}