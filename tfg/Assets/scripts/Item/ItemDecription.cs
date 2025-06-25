using UnityEngine;

public class ItemDecription : ISendDesc
{
    void Start()
    {
        GetDescription();
    }
    private void GetDescription()
    {
        Item item = GetComponent<Item>();
        SCT sct = item.sct;
        string description = "";
        description += "<color=#ff0000>" + sct.Name + "</color>\n";
        if(sct.Name!="Money")
        description += "Level: <color=#0000ff>" + Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";

        // description += "\n";
        if (sct.equipType != EquipType.Consumable)
        {
            #region Inv
            description += "<color=#005500>On inventory:</color>\n";
            if (sct.iAttackRange != 0)
            {
                description += "Attack Range: ";
                description += PosOrNeg(sct.iAttackRange);
                description += sct.iAttackRange * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iAttackSpeed != 0)
            {
                description += "Attack Speed: ";
                description += PosOrNeg(-sct.iAttackSpeed);
                description += sct.iAttackSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iDamage != 0)
            {
                description += "Damage: ";
                description += PosOrNeg(sct.iDamage);
                description += sct.iDamage * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iDefense != 0)
            {
                description += "Defense: ";
                description += PosOrNeg(sct.iDefense);
                description += sct.iDefense * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iHealth != 0)
            {
                description += "Health: ";
                description += PosOrNeg(sct.iHealth);
                description += sct.iHealth * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iRQuant != 0)
            {
                description += "Regen Quantity: ";
                description += PosOrNeg(sct.iRQuant);
                description += sct.iRQuant * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iSpeed != 0)
            {
                description += "Speed: ";
                description += PosOrNeg(sct.iSpeed);
                description += sct.iSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.iWeight != 0)
            {
                description += "Carry Weight: ";
                description += PosOrNeg(sct.iWeight);
                description += sct.iWeight * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            #endregion
            #region Equip
            if (sct.equipType != EquipType.Extra)
            {
                description += "<color=#005500>Equiped:</color>\n";
                if (sct.attackType != AttackType.ranged && sct.eAttackRange != 0)
                {
                    description += "Attack Range: ";
                    description += PosOrNeg(sct.eAttackRange);
                    description += sct.eAttackRange * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eAttackSpeed != 0)
                {
                    description += "Attack Speed: ";
                    if (sct.equipType == EquipType.PrimaryWeapon) description += PosOrNeg(sct.eAttackSpeed);
                    else description += PosOrNeg(-sct.eAttackSpeed);
                    description += sct.eAttackSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eDamage != 0)
                {
                    description += "Damage: ";
                    description += PosOrNeg(sct.eDamage);
                    description += sct.eDamage * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eDefense != 0)
                {
                    description += "Defense: ";
                    description += PosOrNeg(sct.eDefense);
                    description += sct.eDefense * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eHealth != 0)
                {
                    description += "Health: ";
                    description += PosOrNeg(sct.eHealth);
                    description += sct.eHealth * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eRQuant != 0)
                {
                    description += "Regen Quantity: ";
                    description += PosOrNeg(sct.eRQuant);
                    description += sct.eRQuant * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eSpeed != 0)
                {
                    description += "Speed: ";
                    description += PosOrNeg(sct.eSpeed);
                    description += sct.eSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.eWeight != 0)
                {
                    description += "Carry Weight: ";
                    description += PosOrNeg(sct.eWeight);
                    description += sct.eWeight * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                }
                if (sct.attackType == AttackType.ranged)
                {
                    if (sct.bPiercing > 1)
                    {
                        description += "Piercing: ";
                        description += PosOrNeg(sct.bPiercing);
                        description += sct.bPiercing * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                    }
                    description += "Bullet Speed: ";
                    description += PosOrNeg(sct.bSpeed);
                    description += sct.bSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
                    if (sct.bBounce)
                    {
                        description += "Bullets bounce\n";
                    }
                }
            }
            #endregion
            #region mults
            description += "<color=#005500>Mults:</color>\n";
            if (sct.mAttackSpeed != 0)
            {
                description += "Attack Speed Mult: ";
                description += PosOrNeg(sct.mAttackSpeed);
                description += sct.mAttackSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.mDamage != 0)
            {
                description += "Damage Mult: ";
                description += PosOrNeg(sct.mDamage);
                description += sct.mDamage * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.mDefense != 0)
            {
                description += "Defense Mult: ";
                description += PosOrNeg(sct.mDefense);
                description += sct.mDefense * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            if (sct.mSpeed != 0)
            {
                description += "Speed Mult: ";
                description += PosOrNeg(sct.mSpeed);
                description += sct.mSpeed * Mathf.CeilToInt(item.lvl / 10f) + "</color>\n";
            }
            #endregion
        }
        else if (sct.equipType == EquipType.Consumable)
        {
            description += "When consumed:\n";
            if (sct.tHealth != 0)
            {
                description += "Heals ";
                description += PosOrNeg(sct.tHealth);
                description += sct.tHealth * Mathf.CeilToInt(item.lvl / 10f) + "</color>";
                description += " hp\n";
            }
            if (sct.rHealth != 0)
            {
                description += "Gives ";
                description += PosOrNeg(sct.rHealth);
                description += sct.rHealth * Mathf.CeilToInt(item.lvl / 10f) + "</color>";
                description += " regen points\n";
            }
            description += "Fills ";
            description += sct.filling;
            description += " food points\n";
            if (sct.tempDamageMult != 1)
            {
                description += "Grants a damage multiplier\nOf ";
                description += PosOrNeg(sct.tempDamageMult - 1);
                description += sct.tempDamageMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempDefenseMult != 1)
            {
                description += "Grants a defense multiplier\nOf ";
                description += PosOrNeg(sct.tempDefenseMult - 1);
                description += sct.tempDefenseMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempSpeedMult != 1)
            {
                description += "Grants a speed multiplier\nOf ";
                description += PosOrNeg(sct.tempSpeedMult - 1);
                description += sct.tempSpeedMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempRegenMult != 1)
            {
                description += "Grants a regen quantity multiplier\nOf ";
                description += PosOrNeg(sct.tempRegenMult - 1);
                description += sct.tempRegenMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempAttackSpeedMult != 1)
            {
                description += "Grants a attack speed multiplier\nOf ";
                description += PosOrNeg(sct.tempAttackSpeedMult-1);
                description += 1/sct.tempAttackSpeedMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            
        }

        description += "Weight: ";
        description += sct.peso.ToString()+"\n";
            desc = description;
    }
    private string PosOrNeg(float data)
    {
        if (data < 0)
            return "<color=#E40000>";
        else
            return "<color=#00A200>";
    }
}
