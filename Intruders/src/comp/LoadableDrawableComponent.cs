using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    public class LoadableDrawableComponent : DrawableGameComponent 
    {
        public LoadableDrawableComponent(Game game) : base(game)
        {
            Game.Components.Add(this);
        }
    }
}