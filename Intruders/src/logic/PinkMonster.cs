using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class PinkMonster : Monster
    {
        public PinkMonster(IViewFactory i_Factory) : base(i_Factory)
        {
            Assets = new Asset("Sprites\\Enemies_96x64");
            Assets.addBounds(new Rectangle(0, 0, 32, 32));
            Assets.addBounds(new Rectangle(0, 32, 32, 32));
            Color = Color.LightPink;
            Score = 300;
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill02");
        }
    }
}