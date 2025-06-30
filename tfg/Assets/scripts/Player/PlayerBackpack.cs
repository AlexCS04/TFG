using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBackpack : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, Item> contents = new Dictionary<Vector2Int, Item>();

    [SerializeField] private GameObject slashEffect;
    [SerializeField] private GameObject poundEffect;
    [SerializeField] private Item helmet;
    [SerializeField] private Item chest;
    [SerializeField] private Item pants;
    [SerializeField] private Item boots;
    [SerializeField] private Item weapon;
    [SerializeField] private List<Item> rings=new List<Item>(3);
    [SerializeField] private Item[] consumables=new Item[2];
    [SerializeField] private GameObject consumable1;
    [SerializeField] private GameObject consumable2;
    [SerializeField] private TextMeshProUGUI textStats;
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

    private Coroutine lastTDM;
    private Coroutine lastTSM;
    private Coroutine lastTDeM;
    private Coroutine lastTRM;
    private Coroutine lastTASM;
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
        if (!contents.ContainsKey(key))
        {
            contents.Add(key, item);
        }
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
        if (playerHealth.Consume(consumables[cons].sct.tHealth * Mathf.CeilToInt(consumables[cons].lvl / 10f), consumables[cons].sct.rHealth * Mathf.CeilToInt(consumables[cons].lvl / 10f), consumables[cons].sct.filling))
        {
            
            
            consumables[cons].container.ActualizarPeso(-consumables[cons].sct.peso);
            consumables[cons].AddCantidad(-1);
            CheckMult(consumables[cons].sct);
            if (consumables[cons].GetCantidad() == 0)
            {
                ContainerManager.instance.equipment[cons + 8].RemoveAt(consumables[cons].gridPos);
                Destroy(consumables[cons].gameObject);
                consumables[cons] = null;
            }

            ActualizarStats();
            ConsumableAct();
        }
    }
    #region TemporalFunctions
    private void CheckMult(SCT sct)
    {
        if (sct.tempDamageMult != 1)
        {
            if(lastTDM!=null)StopCoroutine("TempDamage");
            lastTDM=StartCoroutine(TempDamage(sct.tempDamageMult, sct.consumableExtraTime));
        }
        if (sct.tempDefenseMult != 1)
        {
            if(lastTDeM!=null)StopCoroutine("TempDefense");
            lastTDeM=StartCoroutine(TempDefense(sct.tempDefenseMult, sct.consumableExtraTime));
        }
        if (sct.tempSpeedMult != 1)
        {
            if(lastTSM!=null)StopCoroutine("TempSpeed");
            lastTSM=StartCoroutine(TempSpeed(sct.tempSpeedMult, sct.consumableExtraTime));
        }
        if (sct.tempRegenMult != 1)
        {
            if(lastTRM!=null)StopCoroutine(lastTRM);
            lastTRM=StartCoroutine(TempRegen(sct.tempRegenMult, sct.consumableExtraTime));
        }
        if (sct.tempAttackSpeedMult != 1)
        {
            if(lastTASM!=null)StopCoroutine("TempAttackSpeed");
            lastTASM=StartCoroutine(TempAttackSpeed(sct.tempAttackSpeedMult, sct.consumableExtraTime));
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
                consumables[place]=item;
                ConsumableAct();
                break;
            case EquipType.Backpack:
                break;
            case EquipType.SecondaryWeapon:
                break;

        }
        ActualizarStats();
    }
    private void RemoveEquip(Item item,int place)
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
                consumables[place]=null;
                ConsumableAct();
                break;
            case EquipType.Backpack:
                break;
            case EquipType.SecondaryWeapon:
                break;

        }
        ActualizarStats();
    }
    public void ActualizarStats()
    {


        playerControls.currentSpeed = playerControls.speed;
        playerControls.mochilaMaxPeso = 25f;
        playerAttack.damage = 1;
        mDamage = 1;
        mAttackSpeed = 1;
        mSpeed = 1;
        mDefense = 1;
        playerAttack.attackRange = 1f;
        playerAttack.attackSpeed = 2f;
        playerAttack.attackType = AttackType.meleeC;
        playerAttack.particlePref = poundEffect;
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
            if (sct.iDamage > 0) playerAttack.damage += sct.iDamage * stack * lvl; else playerAttack.damage += sct.iDamage * stack * (lvl*.4f+.4f);
            if (sct.mDamage != 0) { if (sct.iDamage > 0) mDamage += sct.mDamage * stack * lvl; else mDamage += sct.mDamage * stack * (lvl * .4f + .4f); }
            if (sct.iAttackSpeed > 0) playerAttack.attackSpeed += sct.iAttackSpeed * stack * lvl; else playerAttack.attackSpeed += sct.iAttackSpeed * stack * (lvl*.4f+.4f);
            if (sct.mAttackSpeed != 0) { if (sct.mAttackSpeed > 0) mAttackSpeed += sct.mAttackSpeed * stack * lvl; else mAttackSpeed += sct.mAttackSpeed * stack * (lvl * .4f + .4f); }
            if (sct.iAttackRange > 0) playerAttack.attackRange += sct.iAttackRange * stack * lvl; else playerAttack.attackRange += sct.iAttackRange * stack * (lvl*.4f+.4f);
            if (sct.iSpeed > 0) playerControls.currentSpeed += sct.iSpeed * stack * lvl; else playerControls.currentSpeed += sct.iSpeed * stack * (lvl*.4f+.4f);
            if (sct.mSpeed != 0) { if (sct.mSpeed > 0) mSpeed += sct.mSpeed * stack * lvl; else mSpeed += sct.mSpeed * stack * (lvl * .4f + .4f); }
            if (sct.iHealth > 0) playerHealth.maxHealth += sct.iHealth * stack * lvl; else playerHealth.maxHealth += sct.iHealth * stack * (lvl * .4f + .4f);
            if (sct.iRQuant > 0) playerHealth.rQuant += sct.iRQuant * stack * lvl; else playerHealth.rQuant += sct.iRQuant * stack * (lvl * .4f + .4f);
            if (sct.iDefense > 0) playerHealth.cDefense += sct.iDefense * stack * lvl; else playerHealth.cDefense += sct.iDefense * stack * (lvl*.4f+.4f);
            if (sct.mDefense != 0) { if (sct.mDefense > 0) mDefense += sct.mDefense * stack * lvl; else mDefense += sct.mDefense * stack * (lvl * .4f + .4f); }
            if (sct.iWeight > 0) playerControls.mochilaMaxPeso += sct.iWeight * stack * lvl; else playerControls.mochilaMaxPeso += sct.iWeight * stack * (lvl*.4f+.4f);
            if (sct.Name == "Money") money += stack;
        }
        Equipment(helmet);
        Equipment(chest);
        Equipment(pants);
        Equipment(boots);
        Equipment(rings);

        playerAttack.damage *= mDamage;
        playerAttack.attackSpeed *= mAttackSpeed;
        playerControls.currentSpeed *= mSpeed;
        playerHealth.cDefense *= mDefense;

        // revisar limite stats
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
        if (playerAttack.attackRange < .6f) playerAttack.attackRange = .6f;
        if (playerAttack.attackSpeed < 0) playerAttack.attackSpeed = 0;
        if (playerControls.mochilaPeso < 0.1f) playerControls.mochilaPeso = 0;
        playerHealth.ActHealthVisual();
        playerAttack.damage *= tempDamageMult;
        playerControls.currentSpeed *= tempSpeedMult;
        playerHealth.cDefense *= tempDefenseMult;
        playerAttack.attackSpeed *= 1/tempAttackSpeedMult;
        // Debug.Log(tempRegenMult);
        playerHealth.rQuant *= tempRegenMult;
        

        ActualizarTextStats();

    }
    private void ActualizarTextStats()
    {
        textStats.text = "Attack Speed: " + playerAttack.attackSpeed.ToString() + "\n" +
                            "Damage: " + playerAttack.damage.ToString() + "\n" +
                            "Range: " + playerAttack.attackRange.ToString() + "\n" +
                            "Speed: " + playerControls.currentSpeed.ToString() + "\n" +
                            "Max Health: " + playerHealth.maxHealth.ToString() + "\n" +
                            "Defence: " + playerHealth.cDefense.ToString() + "\n" +
                            "Weight: " + playerControls.mochilaPeso.ToString() + " / " + playerControls.mochilaMaxPeso.ToString();
    }
    private void ConsumableAct()
    {
        Image srC1 = consumable1.transform.GetChild(0).GetComponent<Image>();
        Image srC2 = consumable2.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI tC1 = consumable1.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI tC2 = consumable2.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (consumables[0] != null)
        {
            srC1.enabled = true;
            srC1.sprite = consumables[0].sct.sprite;
            tC1.text = consumables[0].stack.ToString();
        }
        else
        {
            srC1.enabled = false;
            tC1.text = "";
        }
        if (consumables[1] != null)
        {
            srC2.enabled = true;
            srC2.sprite = consumables[1].sct.sprite;
            tC2.text = consumables[1].stack.ToString();
        }
        else
        {
            srC2.enabled = false;
            tC2.text = "";
        }
    }
    private void Equipment(Item item)
    {
        if (item == null) return;
        int stack = item.stack;
        int lvl = Mathf.CeilToInt(item.lvl / 10f);
        playerAttack.damage += item.sct.eDamage * stack * lvl;
        if (item.sct.mDamage != 0) { if (item.sct.mDamage > 0) mDamage += item.sct.mDamage * stack * lvl; else mDamage += item.sct.mDamage * stack * (lvl * .4f + .4f); }
        if (item.sct.equipType != EquipType.PrimaryWeapon) { if (item.sct.eAttackSpeed > 0) playerAttack.attackSpeed += item.sct.eAttackSpeed * stack * lvl; else playerAttack.attackSpeed += item.sct.eAttackSpeed * stack * (lvl * .4f + .4f); }
        if (item.sct.mAttackSpeed != 0) { if (item.sct.mAttackSpeed > 0) mAttackSpeed += item.sct.mAttackSpeed * stack * lvl; else mAttackSpeed += item.sct.mAttackSpeed * stack * (lvl * .4f + .4f); }
        if (item.sct.equipType != EquipType.PrimaryWeapon) { if (item.sct.eAttackRange > 0) playerAttack.attackRange += item.sct.eAttackRange * stack * lvl; else playerAttack.attackRange += item.sct.eAttackRange * stack * (lvl * .4f + .4f); }
        if (item.sct.eSpeed > 0) playerControls.currentSpeed += item.sct.eSpeed * stack * lvl; else playerControls.currentSpeed += item.sct.eSpeed * stack * (lvl * .4f + .4f);
        if (item.sct.mSpeed != 0) { if (item.sct.mSpeed > 0) mSpeed += item.sct.mSpeed * stack * lvl; else mSpeed += item.sct.mSpeed * stack * (lvl * .4f + .4f); }
        if (item.sct.eHealth > 0) playerHealth.maxHealth += item.sct.eHealth * stack * lvl; else playerHealth.maxHealth += item.sct.eHealth * stack * (lvl * .4f + .4f);
        if (item.sct.eRQuant > 0) playerHealth.rQuant += item.sct.eRQuant * stack * lvl; else playerHealth.rQuant += item.sct.eRQuant * stack * (lvl * .4f + .4f);
        if (item.sct.eDefense > 0) playerHealth.cDefense += item.sct.eDefense * stack * lvl; else playerHealth.cDefense += item.sct.eDefense * stack * (lvl * .4f + .4f);
        if (item.sct.mDefense != 0) { if (item.sct.mDefense > 0) mDefense += item.sct.mDefense * stack * lvl; else mDefense += item.sct.mDefense * stack * (lvl * .4f + .4f); }
        if (item.sct.eWeight > 0) playerControls.mochilaMaxPeso += item.sct.eWeight * stack * lvl; else playerControls.mochilaMaxPeso += item.sct.eWeight * stack * (lvl * .4f + .4f);
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
        if (wep.sct.attackType == AttackType.meleeS) playerAttack.particlePref = slashEffect;
        if (wep.sct.attackType == AttackType.ranged) playerAttack.bullet = wep.sct.bulletPrefab;
        playerAttack.desviation = wep.sct.desviation;
        playerAttack.attackSpeed = wep.sct.eAttackSpeed;
        playerAttack.attackRange = wep.sct.eAttackRange;
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
                item.container.ActualizarPeso(-dar*item.sct.peso);
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
        // ActualizarStats();
    }
}