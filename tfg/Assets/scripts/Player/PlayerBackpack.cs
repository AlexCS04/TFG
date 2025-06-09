using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, Item> contents = new Dictionary<Vector2Int, Item>();

    [SerializeField] private Item helmet;
    [SerializeField] private Item chest;
    [SerializeField] private Item pants;
    [SerializeField] private Item boots;
    [SerializeField] private Item weapon;
    [SerializeField] private List<Item> rings;
    [SerializeField] private List<Item> consumables;
    private PlayerControls playerControls;
    private PlayerHealth playerHealth;
    private Attack playerAttack;
    

    void OnEnable()
    {
        Eventmanager.PutItemEvent += PutItem;
        Eventmanager.RemoveItemEvent += RemoveItem;
        Eventmanager.PutEquipEvent += PutEquip;
        Eventmanager.RemoveEquipEvent += RemoveEquip;
    }
    void OnDisable()
    {
        Eventmanager.PutItemEvent -= PutItem;
        Eventmanager.RemoveItemEvent -= RemoveItem;
        Eventmanager.PutEquipEvent -= PutEquip;
        Eventmanager.RemoveEquipEvent -= RemoveEquip;

    }
    public void PutItem(Item item, Vector2Int key)
    {
        if (contents.ContainsKey(key))
        {
            // Debug.Log("Stacking too much ", item);
            return;
        }
        contents.Add(key, item);
        ActualizarStats();
    }
    public void RemoveItem(Vector2Int key)
    {
        if (!contents.ContainsKey(key))
        {
            // Debug.Log("Removing nothing");
            return;
        }
        contents.Remove(key);
        ActualizarStats();
    }
    void Start()
    {
        playerControls = gameObject.GetComponent<PlayerControls>();
        playerAttack = gameObject.GetComponent<Attack>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var item in contents)
            {
                Debug.Log(item.Key);
                Debug.Log(item.Value);
                Debug.Log(item.Value.stack);
            }
        }
    }

    private void PutEquip(Item item)
    {
        switch (item.sct.equipType)
        {
            case EquipType.Helmet:
                helmet = item;
                break;
            case EquipType.Chestplate:
                chest = item;
                break;
            case EquipType.Pants:
                pants = item;
                break;
            case EquipType.Boots:
                boots = item;
                break;
            case EquipType.PrimaryWeapon:
                weapon = item;
                break;
            case EquipType.Ring:
                rings.Add(item);
                break;
            case EquipType.Consumable:
                consumables.Add(item);
                break;
            case EquipType.Backpack:
                break;
            case EquipType.SecondaryWeapon:
                break;

        }
        ActualizarStats();
    }
    private void RemoveEquip(Item item)
    {
        switch (item.sct.equipType)
        {
            case EquipType.Helmet:
                helmet = null;
                break;
            case EquipType.Chestplate:
                chest = null;
                break;
            case EquipType.Pants:
                pants = null;
                break;
            case EquipType.Boots:
                boots = null;
                break;
            case EquipType.PrimaryWeapon:
                weapon = null;
                break;
            case EquipType.Ring:
                rings.Remove(item);
                break;
            case EquipType.Consumable:
                consumables.Remove(item);
                break;
            case EquipType.Backpack:
                break;
            case EquipType.SecondaryWeapon:
                break;

        }
        ActualizarStats();
    }
    private void ActualizarStats()
    {
        
        if (playerControls.mochilaPeso >= .8f * playerControls.mochilaMaxPeso) playerControls.currentSpeed = playerControls.speed / 2f;
        else playerControls.currentSpeed = playerControls.speed;
        playerAttack.damage = 1;
        playerAttack.attackRange=1.5f;
        playerAttack.attackSpeed=2.3f;
        playerAttack.attackType=AttackType.melee;
        playerHealth.maxHealth=15;
        foreach (var item in contents)
        {
            Debug.Log(item.Value.stack);






        }




    }
}