using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Intruders.comp
{
    public class Asset
    {
        private readonly List<Rectangle> r_Bounds = new List<Rectangle>();
        private readonly string r_Name;

        public Asset(string i_Name)
        {
            r_Name = i_Name;
        }

        public string GetAssetName()
        {
            return r_Name;
        }

        public void addBounds(Rectangle i_Bounds)
        {
            r_Bounds.Add(i_Bounds);
        }

        public Rectangle GetBoundsAt(int i_Index)
        {
            return r_Bounds[i_Index];
        }
    }
}