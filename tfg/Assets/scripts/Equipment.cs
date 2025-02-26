using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment : MonoBehaviour, IPointerDownHandler
{

    public EquipType equipType;

    public bool ocupado;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (BackpackManager.instance.HasItem())
        {
            BackpackManager.instance.EndDrag(this);

        }
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
