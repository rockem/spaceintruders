using GameCommon.manager;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.logic
{
    public class Ship : SpriteLogic
    {
        private const int k_PxForSecond = 120;

        public Ship(IViewFactory i_Factory) : base(i_Factory)
        {
        }

        public override string GetImagePath()
        {
            return "Sprites\\Ship";
        }

        public override void Update(GameTime time)
        {
            int velocity = 0;
            float currentX = getSprite().Position.X;

            if (ViewFactory.InputManager.KeyHeld(Keys.Right) || ViewFactory.InputManager.ButtonIsDown(eInputButtons.Right))
            {
                velocity = k_PxForSecond;
            }
            if (ViewFactory.InputManager.KeyHeld(Keys.Left) || ViewFactory.InputManager.ButtonIsDown(eInputButtons.Left))
            {
                velocity = -k_PxForSecond;
            }

            currentX += velocity * (float)time.ElapsedGameTime.TotalSeconds;
            currentX += ViewFactory.InputManager.MousePositionDelta.X;
            currentX = MathHelper.Clamp(currentX, 0, ViewFactory.ViewWidth - getSprite().Width);
            getSprite().Position = new Vector2(currentX, getSprite().Position.Y);
        }

        public override void Initialize()
        {
            float currentX = 0;
            float currentY = ViewFactory.ViewHeight - getSprite().Height / 2 - 30;
            getSprite().Position = new Vector2(currentX, currentY);
        }
    }
}