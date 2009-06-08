using Intruders.comp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.logic
{
    internal class ScoreDisplay : Logic
    {
        private readonly IFontComponent r_P1Score;
        private readonly IFontComponent r_P2Score;
        private int m_P1Score;
        private int m_P2Score;

        public ScoreDisplay(IViewFactory i_ViewFactory) : base(i_ViewFactory)
        {
            r_P1Score = ViewFactory.CreateFontComponent(@"Fonts\Scorefont");
            r_P1Score.Logic = this;
            r_P2Score = ViewFactory.CreateFontComponent(@"Fonts\Scorefont");
            r_P2Score.Logic = this;
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

        public override void Initialize()
        {
            r_P1Score.PositionOfOrigin = Vector2.Zero;
            r_P1Score.TintColor = Color.Red;
            r_P1Score.Text = "P1 Score: " + P1Score;
            r_P2Score.PositionOfOrigin = new Vector2(0, r_P1Score.Height + 2);
            r_P2Score.TintColor = Color.Green;
            r_P2Score.Text = "P2 Score: " + P2Score;
        }
    }
}