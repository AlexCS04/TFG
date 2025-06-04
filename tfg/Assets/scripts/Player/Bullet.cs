using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public LayerMask layer;

    public int piercing = 1;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layer)
        {
            collision.GetComponent<Health>().TakeDamage(damage, damage);
            piercing -= 1;
            if (piercing <= 0) KillBullet();
        }
        else if (collision.tag.Equals("Wall")) KillBullet();
    }
    private void KillBullet()
    {
        Destroy(gameObject);
    }
}
