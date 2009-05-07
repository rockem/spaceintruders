using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.comp
{
    public class Component : LoadableDrawableComponent, IComponent
    {
        private Color m_Color = Color.White;
        private ILogic m_Logic;
        private float m_Opacity;
        private Vector2 m_RotateOrigin;
        private float m_Rotation;

        public Component(Game game, int i_UpdateOrder) : base(game, i_UpdateOrder)
        {
        }

        public Component(Game game) : base(game)
        {
        }

        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                m_Opacity = value;
                UpdateOpacity();
            }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        #region IComponent Members

        public ILogic Logic
        {
            get { return m_Logic; }
            set { m_Logic = value; }
        }

        public Vector2 RotationOrigin
        {
            get { return m_RotateOrigin;  }
            set { m_RotateOrigin = value; }
        }

        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        #endregion

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Logic.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            m_Logic.Update(i_GameTime);
            base.Update(i_GameTime);
        }

        private void UpdateOpacity()
        {
            Vector4 tintColor = m_Color.ToVector4();
            tintColor.W = m_Opacity / 100f;
            m_Color = new Color(tintColor);
        }
    }
}