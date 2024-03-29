using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public interface IFontComponent
    {
        Vector2 PositionOfOrigin { get; set; }

        int Height { get; }

        int Width { get; }

        Color TintColor { get; set; }

        string Text { get; set; }

        ILogic Logic { get; set; }
    }
}