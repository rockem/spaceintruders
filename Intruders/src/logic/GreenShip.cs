using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.logic
{
    class GreenShip : Ship
    {
        public GreenShip(IViewFactory i_Factory) : base(i_Factory)
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
            float currentY = ViewFactory.ViewHeight - ((ISprite)View).Height / 2 - 30;
            ((ISprite)View).Position = new Vector2(((ISprite)View).Width, currentY);
        }

        protected override void CreateAssets()
        {
            Assets = new string[] { "Sprites\\GreenShip" };
        }


    }
}