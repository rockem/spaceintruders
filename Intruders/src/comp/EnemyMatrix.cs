using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    class EnemyMatrix : SpriteComponent
    {
        private Texture2D m_TextureEnemy1;

        public EnemyMatrix(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            m_TextureEnemy1 = Game.Content.Load<Texture2D>(@"Sprites\Enemy01");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            drawEnemyRow(0, Color.Pink);
            drawEnemyRow(1, Color.LightBlue);
            drawEnemyRow(2, Color.LightBlue);
            drawEnemyRow(3, Color.Yellow);
            drawEnemyRow(4, Color.Yellow);
            base.Draw(gameTime);
        }

        private void drawEnemyRow(int i_Row, Color i_Color)
        {
            SpriteBatch sb = new SpriteBatch(Game.GraphicsDevice);
            sb.Begin();
            Vector2 currentPosition = new Vector2();
            int enemyHeight = m_TextureEnemy1.Height;
            currentPosition.Y = (float)(enemyHeight * 3 + (i_Row * (enemyHeight + enemyHeight * 0.6)));
            for (int i = 0; i < 9; i++)
            {
                int enemyWidth = m_TextureEnemy1.Width;
                currentPosition.X = (float)(i * (enemyWidth + enemyWidth * 0.6));
                sb.Draw(m_TextureEnemy1, currentPosition, i_Color);
            }
            sb.End();

        }
    }
}