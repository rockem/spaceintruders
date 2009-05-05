using Intruders.comp;

namespace Intruders.logic
{

    class SmallBlueShip : SpriteLogic  
    {
        public SmallBlueShip(IViewFactory i_Factory) : base(i_Factory)
        {
            ((ISprite)View).Scale = 0.5f;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] { "Sprites\\BlueShip" };
        }

    }
}