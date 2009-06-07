using Intruders.comp;
using Microsoft.Xna.Framework;

namespace Intruders.logic
{
    internal class DummyLogic : ILogic
    {
        #region ILogic Members

        public void Update(GameTime i_GameTime)
        {
        }

        public void Initialize()
        {
        }

        public void AnimationEnded()
        {
        }

        public IComponent View
        {
            get { return null; }
        }

        #endregion
    }
}