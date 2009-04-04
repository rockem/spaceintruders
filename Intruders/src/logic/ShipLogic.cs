using System;
using GameCommon.manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intruders.logic
{
    public class ShipLogic
    {

        private const int k_PxForSecond = 120;

        private int m_Width;
        private int m_ViewHeight;
        private int m_Height;
        private int m_ViewWidth;
        private float m_CurrentX;
        private float m_CurrentY;

        public ShipLogic(int i_Width, int i_Height, int i_ViewWidth, int i_ViewHeight)
        {
            m_Width = i_Width;
            m_Height = i_Height;
            m_ViewWidth = i_ViewWidth;
            m_ViewHeight = i_ViewHeight;
            setInitialPosition();
        }

        private void setInitialPosition()
        {
            m_CurrentX = 0;
            m_CurrentY = m_ViewHeight - m_Height / 2 - 30;
        }

        public void Update(GameTime time, IInputManager manager)
        {
            int velocity = 0;

            if(manager.KeyHeld(Keys.Right) || manager.ButtonIsDown(eInputButtons.Right))
            {
                velocity = k_PxForSecond;
            } 
            if(manager.KeyHeld(Keys.Left) || manager.ButtonIsDown(eInputButtons.Left))
            {
                velocity = -k_PxForSecond;
            }

            m_CurrentX += velocity * (float)time.ElapsedGameTime.TotalSeconds;
            m_CurrentX += manager.MousePositionDelta.X;
            m_CurrentX = MathHelper.Clamp(m_CurrentX, 0, m_ViewWidth - m_Width);
        }

        public Vector2 getPosition()
        {
            return new Vector2(m_CurrentX, m_CurrentY);
        }
    }
}