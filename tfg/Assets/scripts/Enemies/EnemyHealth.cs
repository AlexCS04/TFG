using UnityEngine;

public class EnemyHealth : Health
{
    public override void Die()
    {
        Eventmanager.OnEnemyDeath();
        ItemSpwnManager.instance.SpawnItem(pool, transform.position, RoomManager.instance.roomRandom);
        Destroy(gameObject);
    }
    public override void TakeDamage(float tHealth, float rHealth)
    {
        AudioManager.PlaySound(EffectTypes.hit);
        base.TakeDamage(tHealth, rHealth);

    }
}
