using Intruders.comp;

namespace Intruders.logic
{
    class SmallGreenShip : SpriteLogic
    {
        public SmallGreenShip(IViewFactory i_Factory) : base(i_Factory)
        {
            ((ISprite)View).Scale = 0.5f;
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Ship02_32x32");
        }

    }
}