using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public interface ISprite : IComponent
    {
        float WidthAfterScale { get; }

        float HeightAfterScale { get; }

        bool Enabled { get; set; }

        bool Visible { get; set; }

        Vector2 PositionOfOrigin { get; set; }

        Color Color { get; set; }

        float Scale { get; set; }

        int CurrentAsset { set; }

        Color[] Pixels { get; set; }

        Rectangle SourceRectangle { get; set; }

        void StartAnimation();
    }
}