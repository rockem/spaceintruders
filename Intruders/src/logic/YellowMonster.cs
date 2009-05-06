using Intruders.comp;
using Microsoft.Xna.Framework;

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
            Assets = new Asset("Sprites\\Enemies_96x64");
            Assets.addBounds(new Rectangle(64, 0, 32, 32));
            Assets.addBounds(new Rectangle(64, 32, 32, 32));
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill03");
        }
    }
}