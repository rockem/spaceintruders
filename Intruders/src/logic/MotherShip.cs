using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    class MotherShip : SpriteLogic 
    {
        public MotherShip(IViewFactory i_Factory) : base(i_Factory)
        {
            Color = Microsoft.Xna.Framework.Graphics.Color.Red;
            Alive = false;
            Score = 500;

        }

        public override void Update(GameTime i_GameTime)
        {
            Position = 
                new Vector2(Position.X + (float)Width / 2 * (float)i_GameTime.ElapsedGameTime.TotalSeconds, Position.Y);

            if(Position.X >= ViewFactory.ViewWidth + Width)
            {
                Alive = false;
            }
        }


        protected override void CreateAssets()
        {
            Assets = new Asset("Sprites\\MotherShip_32x120");
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            ViewFactory.PlayCue("MotherShipKill");
            i_SpriteLogic.Score = Score;
            Alive = false;
        }
    }
}