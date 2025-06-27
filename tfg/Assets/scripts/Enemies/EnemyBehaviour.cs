using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyState state;
    public Transform player;
    [SerializeField] private Rigidbody2D rb;

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
        Scale(RoomManager.instance.wagonCount);
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
        rb.angularVelocity = 0f;
        state.FixedDo();
    }
    private void SelectState()
    {
        if (chaseState.followType != FollowType.random &&chaseState.followType != FollowType.noMove&& Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            state = chaseState;
        }
        else if ((chaseState.followType == FollowType.random || chaseState.followType == FollowType.noMove) && !attacked)
        {
            state = chaseState;
            attacked = true;
        }
        else if (Vector3.Distance(player.position, transform.position) < retrieveDistance)
        {
            state = retrieveState;
        }
        else
        {
            state = attackState;
            attacked = false;
        }
        SetUpState();

    }
    private void SetUpState()
    {
        state.Enter();
    }
    private void Scale(int lvl)
    {

        float progre = lvl * 0.5f;
        int bonus = lvl / 10 * 5;
        float aditivo = 1 + (progre + bonus)/18;

        Attack attack = transform.GetComponent<Attack>();
        Health health = transform.GetComponent<Health>();

        attack.damage *= aditivo;
        attack.secDamage *= aditivo;

        health.bDefense *= aditivo;
        health.maxHealth *= aditivo;
    }



}