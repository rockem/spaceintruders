using Intruders.comp;

namespace Intruders.logic
{
    internal class YellowMonster : Monster
    {
        private string[] m_Assets = new string[] {"Sprites\\Enemy0301", "Sprites\\Enemy0302"};

        public YellowMonster(IViewFactory i_Factory)
            : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.LightYellow;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] {"Sprites\\Enemy0301", "Sprites\\Enemy0302"};
        }
    }
}