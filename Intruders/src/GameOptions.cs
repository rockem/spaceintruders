using Microsoft.Xna.Framework;

namespace Intruders
{
    internal class GameOptions
    {
        private GraphicsDeviceManager m_DeviceManager;
        private int m_LevelNumber = 1;
        private int m_NumberOfPlayers = 1;

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

        public GraphicsDeviceManager DeviceManager
        {
            get { return m_DeviceManager; }
            set { m_DeviceManager = value; }
        }

        public static GameOptions GetInstance()
        {
            return InstanceHolder.Instance;
        }

        #region Nested type: InstanceHolder

        private static class InstanceHolder
        {
            public static readonly GameOptions Instance = new GameOptions();
        }

        #endregion
    }
}