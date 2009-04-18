using Intruders.comp;

namespace Intruders.logic
{
    internal class PinkMonster : Monster
    {
        public PinkMonster(IViewFactory i_Factory) : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.LightPink;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] {"Sprites\\Enemy0101", "Sprites\\Enemy0102"};
        }
    }
}