using System;
using System.Collections.Generic;
using GameCommon.manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    public class EnemyMatrixLogic
    {
        private TimeSpan m_TimeBetweenJumps = TimeSpan.FromSeconds(0.5f);
        private const int k_NumOfColumns = 9;

        private readonly int r_Width;
        private readonly int r_Height;
        private readonly int r_ViewWidth;
        private readonly int r_ViewHeight;
        private readonly float r_JumpLength;
        private readonly List<SpriteAtt> r_Sprites = new List<SpriteAtt>();
        private TimeSpan m_TimeLeftToNextJump;
        private float m_Velocity = 1;
        


        public EnemyMatrixLogic(int i_Width, int i_Height, int i_ViewWidth, int i_ViewHeight)
        {
            r_Width = i_Width;
            r_Height = i_Height;
            r_ViewWidth = i_ViewWidth;
            r_ViewHeight = i_ViewHeight;
            m_Velocity = (float)r_Width / 2;
            initMatrix();
        }

        private void initMatrix()
        {
            addRow(0, Color.Pink);
            addRow(1, Color.LightBlue);
            addRow(2, Color.LightBlue);
            addRow(3, Color.Yellow);
            addRow(4, Color.Yellow);
        }

        private void addRow(int i_Row, Color i_Color)
        {
            float yaxis = (float)(r_Height * 3 + (i_Row * (r_Height + r_Height * 0.6)));
            for (int i = 0; i < k_NumOfColumns; i++)
            {
                SpriteAtt att = new SpriteAtt();
                att.Position = new Vector2((float)(i * (r_Width + r_Width * 0.6)), yaxis);
                att.Color = i_Color;
                r_Sprites.Add(att);
            }
        }

        public List<SpriteAtt> getPosition()
        {
            return r_Sprites;
        }

        public void Update(GameTime i_GameTime, IInputManager i_InputManager)
        {
            m_TimeLeftToNextJump -= i_GameTime.ElapsedGameTime;
            float yvel = 0;
            float tmpVelocity = m_Velocity;
            if (m_TimeLeftToNextJump.TotalSeconds <= 0)
            {
                if(matrixTouchedViewBounds())
                {
                    tmpVelocity = -tmpVelocity;
                    m_Velocity = 0;
                    yvel = (float)r_Width/2;
                    fasterTimeBetweenJumps();
                }
                foreach (SpriteAtt att in r_Sprites)
                {
                    att.Position = new Vector2(att.Position.X + m_Velocity, att.Position.Y + yvel);
                }
                resetTimeForNextJump();
                m_Velocity = tmpVelocity;
            }

        }

        private void resetTimeForNextJump()
        {
            m_TimeLeftToNextJump = m_TimeBetweenJumps;
        }

        private bool matrixTouchedViewBounds()
        {
            return r_Sprites[0].Position.X + getMatrixWidth() + m_Velocity >= r_ViewWidth ||
                   r_Sprites[0].Position.X + m_Velocity <= 0;
        }

        private void fasterTimeBetweenJumps()
        {
            m_TimeBetweenJumps = TimeSpan.FromSeconds(m_TimeBetweenJumps.TotalSeconds*0.95);
        }

        private float getMatrixWidth()
        {
            return k_NumOfColumns*r_Width + k_NumOfColumns*r_Width/2;
        }
    }
}