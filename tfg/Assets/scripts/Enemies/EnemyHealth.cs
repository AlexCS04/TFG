using UnityEngine;

public class EnemyHealth : Health
{
    public override void Die()
    {
        Eventmanager.OnEnemyDeath();
        base.Die();
    }
    public override void TakeDamage(float tHealth, float rHealth)
    {
        AudioManager.PlaySound(EffectTypes.hit);
        base.TakeDamage(tHealth, rHealth);

    }
}
