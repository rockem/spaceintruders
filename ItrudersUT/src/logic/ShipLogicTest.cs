using System;
using GameCommon.manager;
using Intruders.logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework;
using Rhino.Mocks;

namespace ItrudersUT.logic
{
    [TestFixture]
    public class ShipLogicTest
    {
        private const int k_ShipYPos = 60;
        private ShipLogic sl;
        private const int k_XStartPosition = 0;
        private const int k_PxForSec = 120;

        [SetUp]
        public void setUp()
        {
            sl = new ShipLogic(20, 20, 140, 100);
        }

        [Test]
        public void testShouldBeOnLeftSideOnConstruction()
        {
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), sl.getPosition());
        }


        [Test]
        public void testShouldShouldMoveRightOnUserRight()
        {
            makePressKeyAndUpdate(Keys.Right);
            Assert.AreEqual(new Vector2(k_PxForSec, k_ShipYPos), sl.getPosition());
        }

        private void makePressKeyAndUpdate(Keys i_Key)
        {
            MockRepository mocks = new MockRepository();
            GameTime gameTime = new GameTime(new TimeSpan(), new TimeSpan(),
                                             new TimeSpan(), new TimeSpan(k_XStartPosition, k_XStartPosition, 1));
            IInputManager inputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(inputManager.KeyHeld(i_Key)).Return(true);
            Expect.Call(inputManager.KeyHeld(Keys.Right)).Return(false);
            Expect.Call(inputManager.KeyHeld(Keys.Left)).Return(false);
            Expect.Call(inputManager.ButtonIsDown(eInputButtons.Right)).Return(false);
            Expect.Call(inputManager.ButtonIsDown(eInputButtons.Left)).Return(false);
            mocks.ReplayAll();
            sl.Update(gameTime, inputManager);
        }

        [Test]
        public void testShouldShouldMoveRightOnUserLeft()
        {
            makePressKeyAndUpdate(Keys.Right);
            makePressKeyAndUpdate(Keys.Left);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), sl.getPosition());
        }

        [Test]
        public void testShouldStopOnViewLeftBounds()
        {
            makePressKeyAndUpdate(Keys.Left);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), sl.getPosition());
        }

        [Test]
        public void testShouldStopOnViewRightBounds()
        {
            makePressKeyAndUpdate(Keys.Right);
            makePressKeyAndUpdate(Keys.Right);
            Assert.AreEqual(new Vector2(k_PxForSec, k_ShipYPos), sl.getPosition());
        }

        [Test]
        public void testShouldMoveAccordingToMouseDelta()
        {
            MockRepository mocks = new MockRepository();
            IInputManager inputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(inputManager.MousePositionDelta).Return(new Vector2(10, 20));
            mocks.ReplayAll();
            sl.Update(new GameTime(), inputManager);
            Assert.AreEqual(new Vector2(10, k_ShipYPos), sl.getPosition());
        }

        [Test]
        public void testShouldNotMoveBeyondBoundsOnMouseMove()
        {
            MockRepository mocks = new MockRepository();
            IInputManager inputManager = mocks.DynamicMock<IInputManager>();
            Expect.Call(inputManager.MousePositionDelta).Return(new Vector2(-10, 20));
            mocks.ReplayAll();
            sl.Update(new GameTime(), inputManager);
            Assert.AreEqual(new Vector2(k_XStartPosition, k_ShipYPos), sl.getPosition());
        }
    }
}