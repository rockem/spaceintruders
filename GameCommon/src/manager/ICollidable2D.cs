using Microsoft.Xna.Framework;

namespace GameCommon.manager
{
    public interface ICollidable2D : ICollidable 
    {
        Rectangle Bounds
        {
            get;
        }
    }
}


