using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int sizeX;
    public int sizeY;
    public BoolArray2D shape = new BoolArray2D(8);
    
    
    //public MultiDimensionalBool[] myBoolArray;
    
}
public enum Dir
{
    Up,
    Right,
    Down,
    Left
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