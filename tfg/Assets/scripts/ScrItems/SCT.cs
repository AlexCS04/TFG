using UnityEngine;

[CreateAssetMenu(fileName = "SCT", menuName = "Scriptable Objects/SCT")]
public class SCT : ScriptableObject
{

    public Sprite sprite;

    public EquipType equipType;
    public int maxStack;
    public int peso;
    public int sizeX;
    public int sizeY;
    public Vector2Int spwnQuantity;
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
public enum EquipType{

    PrimaryWeapon,
    SecondaryWeapon,
    Ring, 
    Consumable,
    Helmet,
    Chestplate,
    Pants,
    Boots,
    Backpack

}