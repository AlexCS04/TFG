using UnityEngine;

public class EnemyRetrieve : EnemyState
{
    public float retrieveDistance;
    private Vector2 destination;
    public float speed;
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p, rb);
    }
    public override void Do()
    {
        if (Vector3.Distance(player.position, transform.position) < retrieveDistance)
        {
            Debug.Log("Retrieving");
            destination = new Vector2(
                player.position.x > transform.position.x ? transform.position.x - 2 : transform.position.x + 5,
                player.position.y > transform.position.y ? transform.position.y - 2 : transform.position.y + 5
                );
        }
        else Exit();
    }
    public override void Exit()
    {
        completed = true;
    }
    public override void FixedDo()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    //destination = new Vector2(player.position.x > transform.position.x ? transform.position.x-2:transform.position.x+5,player.position.y > transform.position.y ? transform.position.y-2:transform.position.y+5);
}