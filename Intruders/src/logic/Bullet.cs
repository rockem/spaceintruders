using System;
using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public class Bullet : SpriteLogic
    {
        public event EventHandler BulletHit; 

        public Bullet(IViewFactory i_Factory, eSpriteType i_Type) : base(i_Factory)
        {
            Alive = false;
            Type = i_Type;
            BulletHit += Bullet_Dummy;
        }

        private void Bullet_Dummy(object sender, EventArgs e)
        {
            
        }

        public override void Update(GameTime i_GameTime)
        {
            Position = new Vector2(Position.X, Position.Y + YVelocity * (float) i_GameTime.ElapsedGameTime.TotalSeconds);
            if(Position.Y <= 0)
            {
                Alive = false;
            }
            base.Update(i_GameTime);
        }

        public override void CollidedWith(ISpriteLogic i_SpriteLogic)
        {
            if ((Type == eSpriteType.Bullet && i_SpriteLogic.Type == eSpriteType.Monster) ||
                Type == eSpriteType.Bullet && i_SpriteLogic.Type == eSpriteType.Bomb ||
                i_SpriteLogic.Type == eSpriteType.Wall)
            {
                Alive = false;
                Score = i_SpriteLogic.Score;
                BulletHit(this, EventArgs.Empty);
            }
        }

        protected override void CreateAssets()
        {
            Assets = new string[] {"Sprites\\Bullet"};
        }
    }
}