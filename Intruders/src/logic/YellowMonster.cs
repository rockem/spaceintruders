using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class YellowMonster : Monster
    {
        public YellowMonster(IViewFactory i_Factory)
            : base(i_Factory, @"Sprites\Enemies_96x64")
        {
            Color = Color.LightYellow;
            Score = 100;
            AddSourceRectangle(new Rectangle(64, 0, 32, 32));
            AddSourceRectangle(new Rectangle(64, 32, 32, 32));
        }

        protected override void PlayKillCue()
        {
            ViewFactory.PlayCue("EnemyKill03");
        }
    }
}