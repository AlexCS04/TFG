using UnityEngine;

public class EnemyRetrieve : EnemyState
{
    public float retrieveDistance;
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p, rb);
    }
    public override void Do()
    {
        Debug.Log("Retrieving");
        Exit();
    }
    public override void Exit()
    {
        completed = true;
    }
}