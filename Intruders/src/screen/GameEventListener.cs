using Intruders.menu;

namespace Intruders.screen
{
    internal interface GameEventListener
    {
        int SoundVolume { get; set; }

        int MusicVolume { get; set; }

        bool SoundEnabled { get; }

        bool FullScreenMode { get; }

        bool AllowWindowResizing { get; set; }

        bool MouseVisibilty { get; set; }

        int NumberOfPlayers { get; set; }

        void ExitCurrentScreen();

        void ExitGame();

        void GoToMenu(eMenu options);

        void GoToPreviousMenu();

        void ToggleSound();

        void ToggleFullScreen();

        void GoToPlayScreen();
    }
}