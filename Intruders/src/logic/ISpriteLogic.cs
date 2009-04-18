using System.Collections.Generic;

namespace Intruders.logic
{
    public interface ISpriteLogic : ILogic
    {
        int XVelocity { get; }

        int YVelocity { get; }

        void CollidedWith(ISpriteLogic i_SpriteLogic);
    }
}