using UnityEngine;

public class BossAttack : BossState
{
    private Attack attack;
    private int pattern;
    public override void Enter()
    {
        base.Enter();
        GetAttack();
    }
    public override void Do()
    {
        if (pattern == 0) attack.AttackAction(player.position);
        else attack.AttackPattern(pattern);
    }
    public override void FixedDo()
    {
        base.FixedDo();
    }
    private void GetAttack()
    {
        attack = GetComponent<BossBehaviour>().selectedAttack;
        pattern=GetComponent<BossBehaviour>().selectedPattern;
    }


}