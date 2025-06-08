using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : EnemyState
{
    public float chaseDistance;
    private Vector2 destination;
    public float speed;
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p, rb);
    }
    public override void Do()
    {
        completed = false;
        if (Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            Vector3 randomOffset = Random.insideUnitSphere * 2f;
            destination = player.position + randomOffset;
            FacePlayer();
        }
        else
        {
            Exit();
        }
    }
    public override void FixedDo()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    public override void Exit()
    {
        // Debug.Log("exit");
        
        // destination = transform.position;
        completed = true;
    }
    private void FacePlayer()
    {
        if (player.position.x > transform.position.x) transform.eulerAngles = new Vector3(0, 180, 0);
        else transform.eulerAngles = new Vector3(0, 0, 0);
    }
}