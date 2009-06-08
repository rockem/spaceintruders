using GameCommon.input;

namespace Intruders.comp
{
    public interface IViewFactory
    {
        int ViewHeight { get; }

        int ViewWidth { get; }

        IInputManager InputManager { get; }

        ISprite CreateSpriteComponent(string i_AssetName);

        IFontComponent CreateFontComponent(string i_FontName);

        IComponent CreateComponent(string i_AssetName);

        void PlayCue(string cue);
    }
}