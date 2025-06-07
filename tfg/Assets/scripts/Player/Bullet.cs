using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public LayerMask layer;

    public int piercing = 1;

    public bool bouncer;

    private Rigidbody2D rb;

    private float bSpeed;

    private float startTime;
    public float time => Time.time - startTime;


    public void Born(LayerMask _layer, float speed, float _damage)
    {
        startTime = Time.time;
        layer = _layer;
        bSpeed = speed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        damage = _damage;
    }
    public void Shoot(Vector2 vectorB)
    {
        Vector2 vectorA = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(vectorB);
        Vector2 direction = new Vector2(mousePos.x - vectorA.x, mousePos.y - vectorA.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        float max = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);

        rb.linearVelocity = new Vector2(direction.x/max*bSpeed,direction.y/max*bSpeed);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if ((1 << collision.gameObject.layer) == layer.value) //bit thingy. Move 1 coll.layers to the right and compare it with layer.value. coll.layer has a singular one on its bit form
        {
            collision.GetComponent<Health>().TakeDamage(damage, damage);
            piercing -= 1;
            if (piercing <= 0) KillBullet();
        }
        else if (collision.tag.Equals("Wall") && bouncer) Bounce();
        else if (collision.tag.Equals("Wall")) KillBullet();
    }
    private void KillBullet()
    {
        Destroy(gameObject);
    }
    private void Bounce() { }
    void Update()
    {
        if (time > 2) KillBullet();
    }

}
