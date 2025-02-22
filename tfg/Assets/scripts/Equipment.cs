using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment : MonoBehaviour, IPointerDownHandler
{

    public EquipType equipType;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
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
