using UnityEngine;

public class EnemyHealth : Health
{
    public override void Die()
    {
        Eventmanager.OnEnemyDeath();
        base.Die();
    }
}
