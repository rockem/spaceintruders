using Intruders.comp;

namespace Intruders.logic
{
    internal class BlueMonster : Monster
    {
        public BlueMonster(IViewFactory i_Factory) : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.LightBlue;
            Score = 200;
        }

        protected override void CreateAssets()
        {
            Assets = new string[] {"Sprites\\Enemy0201_32x32", "Sprites\\Enemy0202_32x32"};
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill01");
        }
    }
}