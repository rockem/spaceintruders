using GameCommon.comp;
using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    internal class BackgroundSprite : Sprite
    {
        public BackgroundSprite(Game i_Game, int i_Opacity)
            : base("", i_Game)
        {
            Opacity = i_Opacity;
            DrawOrder = int.MinValue;
        }
    }
}