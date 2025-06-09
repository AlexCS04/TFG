using UnityEngine;

[CreateAssetMenu(fileName = "SCT", menuName = "Scriptable Objects/SCT")]
public class SCT : ScriptableObject
{

    public Sprite sprite;
    public string Name;

    public EquipType equipType;
    public int maxStack;
    public int peso;
    public int sizeX;
    public int sizeY;
    public Vector2Int spwnQuantity;

    public float iDamage;
    public float eDamage;
    public float mDamage;
    public float iAttackSpeed;
    public float eAttackSpeed;
    public float mAttackSpeed;
    public float iSpeed;
    public float eSpeed;
    public float mSpeed;

    public float iAttackRange;
    public float eAttackRange;

    public float iHealth;
    public float eHealth;

    public float iDefense;
    public float eDefense;
    public float mDefense;
    public AttackType attackType;

    public float bSpeed;

    public int bPiercing;

    public bool bBounce;



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
    Backpack

}
