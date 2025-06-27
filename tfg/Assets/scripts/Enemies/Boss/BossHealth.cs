using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private SCT money;
    public override void Die()
    {
        // Eventmanager.OnEnemyDeath();
        ItemSpwnManager.instance.SpawnItem(money, transform.position + Random.insideUnitSphere, RoomManager.instance.roomRandom.Next(10, 30));
        ItemSpwnManager.instance.SpawnItem(RoomManager.instance.consumPool, transform.position + Random.insideUnitSphere, RoomManager.instance.roomRandom);
        base.Die();
    }

}
