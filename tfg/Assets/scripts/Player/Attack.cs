using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackSpeed; //get from weapon
    [SerializeField] protected Transform attackPoint; //get from weapon maybe
    public float attackRange; //get from weapon
    public float damage; //change get from weapon
    [SerializeField] protected LayerMask attackLayer;
    public AttackType attackType; //get from weapon
    public GameObject weapon; //change  Weapon holding
    protected float timeSinceAttack;

    public float bSpeed; //change get from weapon


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
        GameObject temp = Instantiate(weapon, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage);
        temp.GetComponent<Bullet>().Shoot(Input.mousePosition);
    }
    private void AttackMelee()
    {
        Collider2D[] c = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (Collider2D item in c)
        {
            item.GetComponent<Health>().TakeDamage(damage, damage);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
public enum AttackType
{
    melee,
    ranged
}
