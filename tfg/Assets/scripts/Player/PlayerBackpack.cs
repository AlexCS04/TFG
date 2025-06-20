using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, Item> contents = new Dictionary<Vector2Int, Item>();

    [SerializeField] private Item helmet;
    [SerializeField] private Item chest;
    [SerializeField] private Item pants;
    [SerializeField] private Item boots;
    [SerializeField] private Item weapon;
    [SerializeField] private List<Item> rings=new List<Item>(3);
    [SerializeField] private List<Item> consumables=new List<Item>(2);
    private PlayerControls playerControls;
    private PlayerHealth playerHealth;
    private Attack playerAttack;
    private float mDamage = 1;
    private float mAttackSpeed = 1;
    private float mSpeed = 1;
    private float mDefense = 1;

    public int money = 0;

    public ShopItem shopItem;
    #region Temporal Mults
    public float tempDamageMult=1; 
    public float tempSpeedMult=1; 
    public float tempDefenseMult=1; 
    public float tempRegenMult=1; 
    public float tempAttackSpeedMult=1; 
    // public float tempMult=1; //por si se me ocurren más
    #endregion

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
        ActualizarStats();
        // damageThread = new Thread(TDamage(consumables[0].sct));
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && consumables[0] != null)
        {
            Consume(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && consumables[1] != null)
        {
            Consume(1);
        }
        if (Input.GetKeyDown(KeyCode.F) && shopItem != null)
        {

            Purchase(shopItem.price);
            shopItem.Buy();
            shopItem = null;
            
        }
    }
    private void Consume(int cons)
    {
        if (playerHealth.Consume(consumables[cons].sct.tHealth* Mathf.CeilToInt(consumables[cons].lvl / 10f), consumables[cons].sct.rHealth * Mathf.CeilToInt(consumables[cons].lvl / 10f), consumables[cons].sct.filling))
        {
            consumables[cons].AddCantidad(-1);
            CheckMult(consumables[cons].sct);
            if (consumables[cons].GetCantidad() == 0)
            {
                ContainerManager.instance.equipment[cons + 10].RemoveAt(consumables[cons].gridPos);
                Destroy(consumables[cons].gameObject);
                // consumables[cons] = null;
                ActualizarStats();
            }
        }
    }
    #region TemporalFunctions
    private void CheckMult(SCT sct)
    {
        if (sct.tempDamageMult != 1 && tempDamageMult <= sct.tempDamageMult)
        {
            StopCoroutine("TempDamage");
            StartCoroutine(TempDamage(sct.tempDamageMult, sct.consumableExtraTime));
        }
        if (sct.tempDefenseMult != 1 && tempDefenseMult <= sct.tempDefenseMult)
        {
            StopCoroutine("TempDefense");
            StartCoroutine(TempDefense(sct.tempDefenseMult, sct.consumableExtraTime));
        }
        if (sct.tempSpeedMult != 1 && tempSpeedMult <= sct.tempSpeedMult)
        {
            StopCoroutine("TempSpeed");
            StartCoroutine(TempSpeed(sct.tempSpeedMult, sct.consumableExtraTime));
        }
        if (sct.tempRegenMult != 1 && tempRegenMult <= sct.tempRegenMult)
        {
            StopCoroutine("TempRegen");
            StartCoroutine(TempRegen(sct.tempRegenMult, sct.consumableExtraTime));
        }
        if (sct.tempAttackSpeedMult != 1 && tempAttackSpeedMult <= sct.tempAttackSpeedMult)
        {
            StopCoroutine("TempAttackSpeed");
            StartCoroutine(TempAttackSpeed(sct.tempAttackSpeedMult, sct.consumableExtraTime));
        }

        ActualizarStats();
    }

    IEnumerator TempDamage(float mult, float time)
    {
        tempDamageMult = mult;
        yield return new WaitForSeconds(time);
        tempDamageMult = 1;
        ActualizarStats();
    }
    IEnumerator TempDefense(float mult, float time)
    {
        tempDefenseMult = mult;
        yield return new WaitForSeconds(time);
        tempDefenseMult = 1;
        ActualizarStats();
    }
    IEnumerator TempSpeed(float mult, float time)
    {
        tempSpeedMult = mult;
        yield return new WaitForSeconds(time);
        tempSpeedMult = 1;
        ActualizarStats();
    }
    IEnumerator TempRegen(float mult, float time)
    {
        tempRegenMult = mult;
        yield return new WaitForSeconds(time);
        tempRegenMult = 1;
        ActualizarStats();
    }
    IEnumerator TempAttackSpeed(float mult, float time)
    {
        tempAttackSpeedMult = mult;
        yield return new WaitForSeconds(time);
        tempAttackSpeedMult = 1;
        ActualizarStats();
    }


    #endregion
    private void PutEquip(Item item, int place)
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
                rings.Insert(place, item);
                break;
            case EquipType.Consumable:
                consumables.Insert(place, item);
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


        playerControls.currentSpeed = playerControls.speed;
        playerAttack.damage = 1;
        mDamage = 1;
        mAttackSpeed = 1;
        mSpeed = 1;
        mDefense = 1;
        playerAttack.attackRange = 1.5f;
        playerAttack.attackSpeed = 2f;
        playerAttack.attackType = AttackType.melee;
        playerHealth.maxHealth = 15;
        playerHealth.rQuant = 1;
        playerHealth.cDefense = playerHealth.bDefense;
        money = 0;
        WeaponEquip(weapon);
        foreach (var item in contents)
        {
            // Debug.Log(item.Value.stack);

            SCT sct = item.Value.sct;
            int stack = item.Value.stack;
            int lvl = Mathf.CeilToInt(item.Value.lvl / 10f);
            playerAttack.damage += sct.iDamage * stack * lvl;
            if (sct.mDamage != 0) mDamage += sct.mDamage * lvl;
            playerAttack.attackSpeed += sct.iAttackSpeed * stack * lvl;
            if (sct.mAttackSpeed != 0) mAttackSpeed += sct.mAttackSpeed * lvl;
            playerAttack.attackRange += sct.iAttackRange * stack * lvl;
            playerControls.currentSpeed += sct.iSpeed * stack * lvl;
            if (sct.mSpeed != 0) mSpeed += sct.mSpeed * lvl;
            playerHealth.maxHealth += sct.iHealth * stack * lvl;
            playerHealth.rQuant += sct.iRQuant * stack * lvl;
            playerHealth.cDefense += sct.iDefense * stack * lvl;
            if (sct.mDefense != 0) mDefense += sct.mDefense * lvl;
            if (sct.Name == "Money") money += stack;
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
        if (playerControls.mochilaPeso >= .8f * playerControls.mochilaMaxPeso) playerControls.currentSpeed = playerControls.currentSpeed / 2f;
        if (playerHealth.cDefense < 0) playerHealth.cDefense = 0f;
        if (playerHealth.maxHealth < 1) playerHealth.maxHealth = 1f;
        if (playerHealth.regenHealth > playerHealth.maxHealth) playerHealth.regenHealth = playerHealth.maxHealth;
        if (playerHealth.currentHealth > playerHealth.maxHealth)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
        playerHealth.ActHealthVisual();
        playerAttack.damage *= tempDamageMult;
        playerControls.currentSpeed *= tempSpeedMult;
        playerHealth.cDefense *= tempDefenseMult;
        playerAttack.attackSpeed *= 1/tempAttackSpeedMult;
        playerHealth.rQuant *= tempRegenMult;



    }
    private void Equipment(Item item)
    {
        if (item == null) return;
        int stack = item.stack;
        int lvl = Mathf.CeilToInt(item.lvl / 10f);
        playerAttack.damage += item.sct.eDamage * stack * lvl;
        if(item.sct.mDamage!=0)mDamage += item.sct.mDamage * lvl;
        playerAttack.attackSpeed += item.sct.eAttackSpeed * stack * lvl;
        if(item.sct.mAttackSpeed!=0)mAttackSpeed += item.sct.mAttackSpeed * lvl;
        playerAttack.attackRange += item.sct.eAttackRange * stack * lvl;
        playerControls.currentSpeed += item.sct.eSpeed * stack * lvl;
        if(item.sct.mSpeed!=0)mSpeed += item.sct.mSpeed * lvl;
        playerHealth.maxHealth += item.sct.eHealth * stack * lvl;
        playerHealth.rQuant += item.sct.eRQuant * stack * lvl;
        playerHealth.cDefense += item.sct.eDefense * stack * lvl;
        if(item.sct.mDefense!=0)mDefense += item.sct.mDefense * lvl;
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
        Equipment(wep);
    }
    public void Purchase(int stack)
    {
        // Debug.Log(contents.Count);
        for (int index = contents.Count - 1; index >= 0; index--)
        {
            // Debug.Log("hUH");
            var cont = contents.ElementAt(index);
            Item item = cont.Value;
            // Debug.Log(item.GetCantidad());
            if (item.sct.Name == "Money")
            {
                int dar = stack - item.GetCantidad();
                if (dar <= 0) dar = stack;
                else dar = item.GetCantidad();
                stack -= dar;
                item.container.ActualizarPeso(-dar);
                item.AddCantidad(-dar);
                Debug.Log(item.GetCantidad());
                item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetCantidad().ToString();
                if (item.GetCantidad() <= 0)
                {
                    ContainerManager.instance.mochila.GetComponent<Container>().RemoveAt(cont.Key);
                    RemoveItem(cont.Key);
                    Destroy(item.gameObject);
                }
            }
            if (stack == 0) return;
        }
        ActualizarStats();
    }
}