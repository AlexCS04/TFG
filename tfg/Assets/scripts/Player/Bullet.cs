using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float tDamage;
    public float rDamage;
    public LayerMask layer;

    public int piercing = 1;

    public bool bouncer;

    public float desviation;

    private Rigidbody2D rb;

    private float bSpeed;

    private float startTime;
    public float time => Time.time - startTime;

    [SerializeField] private Transform help; //punto detras de la bala para solucionar algunas balas atravesando muros


    public void Born(LayerMask _layer, float speed, float _tDamage, float _rDamage, int _piercing, bool _bouncer, float _desviation)
    {
        startTime = Time.time;
        layer = _layer;
        bSpeed = speed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        tDamage = _tDamage;
        rDamage = _rDamage;
        piercing = _piercing;
        bouncer = _bouncer;
        desviation = _desviation;
    }

    public void Shoot(Vector2 targetPos)
    {
        Vector2 randomOffset = Random.insideUnitCircle * desviation;
        Vector2 vectorA = transform.position;
        // targetPos += randomOffset;
        Vector2 direction = new Vector2(targetPos.x - vectorA.x, targetPos.y - vectorA.y) * 50;

        direction = Vector2.ClampMagnitude(direction, 5f) + randomOffset;
        // direction = new Vector2(direction.x - vectorA.x, direction.y - vectorA.y)+randomOffset;
        // Debug.Log(direction);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        float max = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);

        rb.linearVelocity = new Vector2(direction.x / max * bSpeed, direction.y / max * bSpeed);
        AudioManager.PlaySound(EffectTypes.shoot);  

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if ((1 << collision.gameObject.layer) == layer.value || collision.tag.Equals("Obstacle")) //bit thingy. Move 1 coll.layers to the right and compare it with layer.value. coll.layer has a singular one on its bit form
        {
            collision.GetComponent<Health>().TakeDamage(tDamage, rDamage);
            piercing -= 1;
            if (piercing <= 0) KillBullet();
        }
        else if (collision.tag.Equals("Wall") && bouncer)Bounce(collision.ClosestPoint(help.position));
        else if (collision.tag.Equals("Wall")) KillBullet();

    }
    private void KillBullet()
    {
        Destroy(gameObject);
    }
    private void Bounce(Vector2 contactPoint)
    {

        Vector2 normal = ((Vector2)help.position - contactPoint).normalized;
        Vector2 newdir = Vector2.Reflect(rb.linearVelocity.normalized, normal);
        float angle = Mathf.Atan2(newdir.y, newdir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = newdir * bSpeed;
    }
    void Update()
    {
        if (time > 2.4) KillBullet();
    }

}
