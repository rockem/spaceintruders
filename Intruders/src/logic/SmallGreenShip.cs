using Intruders.comp;

namespace Intruders.logic
{
    internal class SmallGreenShip : SpriteLogic
    {
        public SmallGreenShip(IViewFactory i_Factory)
            : base(i_Factory, @"Sprites\\Ship02_32x32")
        {
            ((ISprite)View).Scale = 0.5f;
        }
    }
}