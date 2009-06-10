namespace Intruders
{
    class GameOptions
    {
        private int m_NumberOfPlayers = 1;
        private int m_LevelNumber = 1;

        public int NumberOfPlayers
        {
            get { return m_NumberOfPlayers; }
            set { m_NumberOfPlayers = value; }
        }

        public int CurrentLevelNumber
        {
            get { return m_LevelNumber; }
            set { m_LevelNumber = value; }
        }

        public static GameOptions GetInstance()
        {
            return InstanceHolder.Instance;
        }

        private static class InstanceHolder
        {
            public static readonly GameOptions Instance = new GameOptions();
        }
    }
}