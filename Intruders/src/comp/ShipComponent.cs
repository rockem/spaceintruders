using System;
using GameCommon.manager;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class ShipComponent : SpriteComponent 
    {
        private Texture2D m_TextureShip;
        private ShipLogic r_ComponentLogic;

        public ShipComponent(Game game) : base(game)
        {
        }
        
        protected override void LoadContent()
        {
            m_TextureShip = createTextureFrom(@"Sprites\Ship");
            r_ComponentLogic = 
                new ShipLogic(
                m_TextureShip.Width, 
                m_TextureShip.Height, 
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);
            base.LoadContent();
        }

        private Texture2D createTextureFrom(string i_Path)
        {
            return Game.Content.Load<Texture2D>(i_Path);
        }

        public override void Update(GameTime gameTime)
        {
            r_ComponentLogic.Update(gameTime, (IInputManager) Game.Services.GetService(typeof(IInputManager)));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = new SpriteBatch(Game.GraphicsDevice);
            sb.Begin();
            sb.Draw(m_TextureShip, r_ComponentLogic.getPosition(), Color.White);
            sb.End();
            base.Draw(gameTime);
        }


        public void setComponentLogic(ComponentLogic logic)
        {
            throw new NotImplementedException();
        }
    }
}