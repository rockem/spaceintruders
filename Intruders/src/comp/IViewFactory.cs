using GameCommon.manager;

namespace Intruders.comp
{
    public interface IViewFactory
    {
        int ViewHeight { get;}

        int ViewWidth { get; }

        IInputManager InputManager { get; }

        ISprite CreateSpriteComponent();
    }
}