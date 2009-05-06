using Intruders.comp;
using Microsoft.Xna.Framework;

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
            Assets = new Asset("Sprites\\Enemies_96x64");
            Assets.addBounds(new Rectangle(32, 0, 32, 32));
            Assets.addBounds(new Rectangle(32, 32, 32, 32));
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill01");
        }
    }
}