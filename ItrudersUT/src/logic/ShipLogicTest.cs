using System;
using GameCommon.manager;
using Intruders.comp;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework;
using Rhino.Mocks;

namespace ItrudersUT.logic
{
    [TestFixture]
    public class ShipLogicTest
    {
        private const int k_ShipYPos = 60;
        private const int k_XStartPosition = 0;
        private const int k_PxForSec = 120;

        private Ship m_Ship;
        private StubViewFactory m_Factory;

        internal class StubShipSprite : ISprite
        {
            public IInputManager m_InputManager;
            private Vector2 m_Position;

            public int Width
            {
                get { return 20; }
            }

            public int Height
            {
                get { return 20; }
            }

            public int ViewWidth
            {
                get { return 140; }
            }

            public int ViewHeight
            {
                get { return 100; }
            }

            public Vector2 Position
            {
                get { return m_Position; }
                set { m_Position = value;}
            }

            public Color Color
            {
                get { return Color.White; }
                set {  }
            }

            public IInputManager InputManager
            {
                get { return m_InputManager; }
            }

            public void setComponentLogic(ISpriteLogic i_Logic)
            {
                
            }
        }

        class StubViewFactory : IViewFactory
        {

            public ISprite m_Sprite;
            public IInputManager m_InputManager;

            public int ViewHeight
            {
                get { return 100; }
            }

            public int ViewWidth
            {
                get { return 140; }
            }

            public IInputManager InputManager
            {
                get { return m_InputManager; }
            }

            public ISprite CreateSpriteComponent()
            {
                m_Sprite = new StubShipSprite();
                return m_Sprite;
            }
        }

        [SetUp]
        public void setUp()
        {
            m_Factory = new StubViewFactory();
            m_Ship = new Ship(m_Factory);
            m_Ship.Initialize();
        }

        private void makePressKeyAndUpdate( Keys i_Key )
        {
            MockRepository mocks = new MockRepository();
            GameTime gameTime = new GameTime(new TimeSpan(), new TimeSpan(),
                                             new TimeSpan(), new TimeSpan(0, 0, 1));
            m_Factory.m_InputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(m_Factory.m_InputManager.KeyHeld(i_Key)).Return(true);
            Expect.Call(m_Factory.m_InputManager.KeyHeld(Keys.Right)).Return(false);
            Expect.Call(m_Factory.m_InputManager.KeyHeld(Keys.Left)).Return(false);
            Expect.Call(m_Factory.m_InputManager.ButtonIsDown(eInputButtons.Right)).Return(false);
            Expect.Call(m_Factory.m_InputManager.ButtonIsDown(eInputButtons.Left)).Return(false);
            mocks.ReplayAll();
            m_Ship.Update(gameTime);
        }

        [Test]
        public void testShouldBeOnLeftSideOnConstruction()
        {
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

        [Test]
        public void testShouldMoveAccordingToMouseDelta()
        {
            MockRepository mocks = new MockRepository();
            m_Factory.m_InputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(m_Factory.m_InputManager.MousePositionDelta).Return(new Vector2(10, 20));
            mocks.ReplayAll();
            m_Ship.Update(new GameTime());
            Assert.AreEqual(new Vector2(10, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

        [Test]
        public void testShouldNotMoveBeyondBoundsOnMouseMove()
        {
            MockRepository mocks = new MockRepository();
            m_Factory.m_InputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(m_Factory.m_InputManager.MousePositionDelta).Return(new Vector2(-10, 20));
            mocks.ReplayAll();
            m_Ship.Update(new GameTime());
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

        [Test]
        public void testShouldShouldMoveLeftOnUserLeft()
        {
            makePressKeyAndUpdate(Keys.Right);
            makePressKeyAndUpdate(Keys.Left);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

        [Test]
        public void testShouldShouldMoveRightOnUserRight()
        {
            makePressKeyAndUpdate(Keys.Right);
            Assert.AreEqual(new Vector2(k_PxForSec, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

        [Test]
        public void testShouldStopOnViewLeftBounds()
        {
            makePressKeyAndUpdate(Keys.Left);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

        [Test]
        public void testShouldStopOnViewRightBounds()
        {
            makePressKeyAndUpdate(Keys.Right);
            makePressKeyAndUpdate(Keys.Right);
            Assert.AreEqual(new Vector2(k_PxForSec, k_ShipYPos), m_Factory.m_Sprite.Position);
        }

    }

    
}