using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public bool completed{ get; protected set; }
    public Transform player{ get; protected set; }
    public Rigidbody2D rb{ get; protected set; }


    public virtual void Enter(Transform p, Rigidbody2D br)
    {
        player = p;
        rb = br;
    }
    public virtual void Do(){}
    public virtual void FixedDo(){}
    public virtual void Exit(){}
}
