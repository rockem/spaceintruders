using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    public class LoadableDrawableComponent : DrawableGameComponent
    {
        public LoadableDrawableComponent(Game game, int i_UpdateOrder) : base(game)
        {
            Game.Components.Add(this);
            UpdateOrder = i_UpdateOrder;
        }

        public LoadableDrawableComponent(Game game) : base(game)
        {
            Game.Components.Add(this);
            UpdateOrder = int.MaxValue;
        }


    }
}