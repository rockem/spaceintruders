using Intruders.comp;

namespace Intruders.logic
{
    internal class SmallBlueShip : SpriteLogic
    {
        public SmallBlueShip(IViewFactory i_Factory) : base(i_Factory)
        {
            Assets = new Asset("Sprites\\Ship01_32x32");
            ((ISprite)View).Scale = 0.5f;
        }
    }
}