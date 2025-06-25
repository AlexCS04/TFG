using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackSpeed; //modify from weapon
    [SerializeField] protected Transform attackPoint; //get from weapon maybe
    public float attackRange; //modify from weapon
    public float damage; //modify from weapon
    public float secDamage = 1f; //regen attack
    [SerializeField] protected LayerMask attackLayer;
    public AttackType attackType; //get from weapon
    public GameObject bullet; //change  Weapon holding. maybe
    protected float timeSinceAttack;

    public GameObject particlePref;

    public float rangeY = 1;

    public float desviation;


    public float bSpeed; //change get from weapon
    public int bPiercing = 1;
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
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(target);
    }
    private void AttackMelee()
    {
        //characters
        Collider2D[] c;
        GameObject effect = Instantiate(particlePref, attackPoint.position, Quaternion.identity);
        Vector3 newScale = effect.transform.localScale;
        if (attackType == AttackType.meleeC)//circle attack
        {
            c = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
            newScale *= attackRange;
            AudioManager.PlaySound(EffectTypes.pound);
        }
        else //rectangle attack
        {
            c = Physics2D.OverlapBoxAll(new Vector3(attackPoint.position.x + attackRange / 2 * (transform.eulerAngles.y == 0 ? 1 : -1), attackPoint.position.y, 0), new Vector2(attackRange, rangeY), 0, attackLayer);
            newScale.x = attackRange;
            if (Random.Range(0, 2) == 0) AudioManager.PlaySound(EffectTypes.sword1); else AudioManager.PlaySound(EffectTypes.sword2); 

        }
        foreach (Collider2D item in c)
        {
            if (item.GetComponent<Health>())
                item.GetComponent<Health>().TakeDamage(damage, secDamage);
        }
        //obstaculos
        if (attackType == AttackType.meleeC)
            c = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Obstacles"));
        else
            c = Physics2D.OverlapBoxAll(new Vector3(attackPoint.position.x + attackRange / 2 * (transform.eulerAngles.y == 0 ? 1 : -1), attackPoint.position.y, 0), new Vector2(attackRange, rangeY), 0, LayerMask.GetMask("Obstacles"));

        foreach (Collider2D item in c)
        {
            if (item.GetComponent<Health>())
                item.GetComponent<Health>().TakeDamage(damage, damage);
        }
        effect.transform.localScale = newScale;
        if (transform.eulerAngles.y == 0)
        {
            effect.GetComponent<ParticleSystemRenderer>().flip = Vector3.zero;
            effect.GetComponent<ParticleSystemRenderer>().pivot = Vector3.zero;
        }
        else
        {
            effect.GetComponent<ParticleSystemRenderer>().flip = Vector3.right;
            effect.GetComponent<ParticleSystemRenderer>().pivot = Vector3.left;
        }
    }
    void OnDrawGizmos()
    {
        if (attackType == AttackType.meleeC)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        else Gizmos.DrawWireCube(new Vector3(attackPoint.position.x + attackRange / 2 * (transform.eulerAngles.y == 0 ? 1 : -1), attackPoint.position.y, 0), new Vector2(attackRange, 3f));
    }


    public void AttackPattern(int pattern)
    {
        if (timeSinceAttack > 0) return;
        if (Time.timeScale == 0) return;

        timeSinceAttack = attackSpeed;
        switch (pattern)
        {
            case 1:
                PlusAttack();
                break;
            case 2:
                CrossAttack();
                break;
            case 3:
                PlusAttack();
                CrossAttack();
                break;
            case 4:
                RandomShoot(3);
                break;
            case 5:
                RandomShoot(6);
                break;
            case -1:
                StartCoroutine("BossPAttack1");
                break;
            case -2:
                StartCoroutine("BossPAttack2");
                break;
            case -3:
                StartCoroutine("BossPAttack3");
                break;
            case -10:
                StartCoroutine("BossPAttack10");
                break;
            case -20:
                StartCoroutine("BossPAttack20");
                break;
            case -30:
                StartCoroutine("BossPAttack30");
                break;

        }


    }

    private void PlusAttack()
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x + 1, transform.position.y));
        temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x - 1, transform.position.y));
        temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x, transform.position.y + 1));
        temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x, transform.position.y - 1));
    }
    private void CrossAttack()
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x + 1, transform.position.y + 1));
        temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x - 1, transform.position.y + 1));
        temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x + 1, transform.position.y - 1));
        temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
        temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x - 1, transform.position.y - 1));
    }
    private void RandomShoot(int quant)
    {
        for (int i = 0; i < quant; i++)
        {
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot(new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)));
        }
    }


    #region BossPatternAttacks

    IEnumerator BossPAttack1()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 1; i <= 32; i++)
        {
            float angulo = 2 * Mathf.PI * i / 32;
            Vector2 pos = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
            Debug.Log(pos);
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot((Vector2)transform.position + pos);
        }
    }
    IEnumerator BossPAttack2()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 8; i++)
        {

            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot(RoomManager.instance.player.position);
        }
    }
    IEnumerator BossPAttack3()
    {
        yield return new WaitForSeconds(0.4f);
        attackType = AttackType.meleeC;
        attackPoint = transform;
        AttackMelee();
    }
    IEnumerator BossPAttack10()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 1; i <= 32; i++)
        {
            float angulo = 2 * Mathf.PI * i / 32;
            Vector2 pos = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
            Debug.Log(pos);
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot((Vector2)transform.position + pos);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 1; i <= 9; i++)
        {
            float angulo = 2 * Mathf.PI * i / 32;
            Vector2 pos = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
            Debug.Log(pos);
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot((Vector2)transform.position + pos);
        }
    }
    IEnumerator BossPAttack20()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 8; i++)
        {

            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot(RoomManager.instance.player.position);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 8; i++)
        {

            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Born(attackLayer, bSpeed, damage, secDamage, bPiercing, bBounce, desviation);
            temp.GetComponent<Bullet>().Shoot(RoomManager.instance.player.position);
        }
    }
    IEnumerator BossPAttack30()
    {
        yield return new WaitForSeconds(0.4f);
        attackType = AttackType.meleeC;
        attackPoint = transform;
        AttackMelee();
        CrossAttack();
        PlusAttack();
    }

    #endregion
}

public enum AttackType
{
    meleeC,
    ranged,
    meleeS
}
