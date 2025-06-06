using UnityEngine;

public class EnemyIdle : EnemyState
{
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p,rb);
    }
    public override void Do()
    {
        completed = true;
    }
    public override void Exit()
    {
        completed = true;
    }
}