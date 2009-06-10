using Intruders.menu;

namespace Intruders.screen
{
    interface GameEventListener
    {
        void ExitCurrentScreen();

        void ExitGame();

        void GoToMenu(eMenu options);

        void GoToPreviousMenu();

        int SoundVolume { get; set; }

        int MusicVolume { get; set; }

        bool SoundEnabled { get; }

        bool FullScreenMode { get; }

        bool AllowWindowResizing { get; set; }

        bool MouseVisibilty { get; set; }

        int NumberOfPlayers { get; set;}

        void ToggleSound();

        void ToggleFullScreen();

    }
}