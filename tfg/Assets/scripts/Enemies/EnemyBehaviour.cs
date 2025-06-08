using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyState state;
    public Transform player;
    [SerializeField]private Rigidbody2D rb;

    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState retrieveState;

    [SerializeField] private float chaseDistance;
    [SerializeField] private float retrieveDistance;

    void Start()
    {
        player = RoomManager.instance.player;
        SetUpStates();
        SelectState();
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
        if (Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            state = chaseState;
            Debug.Log("Chasing");
            Debug.Log(Vector3.Distance(player.position, transform.position));
        }
        else if (Vector3.Distance(player.position, transform.position) < retrieveDistance)
        {
            state = retrieveState;
            Debug.Log("Retriving");
        }
        else 
        {
            state = attackState;
            Debug.Log("Attacking");
        }

    }
    private void SetUpStates()
    {
        chaseState.Enter(player, rb);
    }



}