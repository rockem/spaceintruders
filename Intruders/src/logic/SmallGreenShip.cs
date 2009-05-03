using Intruders.comp;

namespace Intruders.logic
{
    class SmallGreenShip : SpriteLogic
    {
        public SmallGreenShip(IViewFactory i_Factory) : base(i_Factory)
        {
            getSprite().Scale = 0.5f;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] { "Sprites\\GreenShip" };
        }

    }
}