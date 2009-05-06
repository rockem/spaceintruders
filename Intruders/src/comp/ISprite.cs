using System.Collections.Generic;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public interface ISprite : IComponent
    {
        bool Enabled { get; set; }

        bool Visible { get; set; }

        int Width { get; }

        int Height { get; }

        Vector2 Position { get; set; }

        Color Color { get; set; }

        float Scale { get; set; }

        int CurrentAsset { set; }
    }
}