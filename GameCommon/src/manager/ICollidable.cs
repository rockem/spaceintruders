using System;

namespace GameCommon.manager
{
    public delegate void PositionChangedEventHandler(object i_Collidable);

    public interface ICollidable
        {
            event PositionChangedEventHandler PositionChanged;
            event EventHandler Disposed;

            bool Visible
            {
                get;
            }

            bool CheckCollision(ICollidable i_Source);
            void Collided(ICollidable i_Collidable);
    }
}