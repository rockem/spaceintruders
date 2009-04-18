using System.Collections.Generic;
using Intruders.comp;

namespace Intruders.logic
{
    public interface ISpriteLogic : ILogic
    {
        int XVelocity { get; }

        int YVelocity { get; }

        eSpriteType Type { get; set; }

        void CollidedWith(ISpriteLogic i_SpriteLogic);
    }
}