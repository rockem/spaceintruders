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
            Assets = new string[] {"Sprites\\Barrier"};
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            if(i_SpriteLogic.Type == eSpriteType.Monster)
            {
                Alive = false;
            }
        }
    }
}