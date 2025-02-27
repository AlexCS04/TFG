using UnityEngine;

public class GrounItemtest : MonoBehaviour
{
    public ItemSO itemSO;

    [SerializeField]private int cantidad=1;
    [SerializeField]private int level=1;




    public int GetCantidad(){
        return cantidad;
    }
    public void AddCantidad(int c){
        cantidad += c;
    }
    public void SetCantidad(int c){
        cantidad = c;
    }
    public int GetLevel(){
        return level;
    }
    public void SetLevel(int l){
        level = l;
    }
}
