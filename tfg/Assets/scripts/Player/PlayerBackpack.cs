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
    private float mDamage = 1;
    private float mAttackSpeed = 1;
    private float mSpeed = 1;
    private float mDefense = 1;


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
        if(Input.GetKeyDown(KeyCode.Alpha1)&& consumables[0]!=null) {}
        if(Input.GetKeyDown(KeyCode.Alpha2)&& consumables[0]!=null){}
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
        mDamage = 1;
        mAttackSpeed = 1;
        mSpeed = 1;
        mDefense = 1;
        playerAttack.attackRange = 1.5f;
        playerAttack.attackSpeed = 2f;
        playerAttack.attackType = AttackType.melee;
        playerHealth.maxHealth = 15;
        playerHealth.cDefense = playerHealth.bDefense;
        WeaponEquip(weapon);
        foreach (var item in contents)
        {
            Debug.Log(item.Value.stack);

            SCT sct = item.Value.sct;
            int stack = item.Value.stack;
            int lvl = Mathf.CeilToInt(item.Value.lvl / 10f);
            playerAttack.damage += sct.iDamage * stack * lvl;
            mDamage += sct.mDamage * lvl;
            playerAttack.attackSpeed += sct.iAttackSpeed * stack * lvl;
            mAttackSpeed += sct.mAttackSpeed * lvl;
            playerAttack.attackRange += sct.iAttackRange * stack * lvl;
            playerControls.currentSpeed += sct.iSpeed * stack * lvl;
            mSpeed += sct.mSpeed * lvl;
            playerHealth.maxHealth += sct.iHealth * stack * lvl;
            playerHealth.cDefense += sct.iDefense * stack * lvl;
            mDefense += sct.mDefense * lvl;
        }
        Equipment(helmet);
        Equipment(chest);
        Equipment(pants);
        Equipment(boots);
        WeaponEquip(weapon);
        Equipment(rings);

        playerAttack.damage *= mDamage;
        playerAttack.attackSpeed *= mAttackSpeed;
        playerControls.currentSpeed *= mSpeed;
        playerHealth.cDefense *= mDefense;

        // revisar stats
        if (playerAttack.damage < 0.5f) playerAttack.damage = 0.5f;
        if (playerControls.currentSpeed <= 3f) playerControls.currentSpeed = 3f;
        if (playerHealth.cDefense < 0) playerHealth.cDefense = 0f;
        if (playerHealth.maxHealth < 1) playerHealth.maxHealth = 1f;
        if (playerHealth.regenHealth > playerHealth.maxHealth) playerHealth.regenHealth = playerHealth.maxHealth;
        if (playerHealth.currentHealth > playerHealth.maxHealth)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
            playerHealth.regenHealth = playerHealth.maxHealth;
        }
        playerHealth.ActHealthVisual();



    }
    private void Equipment(Item item)
    {
        if (item == null) return;
        int stack = item.stack;
        int lvl =Mathf.CeilToInt(item.lvl / 10f);
        playerAttack.damage += item.sct.eDamage*stack*lvl;
        mDamage += item.sct.mDamage*lvl;
        playerAttack.attackSpeed += item.sct.eAttackSpeed*stack*lvl;
        mAttackSpeed += item.sct.mAttackSpeed*lvl;
        playerAttack.attackRange += item.sct.eAttackRange*stack*lvl;
        playerControls.currentSpeed += item.sct.eSpeed*stack*lvl;
        mSpeed += item.sct.mSpeed*lvl;
        playerHealth.maxHealth += item.sct.eHealth*stack*lvl;
        playerHealth.cDefense += item.sct.eDefense*stack*lvl;
        mDefense += item.sct.mDefense*lvl;
    }
    private void Equipment(List<Item> items)
    {
        foreach (Item item in items)
        {
            Equipment(item);
        }
    }
    private void WeaponEquip(Item wep)
    {
        if (wep == null) return;
        playerAttack.attackType = wep.sct.attackType;
        playerAttack.attackSpeed = wep.sct.eAttackSpeed;
        playerAttack.bSpeed = wep.sct.bSpeed;
        playerAttack.bPiercing = wep.sct.bPiercing;
        playerAttack.bBounce = wep.sct.bBounce;
    }
}