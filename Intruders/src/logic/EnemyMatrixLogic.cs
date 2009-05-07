using System;
using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public class EnemyMatrixLogic : Logic
    {
        private const int k_NumOfColumns = 9;
        private const int k_NumOfRows = 5;

        private readonly Monster[,] r_Monsters = new Monster[k_NumOfColumns, k_NumOfRows];
        private int m_EndColumn = k_NumOfColumns - 1;
        private int m_EndRow = k_NumOfRows - 1;
        private int m_NumberOfMonsters;
        private int m_StartColumn;
        private TimeSpan m_TimeBetweenJumps = TimeSpan.FromSeconds(0.5f);
        private TimeSpan m_TimeLeftToNextJump;
        private float m_Velocity = 1;

        public EnemyMatrixLogic(IViewFactory i_Factory) : base(i_Factory)
        {
            createMonsters();
            CreateView(ViewFactory.CreateComponent());
        }

        public event EventHandler MatrixChanged;

        private void createMonsters()
        {
            for(int i = 0; i < k_NumOfColumns; i++)
            {
                for(int j = 0; j < k_NumOfRows; j++)
                {
                    Monster item = createMonsterForRow(j);
                    item.MonsterHit += EnemyMatrix_MonsterHit;
                    r_Monsters[i, j] = item;
                }
            }
        }

        private Monster createMonsterForRow(int i_Row)
        {
            Monster monster;
            switch(i_Row)
            {
                case 0:
                    monster = new PinkMonster(ViewFactory);
                    break;
                case 1:
                    monster = new BlueMonster(ViewFactory);
                    break;
                case 2:
                    monster = new BlueMonster(ViewFactory);
                    monster.SwitchLook();
                    break;
                case 3:
                    monster = new YellowMonster(ViewFactory);
                    break;
                default:
                    monster = new YellowMonster(ViewFactory);
                    monster.SwitchLook();
                    break;
            }

            return monster;
        }

        private void EnemyMatrix_MonsterHit(object sender, EventArgs e)
        {
            updateMatrixBounds();
            fasterTimeBetweenJumps();
            MatrixChanged(this, EventArgs.Empty);
        }

        private void updateMatrixBounds()
        {
            if(checkForEmptyColumn(m_StartColumn))
            {
                m_StartColumn++;
            }

            if(checkForEmptyColumn(m_EndColumn))
            {
                m_EndColumn--;
            }

            if(checkForEmptyRow(m_EndRow))
            {
                m_EndRow--;
            }
        }

        private bool checkForEmptyRow(int i_Row)
        {
            bool decBound = true;
            for(int i = 0; i < k_NumOfColumns; i++)
            {
                if(r_Monsters[i, i_Row].Alive)
                {
                    decBound = false;
                    break;
                }
            }

            return decBound;
        }

        private bool checkForEmptyColumn(int i_Column)
        {
            bool decBound = true;
            for(int i = 0; i < k_NumOfRows; i++)
            {
                if(r_Monsters[i_Column, i].Alive)
                {
                    decBound = false;
                    break;
                }
            }

            return decBound;
        }

        private void initMatrix()
        {
            Monster first = r_Monsters[0, 0];
            for(int r = 0; r < k_NumOfRows; r++)
            {
                float yaxis = (float)((first.Height * 3) + (r * (first.Height + (first.Height * 0.6))));
                for(int i = 0; i < k_NumOfColumns; i++)
                {
                    Monster att = r_Monsters[i, r];
                    att.Position = new Vector2((float)(i * (att.Width + (att.Width * 0.6))), yaxis);
                }
            }
        }

        public override void Update(GameTime i_GameTime)
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
                    yvel = (float)r_Monsters[0, 0].Width / 2;
                    fasterTimeBetweenJumps();
                }

                m_NumberOfMonsters = 0;
                foreach(Monster mon in r_Monsters)
                {
                    mon.Position = new Vector2(mon.Position.X + m_Velocity, mon.Position.Y + yvel);
                    if(mon.Alive)
                    {
                        mon.SwitchLook();
                        m_NumberOfMonsters++;
                    }
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
            return r_Monsters[m_EndColumn, 0].Position.X + r_Monsters[m_EndColumn, 0].Width + m_Velocity >=
                   ViewFactory.ViewWidth ||
                   r_Monsters[m_StartColumn, 0].Position.X + m_Velocity <= 0;
        }

        private void fasterTimeBetweenJumps()
        {
            m_TimeBetweenJumps = TimeSpan.FromSeconds(m_TimeBetweenJumps.TotalSeconds * 0.95);
        }

        public override void Initialize()
        {
            initMatrix();
            m_Velocity = (float)r_Monsters[0, 0].Width / 2;
        }

        public float GetLowerBound()
        {
            return r_Monsters[0, m_EndRow].Position.Y + r_Monsters[0, m_EndRow].Height;
        }

        public int GetNumberOfMonstersAlive()
        {
            return m_NumberOfMonsters;
        }
    }
}