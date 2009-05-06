using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    internal class PinkMonster : Monster
    {
        public PinkMonster(IViewFactory i_Factory) : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.LightPink;
            Score = 300;
        }

        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\Enemies_96x64");
            Assets.addBounds(new Rectangle(0, 0, 32, 32));
            Assets.addBounds(new Rectangle(0, 32, 32, 32));
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill02");
        }

    }
}