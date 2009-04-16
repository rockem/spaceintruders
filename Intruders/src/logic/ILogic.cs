using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    public interface ILogic
    {
        void Update(GameTime i_GameTime);

        void Initialize();
    }
}