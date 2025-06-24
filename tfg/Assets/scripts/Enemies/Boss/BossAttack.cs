using System.Collections;
using UnityEngine;

public class BossAttack : EnemyState
{
    private Attack attack;
    private int pattern;
    public override void Enter()
    {
        base.Enter();
        GetAttack();

        if (pattern == 0) attack.AttackAction(player.position);
        else attack.AttackPattern(pattern);
        StartCoroutine("Wait");
    }

    public override void FixedDo()
    {
        base.FixedDo();
    }
    private void GetAttack()
    {
        attack = GetComponent<BossBehaviour>().selectedAttack;
        pattern = GetComponent<BossBehaviour>().selectedPattern;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.66f);
        Exit();
    }


}