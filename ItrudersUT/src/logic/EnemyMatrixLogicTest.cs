using System;
using Intruders.logic;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace ItrudersUT.logic
{
    [TestFixture]
    public class EnemyMatrixLogicTest
    {
        private EnemyMatrixLogic r_MatrixLogic;
        private const int k_SpriteWidth = 12;

        [SetUp]
        public void setUp()
        {
            r_MatrixLogic = new EnemyMatrixLogic(k_SpriteWidth, k_SpriteWidth, 176, 200);
        }

        [Test]
        public void testShouldBeOnLeftScreen()
        {
            Assert.AreEqual(0, r_MatrixLogic.getPosition()[0].Position.X);
        }

        [Test]
        public void testShouldJumpRight()
        {
            r_MatrixLogic.Update(createGameTime(1000), null);
            Assert.AreEqual(6, r_MatrixLogic.getPosition()[0].Position.X);
        }

        [Test]
        public void testShouldJumpEveryHalfASecond()
        {
            r_MatrixLogic.Update(createGameTime(500/2), null);
            r_MatrixLogic.Update(createGameTime(500/2), null);
            Assert.AreEqual(k_SpriteWidth / 2, r_MatrixLogic.getPosition()[0].Position.X);
            r_MatrixLogic.Update(createGameTime(500/2), null);
            Assert.AreEqual(k_SpriteWidth, r_MatrixLogic.getPosition()[0].Position.X);
        }

        private GameTime createGameTime(int milliseconds)
        {
            return new GameTime(new TimeSpan(), new TimeSpan(),
                                new TimeSpan(), new TimeSpan(0, 0, 0, 0, milliseconds));
        }

        [Test]
        public void testShouldChangeDirectionAfterTouchingViewBounds()
        {
            makeMovePastRightBounds();
            r_MatrixLogic.Update(createGameTime(475), null);
            Assert.AreEqual(k_SpriteWidth / 2, r_MatrixLogic.getPosition()[0].Position.X);
        }

        [Test]
        public void testShouldGoDownOnEveryDirectionChange()
        {
            makeMovePastRightBounds();
            Assert.AreEqual((k_SpriteWidth * 3) + (k_SpriteWidth / 2), 
                r_MatrixLogic.getPosition()[0].Position.Y);
            Assert.AreEqual(k_SpriteWidth, r_MatrixLogic.getPosition()[0].Position.X);
        }

        private void makeMovePastRightBounds()
        {
            // Move right
            r_MatrixLogic.Update(createGameTime(500), null);
            r_MatrixLogic.Update(createGameTime(500), null);
            // Start move left
            r_MatrixLogic.Update(createGameTime(500), null);
        }

        [Test]
        public void testShouldDecreaseTimeBetweenJumpsIn5PercentOnChangeDirection()
        {
            
        }
    }
}