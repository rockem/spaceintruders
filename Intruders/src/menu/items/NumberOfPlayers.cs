using Intruders.screen;

namespace Intruders.menu.items
{
    internal class NumberOfPlayers : MenuItem
    {
        private int m_NumPlayers = 1;

        public NumberOfPlayers(GameEventListener i_Manager)
            : base(i_Manager)
        {
        }

        protected override string GetItemText()
        {
            return "Players: " + (GameListener().NumberOfPlayers == 1 ? "One" : "Two");
        }

        public override void LowerValue()
        {
            toggleNumberOfPlayers();
        }

        private void toggleNumberOfPlayers()
        {
            if(GameListener().NumberOfPlayers == 1)
            {
                GameListener().NumberOfPlayers = 2;
            }
            else
            {
                GameListener().NumberOfPlayers = 1;
            }
        }

        public override void RiseValue()
        {
            toggleNumberOfPlayers();
        }
    }
}