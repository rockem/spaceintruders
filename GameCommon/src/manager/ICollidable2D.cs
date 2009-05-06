using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCommon.manager
{
    public interface ICollidable2D : ICollidable 
    {
        Rectangle Bounds
        {
            get;
        }

        Color[] GetPixelArray();
    }
}


