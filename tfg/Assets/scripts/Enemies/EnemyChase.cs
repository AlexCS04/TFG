using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : EnemyState
{
    public float chaseDistance;
    private Vector2 destination;
    public float speed;
    public FollowType followType;
    private float startTime;
    public float time => Time.time - startTime;
    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
        destination = transform.position;
        if (followType == FollowType.lateChase) destination = player.transform.position;
        else if (followType == FollowType.random) destination = new Vector2(Random.Range(.7f,RoomManager.WAGON_WIDHT-.7f)+(RoomManager.instance.actualWagon*RoomManager.WAGON_WIDHT), Random.Range(0.7f,RoomManager.WAGON_HEIGHT-0.7f));
    }
    public override void Do()
    {

        switch (followType)
        {
            case FollowType.straight:
                StraightFollow();
                break;
            case FollowType.drunk:
                DrunkFollow();
                break;
            case FollowType.lateChase:
                LateFollow();
                break;
            case FollowType.random:
                RandomFollow();
                break;
            case FollowType.noMove:
                NoMove();
                break;
        }



    }
    private void StraightFollow()
    {
        Vector3 randomOffset = Random.insideUnitSphere * 2f;
        destination = player.position + randomOffset;
        FaceDir();
        if (Vector3.Distance(player.position, transform.position) < chaseDistance) Exit();

    }
    private void DrunkFollow()
    {
        if (time > 0.3f)
        {
            startTime = Time.time;
            destination = new Vector2(
                player.position.x > transform.position.x ? transform.position.x + Random.Range(.5f, 5) : transform.position.x + Random.Range(-5, -.5f),
                player.position.y > transform.position.y ? transform.position.y + Random.Range(0.5f, 4) : transform.position.y + Random.Range(-4, -.5f)
            );
            // Debug.Log(destination);
        }
        FaceDir();
        if (Vector3.Distance(player.position, transform.position) < chaseDistance) Exit();


    }
    private void RandomFollow()
    {
        if (time > 3.5f || Vector2.Distance(transform.position, destination) <= 0.1f) Exit();
        FaceDir();
    }
    private void LateFollow()
    {
        if (Vector2.Distance(transform.position, destination) <= 0.1f) Exit();
        FaceDir();

    }
    private void NoMove()
    {
        if (time > chaseDistance) Exit();
    }
    public override void FixedDo()
    {
        if (followType != FollowType.noMove)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
    public override void Exit()
    {
        completed = true;
    }
    // private void FacePlayer()
    // {
    //     if (player.position.x > transform.position.x) transform.eulerAngles = new Vector3(0, 0, 0);
    //     else transform.eulerAngles = new Vector3(0, 180, 0);
    // }
    private void FaceDir()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        if (direction.x >= 0) transform.eulerAngles = new Vector3(0, 0, 0);
        else transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
public enum FollowType
{

    straight,
    drunk,
    lateChase,
    random,
    noMove
    
}