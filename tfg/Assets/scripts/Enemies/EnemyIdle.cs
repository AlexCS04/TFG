using UnityEngine;

public class EnemyIdle : EnemyState
{
    private float startTime;
    public float time => Time.time - startTime;
    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }
    public override void Do()
    {
        if (time > 1.5f) Exit();
    }
    public override void Exit()
    {
        completed = true;
    }
}