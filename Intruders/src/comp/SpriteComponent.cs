using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    public abstract class SpriteComponent : DrawableGameComponent 
    {
        public SpriteComponent(Game game) : base(game)
        {
            Game.Components.Add(this);
        }
    }
}