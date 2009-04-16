using System;
using System.Collections.Generic;
using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    public class EnemyMatrixLogic : ILogic
    {
        private const int k_NumOfColumns = 9;

        private readonly List<Monster> r_Sprites = new List<Monster>();
        private TimeSpan m_TimeBetweenJumps = TimeSpan.FromSeconds(0.5f);
        private TimeSpan m_TimeLeftToNextJump;
        private float m_Velocity = 1;
        private readonly IViewFactory r_Factory;


        public EnemyMatrixLogic(IViewFactory i_Factory)
        {
            r_Factory = i_Factory;
            createMonsters();
        }

        private void createMonsters()
        {
            for(int i = 0; i < k_NumOfColumns * 5; i++)
            {
                r_Sprites.Add(new Monster(r_Factory));
            }
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
            Monster first = r_Sprites[0];
            float yaxis = (float)(first.Height * 3 + (i_Row * (first.Height + first.Height * 0.6)));
            for(int i = 0; i < k_NumOfColumns; i++)
            {
                Monster att = r_Sprites[i_Row * k_NumOfColumns + i];
                att.Position = new Vector2((float)(i * (att.Width + att.Width * 0.6)), yaxis);
                att.Color = i_Color;
            }
        }

        public void Update(GameTime i_GameTime)
        {
            m_TimeLeftToNextJump -= i_GameTime.ElapsedGameTime;
            float yvel = 0;
            float tmpVelocity = m_Velocity;
            if(m_TimeLeftToNextJump.TotalSeconds <= 0)
            {
                if(matrixTouchedViewBounds())
                {
                    tmpVelocity = -tmpVelocity;
                    m_Velocity = 0;
                    yvel = (float)r_Sprites[0].Width / 2;
                    fasterTimeBetweenJumps();
                }
                foreach(Monster att in r_Sprites)
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
            return r_Sprites[0].Position.X + getMatrixWidth() + m_Velocity >= r_Factory.ViewWidth ||
                   r_Sprites[0].Position.X + m_Velocity <= 0;
        }

        private void fasterTimeBetweenJumps()
        {
            m_TimeBetweenJumps = TimeSpan.FromSeconds(m_TimeBetweenJumps.TotalSeconds * 0.95);
        }

        private float getMatrixWidth()
        {
            return k_NumOfColumns * r_Sprites[0].Width + k_NumOfColumns * r_Sprites[0].Width / 2;
        }

        public void Initialize()
        {
            initMatrix();
            m_Velocity = (float)r_Sprites[0].Width / 2;
        }
    }
}