using System;
using GameCommon.manager;
using Intruders.comp;
using Intruders.logic;
using ItrudersUT.comp.mock;
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
        private const int k_ShipSize = 20;

        private Ship m_Ship;
        private MockViewFactory m_Factory;

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
                m_Sprite = new MockSprite(k_ShipSize, k_ShipSize);
                return m_Sprite;
            }
        }

        [SetUp]
        public void setUp()
        {
            m_Factory = new MockViewFactory(140, 100, k_ShipSize, k_ShipSize);
            m_Ship = new Ship(m_Factory);
            m_Ship.Initialize();
        }

        private void holdKeyAndUpdate( Keys i_Key )
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

        private void pressKeyAndUpdate(Keys i_Key)
        {
            MockRepository mocks = new MockRepository();
            GameTime gameTime = new GameTime(new TimeSpan(), new TimeSpan(),
                                             new TimeSpan(), new TimeSpan(0, 0, 1));
            m_Factory.m_InputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(m_Factory.m_InputManager.KeyPressed(i_Key)).Return(true);
            mocks.ReplayAll();
            m_Ship.Update(gameTime);
        }

        [Test]
        public void testShouldBeOnLeftSideOnConstruction()
        {
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldMoveAccordingToMouseDelta()
        {
            MockRepository mocks = new MockRepository();
            m_Factory.m_InputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(m_Factory.m_InputManager.MousePositionDelta).Return(new Vector2(10, 20));
            mocks.ReplayAll();
            m_Ship.Update(new GameTime());
            Assert.AreEqual(new Vector2(10, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldNotMoveBeyondBoundsOnMouseMove()
        {
            MockRepository mocks = new MockRepository();
            m_Factory.m_InputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(m_Factory.m_InputManager.MousePositionDelta).Return(new Vector2(-10, 20));
            mocks.ReplayAll();
            m_Ship.Update(new GameTime());
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldShouldMoveLeftOnUserLeft()
        {
            holdKeyAndUpdate(Keys.Right);
            holdKeyAndUpdate(Keys.Left);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldShouldMoveRightOnUserRight()
        {
            holdKeyAndUpdate(Keys.Right);
            Assert.AreEqual(new Vector2(k_PxForSec, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldStopOnViewLeftBounds()
        {
            holdKeyAndUpdate(Keys.Left);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldStopOnViewRightBounds()
        {
            holdKeyAndUpdate(Keys.Right);
            holdKeyAndUpdate(Keys.Right);
            Assert.AreEqual(new Vector2(k_PxForSec, k_ShipYPos), m_Factory.r_Sprites[0].Position);
        }

        [Test]
        public void testShouldCreateBulletOnInitialize()
        {
            Assert.AreEqual(typeof(Bullet), m_Factory.r_Sprites[1].SpriteLogic.GetType());
            Assert.AreEqual(0, m_Factory.r_Sprites[1].SpriteLogic.XVelocity);
            Assert.AreEqual(-200, m_Factory.r_Sprites[1].SpriteLogic.YVelocity);
            Assert.AreEqual(false, m_Factory.r_Sprites[1].Enabled);
            Assert.AreEqual(false, m_Factory.r_Sprites[1].Visible);
        }

        [Test]
        public void testShouldEnableBulletOnEnterPress()
        {
            pressKeyAndUpdate(Keys.Enter);
            Assert.AreEqual(
                new Vector2(k_XStartPosition + k_ShipSize / 2 - m_Factory.r_Sprites[1].Width / 2,
                k_ShipYPos - m_Factory.r_Sprites[1].Height), m_Factory.r_Sprites[1].Position);
            Assert.AreEqual(true, m_Factory.r_Sprites[1].Enabled);
            Assert.AreEqual(true, m_Factory.r_Sprites[1].Visible);
        }
    }

    
}