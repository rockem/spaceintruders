using GameCommon.manager;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public interface ISprite
    {
        bool Enabled { get; set; }

        bool Visible { get; set; }

        int Width { get; }

        int Height { get; }

        Vector2 Position { get; set; }

        Color Color { get; set; }

        ISpriteLogic SpriteLogic { get; set; }

    }
}