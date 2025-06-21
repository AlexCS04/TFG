using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyState state;
    public Transform player;
    [SerializeField]private Rigidbody2D rb;

    public EnemyChase chaseState;
    public EnemyAttack attackState;
    public EnemyRetrieve retrieveState;
    public EnemyIdle idleState;
    private bool attacked;

    [SerializeField] private float chaseDistance;
    [SerializeField] private float retrieveDistance;
    [SerializeField] private float speed;

    void Start()
    {
        player = RoomManager.instance.player;
        // SelectState();
        state = idleState;
        chaseState.speed = speed;
        retrieveState.speed = speed;
        chaseState.chaseDistance = chaseDistance;
        retrieveState.retrieveDistance = retrieveDistance;
        SetUpState();
    }
    void Update()
    {
        if (state.completed) SelectState();
        state.Do();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity=0f;
        state.FixedDo();
    }
    private void SelectState()
    {
        // state = new EnemyIdle();
        if (chaseState.followType != FollowType.random && Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            state = chaseState;
            // Debug.Log("Chasing");
            // Debug.Log(Vector3.Distance(player.position, transform.position));
        }
        else if (chaseState.followType == FollowType.random && !attacked)
        {
            state = chaseState;
            attacked = true;
        }
        else if (Vector3.Distance(player.position, transform.position) < retrieveDistance)
        {
            state = retrieveState;
            // Debug.Log("Retriving");
        }
        else
        {
            state = attackState;
            attacked = false;
            // Debug.Log("Attacking");
        }
        SetUpState();

    }
    private void SetUpState()
    {
        state.Enter(player, rb);
    }



}