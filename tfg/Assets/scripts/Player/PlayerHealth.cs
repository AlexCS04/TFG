using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void TakeDamage(float tHealth, float rHealth)
    {
        base.TakeDamage(tHealth, rHealth);
        StartCoroutine("InvuFeel");
    }

    public override void Die()
    {
        Debug.Log("Skill isue");
    }
    IEnumerator InvuFeel()
    {
        SpriteRenderer s;
        if (GetComponent<SpriteRenderer>())
        {
            s = GetComponent<SpriteRenderer>();
            Color c = s.color;
            for (int i = 0; i < 10; i++)
            {
                c.a = 0.4f;
                s.color = c;
                yield return new WaitForSeconds(0.1f);
                c.a = 1f;
                s.color = c;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
    }
}
