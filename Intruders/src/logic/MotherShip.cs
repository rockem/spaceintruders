using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class MotherShip : SpriteLogic
    {
        public MotherShip(IViewFactory i_Factory)
            : base(i_Factory, @"Sprites\MotherShip_32x120")
        {
            Color = Color.Red;
            Alive = false;
            Score = 500;
        }

        public override void Update(GameTime i_GameTime)
        {
            Position =
                new Vector2(Position.X + ((Width / 2) * (float)i_GameTime.ElapsedGameTime.TotalSeconds), Position.Y);

            if(Position.X >= ViewFactory.ViewWidth + Width)
            {
                Alive = false;
            }
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            ViewFactory.PlayCue("MotherShipKill");
            i_SpriteLogic.Score = Score;
            (View as ISprite).StartAnimation();
        }

        public override void AnimationEnded()
        {
            Alive = false;
        }
    }
}