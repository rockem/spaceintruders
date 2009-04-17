using System;
using Intruders.logic;
using ItrudersUT.comp.mock;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace ItrudersUT.logic
{
    [TestFixture]
    public class BulletTest
    {

        [Test]
        public void testShouldMoveAccordingToYVelocity()
        {
            Bullet bullet = new Bullet(new MockViewFactory(100, 100, 10, 10));
            bullet.Position = new Vector2(50, 50);
            bullet.YVelocity = -10;
            bullet.Update(gameTimeWithOneSecond());
            Assert.AreEqual(new Vector2(50, 40), bullet.Position);
        }

        private GameTime gameTimeWithOneSecond()
        {
            return new GameTime(new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan(0, 0, 1));
        }

        [Test]
        public void testShouldKillItselfWhenReachingTheEdgeOfTheView()
        {
            Bullet bullet = new Bullet(new MockViewFactory(100, 100, 10, 10));
            bullet.Position = new Vector2(50, 10);
            bullet.YVelocity = -10;
            bullet.Alive = true;
            bullet.Update(gameTimeWithOneSecond());
            Assert.AreEqual(false, bullet.Alive);
        }
    }
}