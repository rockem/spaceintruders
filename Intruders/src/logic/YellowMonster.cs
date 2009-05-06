using Intruders.comp;

namespace Intruders.logic
{
    internal class YellowMonster : Monster
    {
        public YellowMonster(IViewFactory i_Factory)
            : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.LightYellow;
            Score = 100;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] {"Sprites\\Enemy0301_32x32", "Sprites\\Enemy0302_32x32"};
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill03");
        }
    }
}