using Intruders.comp;

namespace Intruders.logic
{
    class Wall : SpriteLogic
    {
        public Wall(IViewFactory i_Factory) : base(i_Factory)
        {
            Type = eSpriteType.Wall;
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Barrier_44x32");
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            ViewFactory.PlayCue("BarrierHit");
            if(i_SpriteLogic.Type == eSpriteType.Monster)
            {
                Alive = false;
            }
        }
    }
}