using GameCommon.manager;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public interface ISprite
    {
        int Width { get; }

        int Height { get; }

        Vector2 Position { get; set; }

        Color Color { get; set; }

        void setComponentLogic(ISpriteLogic i_Logic);
    }
}