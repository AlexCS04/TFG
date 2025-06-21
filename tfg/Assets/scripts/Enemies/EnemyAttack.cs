using UnityEngine;

public class EnemyAttack : EnemyState
{
    public Attack attack;
    public int attackPattern;
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p, rb);
    }
    public override void Do()
    {
        if(attackPattern==0)
                attack.AttackAction(player.position);
        else
                attack.AttackPattern(attackPattern);        
        Exit();
    }
    public override void Exit()
    {
        completed = true;
    }
}