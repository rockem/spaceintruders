using Intruders.comp;

namespace Intruders.logic
{
    internal class BlueMonster : Monster
    {
        public BlueMonster(IViewFactory i_Factory) : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.LightBlue;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] {"Sprites\\Enemy0201", "Sprites\\Enemy0202"};
        }
    }
}