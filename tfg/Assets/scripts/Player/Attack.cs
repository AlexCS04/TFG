using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackSpeed; //modify from weapon
    [SerializeField] protected Transform attackPoint; //get from weapon maybe
    public float attackRange; //modify from weapon
    public float damage; //modify from weapon
    public float secDamage=1f; //regen attack
    [SerializeField] protected LayerMask attackLayer;
    public AttackType attackType; //get from weapon
    public GameObject bullet; //change  Weapon holding. maybe
    protected float timeSinceAttack;

    [SerializeField] private bool isPlayer;

    public float bSpeed; //change get from weapon
    public int bPiercing=1;
    public bool bBounce;


    void Update()
    {
        if (timeSinceAttack > 0) timeSinceAttack -= Time.deltaTime;
    }
    public virtual void AttackAction(Vector3 target)
    {
        if (timeSinceAttack > 0) return;
        if (Time.timeScale == 0) return;

        timeSinceAttack = attackSpeed;
        if (attackType == AttackType.ranged) AttackRanged(target);
        else AttackMelee();
    }
    private void AttackRanged(Vector3 target)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, bPiercing, bBounce);
        temp.GetComponent<Bullet>().Shoot(target, isPlayer);
    }
    private void AttackMelee()
    {
        Collider2D[] c = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (Collider2D item in c)
        {
            item.GetComponent<Health>().TakeDamage(damage, secDamage);
        }
        Collider2D[] obs = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Obstacles"));
        foreach (Collider2D item in obs)
        {
            item.GetComponent<Health>().TakeDamage(damage, damage*.8f);
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
