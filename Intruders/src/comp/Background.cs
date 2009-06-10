using GameCommon.comp;
using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    internal class Background : Sprite
    {
        public Background(Game i_Game)
            : base(@"Sprites\BG_Space01_1024x768", i_Game)
        {
            DrawOrder = int.MinValue;
        }
    }
}