using GameCommon.manager;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.logic
{
    class BlueShip : Ship
    {
        public BlueShip(IViewFactory i_Factory) : base(i_Factory)
        {
        }

        protected override bool inputFire()
        {
            return ViewFactory.InputManager.KeyPressed(Keys.Enter) ||
                   ViewFactory.InputManager.ButtonPressed(eInputButtons.Left);
        }

        protected override bool inputLeft()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.Left);
        }


        protected override bool inputRight()
        {
            return ViewFactory.InputManager.KeyHeld(Keys.Right);
        }

        protected override void initPosition()
        {
            float currentY = ViewFactory.ViewHeight - ((ISprite)View).Height / 2 - 30;
            ((ISprite)View).Position = new Vector2(0, currentY);
        }

        protected override bool useMouseForMovement()
        {
            return true;
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Ship01_32x32");
        }
    }
}