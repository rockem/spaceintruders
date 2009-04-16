using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    class Monster : SpriteLogic
    {
        public Monster(IViewFactory i_Factory) : base(i_Factory)
        {
        }

        public override string GetImagePath()
        {
            return "Sprites\\Enemy01";
        }
    }
}