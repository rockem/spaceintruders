using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public interface ISpriteLogic : ILogic
    {
        int XVelocity { get; }

        int YVelocity { get; }

        eSpriteType Type { get; set; }

        int Score { get; set; }

        void CollidedWith(ISpriteLogic i_SpriteLogic);

        Rectangle Bounds { get; }
    }
}