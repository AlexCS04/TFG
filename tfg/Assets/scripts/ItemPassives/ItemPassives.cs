using UnityEngine;
[System.Serializable]
public abstract class ItemPassives
{
    public abstract string Givename();
    public virtual void Update(Player player){}
}
public class HealingItem : ItemPassives
{
    public override string Givename()
    {
        return "Pepe";
    }
    public override void Update(Player player)
    {
        
    }
}
