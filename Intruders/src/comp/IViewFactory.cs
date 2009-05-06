using System.Collections.Generic;
using GameCommon.manager;

namespace Intruders.comp
{
    public interface IViewFactory
    {
        int ViewHeight { get; }

        int ViewWidth { get; }

        IInputManager InputManager { get; }

        ISprite CreateSpriteComponent(Asset i_Asset);

        IFontComponent CreateFontComponent();

        IComponent CreateComponent();

        void PlayCue(string cue);
    }
}