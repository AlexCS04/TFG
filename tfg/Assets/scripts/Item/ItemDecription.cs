using UnityEngine;

public class ItemDecription : ISendDesc
{
    void Start()
    {
        GetDescription();
    }
    private void GetDescription()
    {
        SCT sct = GetComponent<Item>().sct;
        string description = "";
        description += "Name: <color=#ff0000>" + sct.Name + "</color>\n";
        description += "Get\n";
        // description += "\n";
        description += "<color=#005500>On inventory:</color>\n";
        if (sct.iAttackRange != 0)
        {
            description += "Attack Range: ";
            description += PosOrNeg(sct.iAttackRange);
            description += sct.iAttackRange + "</color>\n";
        }
        description += "<color=#005500>Equiped:</color>\n";
        if (sct.eAttackRange != 0)
        {
            description += "Attack Range: ";
            description += PosOrNeg(sct.eAttackRange);
            description += sct.eAttackRange + "</color>\n";
        }



            desc = description;
    }
    private string PosOrNeg(float data)
    {
        if (data < 0)
            return "<color=#ff0000>";
        else
            return "<color=#00ff00>";
    }
}
