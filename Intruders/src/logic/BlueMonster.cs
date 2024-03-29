using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class BlueMonster : Monster
    {
        public BlueMonster(IViewFactory i_Factory)
            : base(i_Factory, @"Sprites\Enemies_96x64")
        {
            Color = Color.LightBlue;
            Score = 200;
            AddSourceRectangle(new Rectangle(32, 0, 32, 32));
            AddSourceRectangle(new Rectangle(32, 32, 32, 32));
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill01");
        }
    }
}