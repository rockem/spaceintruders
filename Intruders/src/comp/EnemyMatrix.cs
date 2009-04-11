using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    class EnemyMatrix : SpriteComponent
    {
        private Texture2D m_TextureEnemy1;
        private EnemyMatrixLogic r_ComponentLogic;

        public EnemyMatrix(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            m_TextureEnemy1 = Game.Content.Load<Texture2D>(@"Sprites\Enemy01");
            r_ComponentLogic =
                new EnemyMatrixLogic(
                m_TextureEnemy1.Width,
                m_TextureEnemy1.Height,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            r_ComponentLogic.Update(gameTime, null);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = new SpriteBatch(Game.GraphicsDevice);
            sb.Begin();
            foreach (SpriteAtt sprite in r_ComponentLogic.getPosition())
            {
                sb.Draw(m_TextureEnemy1, sprite.Position, sprite.Color);
            }

            sb.End();
            base.Draw(gameTime);
        }
    }
}