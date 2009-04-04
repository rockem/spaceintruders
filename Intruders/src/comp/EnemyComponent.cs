using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    class EnemyComponent : DrawableGameComponent 
    {
        private Texture2D m_TextureEnemy1;

        public EnemyComponent(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}