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

    private EnemyState state;
    private Health health;
    [SerializeField]private EnemyState idleState;
    [SerializeField]private EnemyChase chaseState;
    [SerializeField]private EnemyState attackState;
    // [SerializeField]private BossState retrieve;
    private Transform player;
    private bool attacked;
    private Animator animator;
    private float startTime;
    public float time => Time.time - startTime;
    private float lastAttackTime;
    public float attackTime => Time.time - lastAttackTime;

    void Start()
    {
        Scale(RoomManager.instance.wagonCount);
        animator = GetComponent<Animator>();
        startTime = Time.time;
        lastAttackTime = Time.time;
        // SelectState();
        player = RoomManager.instance.player;
        chaseState.chaseDistance = chaseDistance;
        state = idleState;
        SetUpState();
    }
    void Update()
    {
        if (state.completed) SelectState();
        state.Do();
        if (time > 3.54f)
        {
            startTime = Time.time;
            attacked = false;
            SelectState();
        }
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
        if (health.currentHealth < health.maxHealth/2) selectedPattern *= 10;
        
        if (attacked && Vector3.Distance(player.position, transform.position) >= chaseDistance)
        {
            state = chaseState;
            attacked = false;
            SetUpState();
        }
        else
        {
            if (attackTime > 1.77f)
            {
                lastAttackTime = Time.time;
                Attacking();      
            }
        }

    }
    private void Attacking()
    {
        animator.Play("ChargeAttack");
        // yield return new WaitForSeconds(0.7f);
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

        health = transform.GetComponent<Health>();

        foreach (Attack attack in attacks)
        {
            attack.damage *= aditivo;
            attack.secDamage *= aditivo;
        }

        health.bDefense *= aditivo;
        health.maxHealth *= aditivo;
        health.currentHealth = health.maxHealth;
        health.regenHealth = health.maxHealth;
        health.ActHealthVisual();
    }
}