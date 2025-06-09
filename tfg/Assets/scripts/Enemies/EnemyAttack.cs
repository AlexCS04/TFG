using UnityEngine;

public class EnemyAttack : EnemyState
{
    public Attack attack;
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p, rb);
    }
    public override void Do()
    {
        attack.AttackAction(player.position);
        Exit();
    }
    public override void Exit()
    {
        completed = true;
    }
}