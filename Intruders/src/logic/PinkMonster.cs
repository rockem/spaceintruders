using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class PinkMonster : Monster
    {
        public PinkMonster(IViewFactory i_Factory)
            : base(i_Factory, @"Sprites\Enemies_96x64")
        {
            Color = Color.LightPink;
            Score = 300;
            AddSourceRectangle(new Rectangle(0, 0, 32, 32));
            AddSourceRectangle(new Rectangle(0, 32, 32, 32));
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill02");
        }
    }
}