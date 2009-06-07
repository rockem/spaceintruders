using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public interface ILogic
    {
        IComponent View { get; }

        void Update(GameTime i_GameTime);

        void Initialize();

        void AnimationEnded();
    }
}