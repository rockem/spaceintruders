using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.logic
{
    internal class GreenShip : Ship
    {
        public GreenShip(IViewFactory i_Factory)
            : base(i_Factory, @"Sprites\Ship02_32x32")
        {
        }

        protected override bool inputFire()
        {
            return ViewFactory.InputManager.KeyPressed(Keys.Space);
        }

        protected override bool inputLeft()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.A);
        }

        protected override bool inputRight()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.D);
        }

        protected override void initPosition()
        {
            float currentY = ViewFactory.ViewHeight - (((ISprite)View).HeightAfterScale / 2) - 30;
            ((ISprite)View).PositionOfOrigin = new Vector2(((ISprite)View).WidthAfterScale, currentY);
        }
    }
}