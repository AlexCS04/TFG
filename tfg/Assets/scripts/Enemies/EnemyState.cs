using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public bool completed{ get; protected set; }
    public Transform player{ get; protected set; }
    public Rigidbody2D rb{ get; protected set; }


    public virtual void Enter()
    {
        player = RoomManager.instance.player;
        rb = GetComponent<Rigidbody2D>();
        completed = false;
    }
    public virtual void Do(){}
    public virtual void FixedDo(){}
    public virtual void Exit()
    {
        completed = true;
    }
}
