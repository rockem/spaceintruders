using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intruders.utils
{
    public enum eAssetType
    {
        Ship, 
        Player1,
        Player2,
        PinkEnemy1,
        PinkEnemy2,
        YellowEnemy1,
        YellowEnemy2,
        BlueEnemy1,
        BlueEnemy2,
        MotherShip,
        Bullet,
        Barrier,
        Background
    }

    public class Asset
    {
        private readonly Texture2D m_Texture;
        private readonly Rectangle? m_Bounds;

        public Asset(Texture2D i_Texture, Rectangle? i_Bounds)
        {
            m_Texture = i_Texture;
            m_Bounds = i_Bounds;
        }
        public Texture2D Texture
        {
            get { return m_Texture; }
        }

        public Rectangle? Bounds
        {
            get { return m_Bounds; }
        }
    }

    public class AssetRepository
    {
        private readonly Game r_Game;
        private readonly Dictionary<eAssetType, Asset> r_Assets = new Dictionary<eAssetType, Asset>();

        public AssetRepository(Game i_Game)
        {
            r_Game = i_Game;
        }


        public void LoadContent()
        {
            addAsset(@"Sprites\BlueShip", eAssetType.Player1);
            addAsset(@"Sprites\GreenShip", eAssetType.Player2);
            addAsset(@"Sprites\Bullet", eAssetType.Bullet);
            addAsset(@"Sprites\MotherShip", eAssetType.MotherShip);
            addAsset(@"Sprites\Barrier", eAssetType.Barrier);
            addAsset(@"Sprites\Background1", eAssetType.Background);
            Texture2D texture = r_Game.Content.Load<Texture2D>(@"Sprites\Enemies");
            r_Assets.Add(eAssetType.BlueEnemy1, new Asset(texture, new Rectangle(0, 0, 32, 32)));
            r_Assets.Add(eAssetType.BlueEnemy2, new Asset(texture, new Rectangle(0, 32, 32, 32)));
            r_Assets.Add(eAssetType.PinkEnemy1, new Asset(texture, new Rectangle(32, 0, 32, 32)));
            r_Assets.Add(eAssetType.PinkEnemy2, new Asset(texture, new Rectangle(32, 32, 32, 32)));
            r_Assets.Add(eAssetType.YellowEnemy1, new Asset(texture, new Rectangle(64, 0, 32, 32)));
            r_Assets.Add(eAssetType.YellowEnemy2, new Asset(texture, new Rectangle(64, 64, 32, 32)));

        }

        private void addAsset(string i_Name, eAssetType i_Type)
        {
            Texture2D texture = r_Game.Content.Load<Texture2D>(i_Name);
            r_Assets.Add(i_Type, new Asset(texture, null));
        }

        public Asset GetAsset(eAssetType i_Type)
        {
            return r_Assets[i_Type];
        }
    }
}