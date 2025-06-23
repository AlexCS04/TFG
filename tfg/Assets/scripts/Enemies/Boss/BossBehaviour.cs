using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Attack selectedAttack { get; private set; }
    public int selectedPattern { get; private set; }

    [SerializeField] private List<Attack> attacks;
    [SerializeField] private List<int> patterns;
    [SerializeField] private int chaseDistance;

    private BossState state;
    private Health health;
    [SerializeField]private BossState idleState;
    [SerializeField]private BossState chaseState;
    [SerializeField]private BossState attackState;
    // [SerializeField]private BossState retrieve;
    private Transform player;
    private bool attacked;
    [SerializeField]private Animator animator;

    void Start()
    {
        Scale(RoomManager.instance.wagonCount);
        // SelectState();
        player = RoomManager.instance.player;
        state = idleState;
        SetUpState();
    }
    void Update()
    {
        if (state.completed) SelectState();
        state.Do();
    }
    void FixedUpdate()
    {
        state.FixedDo();
    }
    private void SelectState()
    {
        int attPat = Random.Range(0, patterns.Count);
        selectedAttack = attacks[attPat];
        selectedPattern = patterns[attPat];
        if (health.currentHealth < health.maxHealth) selectedPattern *= 10;
        if (attacked && Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            state = chaseState;
            attacked = false;
            SetUpState();
        }
        else{

            StartCoroutine("Attacking");
        }

    }
    IEnumerator Attacking()
    {
        animator.Play("ChargeAttack");
        yield return new WaitForSeconds(0.7f);
        state = attackState;
        attacked = true;
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
        float aditivo = 1 + (progre + bonus) / 18;

        Attack[] attacks = transform.GetComponents<Attack>();
        Health health = transform.GetComponent<Health>();

        foreach (Attack attack in attacks)
        {
            attack.damage *= aditivo;
            attack.secDamage *= aditivo;
        }

        health.bDefense *= aditivo;
        health.maxHealth *= aditivo;
    }
}