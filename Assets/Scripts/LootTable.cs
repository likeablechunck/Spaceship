using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct LootTableEntry
{
    public int itemLevel;
    public GameObject item;
    public float chance;
}

[System.Serializable]
public class LootTable : ScriptableObject
{
    public List<LootTableEntry> data;

    // How do we make one?!
    [MenuItem("Item Database/Create Loot Table")]
    public static LootTable CreateLootTable()
    {
        
        string assetPath = "Assets/LootTable.asset";
        LootTable loadedAsset = AssetDatabase.LoadAssetAtPath<LootTable>(assetPath);
        if (loadedAsset == null)
        {
            LootTable lt = ScriptableObject.CreateInstance<LootTable>();
            AssetDatabase.CreateAsset(lt, "Assets/LootTable.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            loadedAsset = AssetDatabase.LoadAssetAtPath<LootTable>(assetPath);
        }
        if(loadedAsset.data.Count == 0)
        {
            // add few loot table entries there
            LootTableEntry Coins = new LootTableEntry();
            Coins.chance = 0.85f;
            Coins.itemLevel = 0;
            Coins.item = (GameObject)Resources.Load("Treasures/Coins");
            LootTableEntry goldFruit = new LootTableEntry();
            goldFruit.chance = 0.25f;
            goldFruit.itemLevel = 0;
            goldFruit.item = (GameObject)Resources.Load("Treasures/Gold_Fruit");
            LootTableEntry specialBullet = new LootTableEntry();
            specialBullet.chance = 0.65f;
            specialBullet.itemLevel = 0;
            specialBullet.item = (GameObject)Resources.Load("Treasures/Special_Bullet");
            LootTableEntry specialTurret = new LootTableEntry();
            specialTurret.chance = 0.90f;
            specialTurret.itemLevel = 0;
            specialTurret.item = (GameObject)Resources.Load("Treasures/Special_Turret");
            loadedAsset.data.Add(Coins);
            loadedAsset.data.Add(specialTurret);
            loadedAsset.data.Add(specialBullet);
            loadedAsset.data.Add(goldFruit);
        }
        return loadedAsset;
    }
}