using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class YellowMonster : Monster
    {
        public YellowMonster(IViewFactory i_Factory)
            : base(i_Factory)
        {
            Assets = new Asset("Sprites\\Enemies_96x64");
            Assets.addBounds(new Rectangle(64, 0, 32, 32));
            Assets.addBounds(new Rectangle(64, 32, 32, 32));
            Color = Color.LightYellow;
            Score = 100;
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill03");
        }
    }
}