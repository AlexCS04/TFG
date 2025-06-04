using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackSpeed;
    [SerializeField] protected Transform attackPoint;
    public float attackRange;
    public float damage; //change
    [SerializeField] protected LayerMask attackLayer;
    public AttackType attackType;
    public GameObject weapon;
    protected float timeSinceAttack;


    void Update()
    {
        if (timeSinceAttack > 0) timeSinceAttack -= Time.deltaTime;
    }
    public virtual void AttackAction()
    {
        if (timeSinceAttack > 0) return;

        timeSinceAttack = attackSpeed;
        if (attackType == AttackType.ranged) AttackRanged();
        else AttackMelee();
    }
    private void AttackRanged()
    {
        
    }
    private void AttackMelee()
    {
        Collider2D[] c = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (Collider2D item in c)
        {
            item.GetComponent<Health>().TakeDamage(damage, damage);
        }
    }

}
public enum AttackType
{
    melee,
    ranged
}
