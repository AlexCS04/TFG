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
        int lvl = Mathf.CeilToInt(item.lvl/10f);
        string description = "";
        description += "<color=#ff0000>" + sct.Name + "</color>\n";
        if (sct.Name != "Money")
            description += "Level: <color=#0000ff>" + lvl + "</color>\n";

        // description += "\n";
        if (sct.equipType != EquipType.Consumable)
        {
            #region Inv
            description += "<color=#005500><b>On inventory:</b></color>\n";
            if (sct.iAttackRange != 0)
            {
                description += "Attack Range: ";
                if (PosOrNeg(sct.iAttackRange))
                {
                    description += "<color=#00A200>";
                    description += sct.iAttackRange * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iAttackRange * (lvl * .4f + .4f) + "</color>\n";
                }
                
            }
            if (sct.iAttackSpeed != 0)
            {
                description += "Attack Speed: ";
                if (PosOrNeg(-sct.iAttackSpeed))
                {
                    description += "<color=#00A200>";
                    description += sct.iAttackSpeed * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iAttackSpeed * (lvl * .4f + .4f) + "</color>\n";
                }
                
            }
            if (sct.iDamage != 0)
            {
                description += "Damage: ";
                if (PosOrNeg(sct.iDamage))
                {
                    description += "<color=#00A200>";
                    description += sct.iDamage * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iDamage * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.iDefense != 0)
            {
                description += "Defense: ";
                if (PosOrNeg(sct.iDefense))
                {
                    description += "<color=#00A200>";
                    description += sct.iDefense * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iDefense * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.iHealth != 0)
            {
                description += "Health: ";
                if (PosOrNeg(sct.iHealth))
                {
                    description += "<color=#00A200>";
                    description += sct.iHealth * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iHealth * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.iRQuant != 0)
            {
                description += "Regen Quantity: ";
                if (PosOrNeg(sct.iRQuant))
                {
                    description += "<color=#00A200>";
                    description += sct.iRQuant * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iRQuant * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.iSpeed != 0)
            {
                description += "Speed: ";
                if (PosOrNeg(sct.iSpeed))
                {
                    description += "<color=#00A200>";
                    description += sct.iSpeed * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iSpeed * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.iWeight != 0)
            {
                description += "Carry Weight: ";
                if (PosOrNeg(sct.iWeight))
                {
                    description += "<color=#00A200>";
                    description += sct.iWeight * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.iWeight * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            #endregion
            #region Equip
            if (sct.equipType != EquipType.Extra)
            {
                description += "<color=#005500><b>Equiped:</b></color>\n";
                if (sct.attackType != AttackType.ranged && sct.eAttackRange != 0)
                {
                    description += "Attack Range: ";
                    if (sct.equipType != EquipType.PrimaryWeapon)
                    {
                        if (PosOrNeg(sct.eAttackRange))
                        {
                            description += "<color=#00A200>";
                            description += sct.eAttackRange * lvl + "</color>\n";
                        }
                        else
                        {
                            description += "<color=#E40000>";
                            description += sct.eAttackRange * (lvl * .4f + .4f) + "</color>\n";
                        }
                    }
                    else
                        description += sct.eAttackRange + "</color>\n";
                }
                if (sct.eAttackSpeed != 0)
                {
                    description += "Attack Speed: ";
                    if (sct.equipType != EquipType.PrimaryWeapon)
                    {
                        if (PosOrNeg(-sct.eAttackSpeed))
                        {
                            description += "<color=#00A200>";
                            description += sct.eAttackSpeed * lvl + "</color>\n";
                        }
                        else
                        {
                            description += "<color=#E40000>";
                            description += sct.eAttackSpeed * (lvl * .4f + .4f) + "</color>\n";
                        }
                    }
                    else
                        description += sct.eAttackRange + "</color>\n";
                }
                if (sct.eDamage != 0)
                {
                    description += "Damage: ";
                    if (PosOrNeg(sct.eDamage))
                    {
                        description += "<color=#00A200>";
                        description += sct.eDamage * lvl + "</color>\n";
                    }
                    else
                    {
                        description += "<color=#E40000>";
                        description += sct.eDamage * (lvl * .4f + .4f) + "</color>\n";
                    }
                        
                }
                if (sct.eDefense != 0)
                {
                    description += "Defense: ";
                    if (PosOrNeg(sct.eDefense))
                    {
                        description += "<color=#00A200>";
                        description += sct.eDefense * lvl + "</color>\n";
                    }
                    else
                    {
                        description += "<color=#E40000>";
                        description += sct.eDefense * (lvl * .4f + .4f) + "</color>\n";
                    }
                }
                if (sct.eHealth != 0)
                {
                    description += "Health: ";
                    if (PosOrNeg(sct.eHealth))
                    {
                        description += "<color=#00A200>";
                        description += sct.eHealth * lvl + "</color>\n";
                    }
                    else
                    {
                        description += "<color=#E40000>";
                        description += sct.eHealth * (lvl * .4f + .4f) + "</color>\n";
                    }
                }
                if (sct.eRQuant != 0)
                {
                    description += "Regen Quantity: ";
                    if (PosOrNeg(sct.eRQuant))
                    {
                        description += "<color=#00A200>";
                        description += sct.eRQuant * lvl + "</color>\n";
                    }
                    else
                    {
                        description += "<color=#E40000>";
                        description += sct.eRQuant * (lvl * .4f + .4f) + "</color>\n";
                    }
                }
                if (sct.eSpeed != 0)
                {
                    description += "Speed: ";
                    if (PosOrNeg(sct.eSpeed))
                    {
                        description += "<color=#00A200>";
                        description += sct.eSpeed * lvl + "</color>\n";
                    }
                    else
                    {
                        description += "<color=#E40000>";
                        description += sct.eSpeed * (lvl * .4f + .4f) + "</color>\n";
                    }
                }
                if (sct.eWeight != 0)
                {
                    description += "Carry Weight: ";
                    if (PosOrNeg(sct.eWeight))
                    {
                        description += "<color=#00A200>";
                        description += sct.eWeight * lvl + "</color>\n";
                    }
                    else
                    {
                        description += "<color=#E40000>";
                        description += sct.eWeight * (lvl * .4f + .4f) + "</color>\n";
                    }
                }
                if (sct.attackType == AttackType.ranged)
                {
                    if (sct.bPiercing > 1)
                    {
                        description += "Piercing: ";
                        description += sct.bPiercing + "</color>\n";
                    }
                    description += "Bullet Speed: ";
                    description += sct.bSpeed+"</color>\n";
                    description += "Desviation: ";
                    description += Desviation(sct.desviation);
                    description += sct.desviation + "</color>\n";
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
                if (PosOrNeg(sct.mAttackSpeed))
                {
                    description += "<color=#00A200>";
                    description += sct.mAttackSpeed * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.mAttackSpeed * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.mDamage != 0)
            {
                description += "Damage Mult: ";
                if (PosOrNeg(sct.mDamage))
                {
                    description += "<color=#00A200>";
                    description += sct.mDamage * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.mDamage * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.mDefense != 0)
            {
                description += "Defense Mult: ";
                if (PosOrNeg(sct.mDefense))
                {
                    description += "<color=#00A200>";
                    description += sct.mDefense * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.mDefense * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            if (sct.mSpeed != 0)
            {
                description += "Speed Mult: ";
                if (PosOrNeg(sct.mSpeed))
                {
                    description += "<color=#00A200>";
                    description += sct.mSpeed * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.mSpeed * (lvl * .4f + .4f) + "</color>\n";
                }
            }
            #endregion
        }
        else if (sct.equipType == EquipType.Consumable)
        {
            description += "When consumed:\n";
            if (sct.tHealth != 0)
            {
                description += "Heals ";
                if (PosOrNeg(sct.tHealth))
                {
                    description += "<color=#00A200>";
                    description += sct.tHealth * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.tHealth * (lvl * .4f + .4f) + "</color>\n";
                }
                description += " hp\n";
            }
            if (sct.rHealth != 0)
            {
                description += "Gives ";
                if (PosOrNeg(sct.rHealth))
                {
                    description += "<color=#00A200>";
                    description += sct.rHealth * lvl + "</color>\n";
                }
                else
                {
                    description += "<color=#E40000>";
                    description += sct.rHealth * (lvl * .4f + .4f) + "</color>\n";
                }
                description += " regen points\n";
            }
            description += "Fills ";
            description += sct.filling;
            description += " food points\n";
            if (sct.tempDamageMult != 1)
            {
                description += "Grants a damage multiplier\nOf ";
                description += GoodBad(sct.tempDamageMult - 1);
                description += sct.tempDamageMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempDefenseMult != 1)
            {
                description += "Grants a defense multiplier\nOf ";
                description += GoodBad(sct.tempDefenseMult - 1);
                description += sct.tempDefenseMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempSpeedMult != 1)
            {
                description += "Grants a speed multiplier\nOf ";
                description += GoodBad(sct.tempSpeedMult - 1);
                description += sct.tempSpeedMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempRegenMult != 1)
            {
                description += "Grants a regen quantity multiplier\nOf ";
                description += GoodBad(sct.tempRegenMult - 1);
                description += sct.tempRegenMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }
            if (sct.tempAttackSpeedMult != 1)
            {
                description += "Grants a attack speed multiplier\nOf ";
                description += GoodBad(sct.tempAttackSpeedMult - 1);
                description += 1 / sct.tempAttackSpeedMult + "</color>";
                description += " during ";
                description += sct.consumableExtraTime + " seconds\n";
            }

        }

        description += "Weight: ";
        description += sct.peso.ToString() + "\n";
        desc = description;
    }
    private bool PosOrNeg(float data)
    {
        if (data < 0)
            return false;
            // return "<color=#E40000>";
        else
            return true;
            // return "<color=#00A200>";
    }
    private string GoodBad(float data)
    {
        if (data < 0)
            return "<color=#E40000>";
        else
            return "<color=#00A200>";
    }
    private string Desviation(float data)
    {
        if (data != 0)
            return "<color=#E40000>";
        else
            return "<color=#00A200>";
    }
}
