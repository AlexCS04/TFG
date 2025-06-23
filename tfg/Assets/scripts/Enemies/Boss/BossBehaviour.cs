using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Attack selectedAttack { get; private set; }
    public int selectedPattern { get; private set; }

    private BossState state;
    private Health health;
    [SerializeField]private BossState idleState;

    void Start()
    {
        Scale(RoomManager.instance.wagonCount);
        // SelectState();
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
        if (health.currentHealth >= health.maxHealth)
        {

        }
        else
        {
            
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