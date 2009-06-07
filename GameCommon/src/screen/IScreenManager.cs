using Infrastructure.ObjectModel.Screens;

namespace GameCommon.screen
{
    public interface IScreensMananger
    {
        GameScreen ActiveScreen { get; }

        void SetCurrentScreen(GameScreen i_NewScreen);

        bool Remove(GameScreen i_Screen);

        void Add(GameScreen i_Screen);
    }
}