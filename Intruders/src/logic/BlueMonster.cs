using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class BlueMonster : Monster
    {
        public BlueMonster(IViewFactory i_Factory) : base(i_Factory)
        {
            Assets = new Asset("Sprites\\Enemies_96x64");
            Assets.addBounds(new Rectangle(32, 0, 32, 32));
            Assets.addBounds(new Rectangle(32, 32, 32, 32));

            Color = Color.LightBlue;
            Score = 200;
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill01");
        }
    }
}