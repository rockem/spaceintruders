using System;
using System.Collections.Generic;
using GameCommon.service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCommon.manager
{
    public class CollisionsManager : GameService, ICollisionsManager
    {
        protected readonly List<ICollidable> m_Collidables = new List<ICollidable>();

        public CollisionsManager(Game i_Game)
            :
                base(i_Game, int.MaxValue)
        {
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ICollisionsManager), this);
        }

        public void AddObjectToMonitor(ICollidable i_Collidable)
        {
            if (!m_Collidables.Contains(i_Collidable))
            {
                m_Collidables.Add(i_Collidable);
                i_Collidable.PositionChanged += collidable_PositionChanged;
                i_Collidable.Disposed += collidable_Disposed;
            }
        }

        private void collidable_PositionChanged(object i_Collidable)
        {
            CheckCollision(i_Collidable as ICollidable);
        }

        private void collidable_Disposed(object sender, EventArgs e)
        {
            ICollidable collidable = sender as ICollidable;

            if (collidable != null
                &&
                this.m_Collidables.Contains(collidable))
            {
                collidable.PositionChanged -= collidable_PositionChanged;
                collidable.Disposed -= collidable_Disposed;
                m_Collidables.Remove(collidable);
            }
        }

        private void CheckCollision(ICollidable i_Source)
        {
            foreach (ICollidable target in m_Collidables)
            {
                if (i_Source != target && target.Visible)
                {
                    if (Check2DCollision(i_Source, target))
                    {
                        target.Collided(i_Source);
                        i_Source.Collided(target);
                    }
                }
            }
        }

        private bool Check2DCollision(ICollidable i_Source, ICollidable i_Target)
        {
            bool collided = false;
            ICollidable2D source = i_Source as ICollidable2D;
            ICollidable2D target = i_Target as ICollidable2D;
            if (source != null && target != null)
            {
                collided = source.Bounds.Intersects(target.Bounds);

                if (collided)
                {
                    collided = collisionOn2DPixelLevel(source, target);
                }
            }

            return collided;
        }

        private bool collisionOn2DPixelLevel(ICollidable2D i_CollisionObject1, ICollidable2D i_CollisionObject2)
        {
            Color[] pixelData1 = i_CollisionObject1.GetPixelArray();
            Color[] pixelData2 = i_CollisionObject2.GetPixelArray();

            int Top = Math.Max(i_CollisionObject1.Bounds.Top, i_CollisionObject2.Bounds.Top);
            int Bottom = Math.Min(i_CollisionObject1.Bounds.Bottom, i_CollisionObject2.Bounds.Bottom);
            int Left = Math.Max(i_CollisionObject1.Bounds.Left, i_CollisionObject2.Bounds.Left);
            int Right = Math.Min(i_CollisionObject1.Bounds.Right, i_CollisionObject2.Bounds.Right);

            for (int y = Top; y < Bottom; y++)
            {
                for (int x = Left; x < Right; x++)
                {
                    Color colorA = pixelData1[((y - i_CollisionObject1.Bounds.Top) * i_CollisionObject1.Bounds.Width) + (x - i_CollisionObject1.Bounds.Left)];
                    Color colorB = pixelData2[((y - i_CollisionObject2.Bounds.Top) * i_CollisionObject2.Bounds.Width) + (x - i_CollisionObject2.Bounds.Left)];

                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}