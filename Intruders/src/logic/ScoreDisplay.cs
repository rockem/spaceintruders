using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    class ScoreDisplay : ILogic
    {
        private readonly IViewFactory r_Factory;
        private readonly IFontComponent r_P1Score;
        private readonly IFontComponent r_P2Score;
        private int m_P1Score;
        private int m_P2Score;

        public ScoreDisplay(IViewFactory i_ViewFactory) 
        {
            r_Factory = i_ViewFactory;
            r_P1Score = r_Factory.CreateFontComponent();
            r_P1Score.Logic = this;
            r_P2Score = r_Factory.CreateFontComponent();
            r_P2Score.Logic = this;
        }

        public void Update(GameTime i_GameTime)
        {
            
        }

        public void Initialize()
        {
            r_P1Score.Position = Vector2.Zero;
            r_P1Score.Color = Color.Red;
            r_P1Score.Text = "P1 Score: " + P1Score;
            r_P2Score.Position = new Vector2(0, r_P1Score.Height + 2);
            r_P2Score.Color = Color.Green;
            r_P2Score.Text = "P2 Score: " + P2Score;

        }

        public int P1Score
        {
            get { return m_P1Score; }
            set
            {
                m_P1Score = value;
                r_P1Score.Text = "P1 Score: " + m_P1Score;
            }
        }

        public int P2Score
        {
            get { return m_P2Score; }
            set 
            {
                m_P2Score = value;
                r_P2Score.Text = "P2 Score: " + m_P2Score;
            }
        }
    }
}