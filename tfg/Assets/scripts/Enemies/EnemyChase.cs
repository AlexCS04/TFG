using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : EnemyState
{
    public float chaseDistance;
    private Vector2 destination;
    public float speed;
    public FollowType followType;
    public float curveHeight;
    private float startTime;
    public float time => Time.time - startTime;
    public override void Enter(Transform p, Rigidbody2D rb)
    {
        base.Enter(p, rb);
        startTime = Time.time;
    }
    public override void Do()
    {
        // completed = false;
        switch (followType)
        {
            case FollowType.straight:
                StraightFollow();
                break;
            case FollowType.random:
                RandomFollow();
                break;
        }
        
    }
    private void StraightFollow()
    {
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
    private void RandomFollow()
    {
        if (Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            if (time > 0.3f)
            {
                startTime = Time.time;
                destination = new Vector2(
                    player.position.x > transform.position.x ? transform.position.x+Random.Range(.5f,5) : transform.position.x+ Random.Range(-5,-.5f),
                    player.position.y > transform.position.y ? transform.position.y +Random.Range(0.5f,4) : transform.position.y + Random.Range(-4,-.5f)
                );
            }
            FacePlayer();
        }
        else Exit();
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
public enum FollowType {

    straight,
    random
}