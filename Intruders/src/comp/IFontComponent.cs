using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public interface IFontComponent
    {
        Vector2 Position { get; set; }

        int Height { get; }

        Color Color { get; set; }

        string Text { get; set; }

        ILogic Logic { get; set; }
    }
}