using UnityEngine;

[CreateAssetMenu(fileName = "SCT", menuName = "Scriptable Objects/SCT")]
public class SCT : ScriptableObject
{

    public Sprite sprite;
    public string Name;

    public EquipType equipType;
    public int maxStack;
    public float peso;
    public int sizeX;
    public int sizeY;
    public Vector2Int spwnQuantity;

    public float iDamage;//
    public float eDamage;//
    public float mDamage;//
    public float iAttackSpeed;//
    public float eAttackSpeed;//
    public float mAttackSpeed;//
    public float iSpeed;//
    public float eSpeed;//
    public float mSpeed;//

    public float iAttackRange;//
    public float eAttackRange;//
    public float iRQuant;//
    public float eRQuant;//

    public float iHealth;//
    public float eHealth;//

    public float iDefense;//
    public float eDefense;//
    public float mDefense;//

    public float iWeight;
    public float eWeight;
    public AttackType attackType;//

    public float bSpeed=1;//

    public int bPiercing=1;//

    public bool bBounce;//

    [Range(5,300)]
    public int price;
    public float tHealth;
    public float rHealth;
    public float filling;

    public float consumableExtraTime;
    public float tempDamageMult = 1; 
    public float tempSpeedMult=1; 
    public float tempDefenseMult=1; 
    public float tempRegenMult=1; 
    public float tempAttackSpeedMult=1; 



    public BoolArray2D shape = new BoolArray2D(8);

}
[System.Serializable]
public class BoolArray2D
{

    [System.Serializable]
    public struct rowData{
        public bool[] array;
    }
    public rowData[] rows;
    public BoolArray2D(int i) { rows = new rowData[i]; }

}
[System.Serializable]
public class MultiDimensionalBool
{
        public bool[] boolArray;
}
public enum EquipType
{

    PrimaryWeapon,
    SecondaryWeapon, //tal vez no
    Ring,
    Consumable,
    Helmet,
    Chestplate,
    Pants,
    Boots,
    Backpack, 
    Extra

}
