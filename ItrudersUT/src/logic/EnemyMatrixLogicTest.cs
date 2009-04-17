using System;
using System.Collections.Generic;
using GameCommon.manager;
using Intruders.comp;
using Intruders.logic;
using ItrudersUT.comp.mock;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;

namespace ItrudersUT.logic
{
    [TestFixture]
    public class EnemyMatrixLogicTest
    {
        private const int k_SpriteWidth = 12;
        private EnemyMatrixLogic m_MatrixLogic;
        private MockViewFactory m_Factory;

        [SetUp]
        public void setUp()
        {
            m_Factory = new MockViewFactory(176, 200, k_SpriteWidth, k_SpriteWidth);
            m_MatrixLogic = new EnemyMatrixLogic(m_Factory);
            m_MatrixLogic.Initialize();
        }

        private GameTime createGameTime( int milliseconds )
        {
            return new GameTime(new TimeSpan(), new TimeSpan(),
                                new TimeSpan(), new TimeSpan(0, 0, 0, 0, milliseconds));
        }

        private void makeMovePastRightBounds()
        {
            // Move right
            m_MatrixLogic.Update(createGameTime(500));
            m_MatrixLogic.Update(createGameTime(500));
            // Start move left
            m_MatrixLogic.Update(createGameTime(500));
        }

        [Test]
        public void testShouldBeOnLeftScreen()
        {
            Assert.AreEqual(0, m_Factory.r_Sprites[0].Position.X);
        }

        [Test]
        public void testShouldChangeDirectionAfterTouchingViewBounds()
        {
            makeMovePastRightBounds();
            m_MatrixLogic.Update(createGameTime(475));
            Assert.AreEqual(k_SpriteWidth / 2, m_Factory.r_Sprites[0].Position.X);
        }

        [Test]
        public void testShouldGoDownOnEveryDirectionChange()
        {
            makeMovePastRightBounds();
            Assert.AreEqual((k_SpriteWidth * 3) + (k_SpriteWidth / 2),
                            m_Factory.r_Sprites[0].Position.Y);
            Assert.AreEqual(k_SpriteWidth, m_Factory.r_Sprites[0].Position.X);
        }

        [Test]
        public void testShouldJumpEveryHalfASecond()
        {
            m_MatrixLogic.Update(createGameTime(500 / 2));
            m_MatrixLogic.Update(createGameTime(500 / 2));
            Assert.AreEqual(k_SpriteWidth / 2, m_Factory.r_Sprites[0].Position.X);
            m_MatrixLogic.Update(createGameTime(500 / 2));
            Assert.AreEqual(k_SpriteWidth, m_Factory.r_Sprites[0].Position.X);
        }

        [Test]
        public void testShouldJumpRight()
        {
            m_MatrixLogic.Update(createGameTime(1000));
            Assert.AreEqual(6, m_Factory.r_Sprites[0].Position.X);
        }
    }
}