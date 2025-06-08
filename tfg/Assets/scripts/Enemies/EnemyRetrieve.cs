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
    //destination = new Vector2(player.position.x > transform.position.x ? transform.position.x-2:transform.position.x+5,player.position.y > transform.position.y ? transform.position.y-2:transform.position.y+5);
}