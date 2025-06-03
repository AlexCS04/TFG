using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    private float timeSinceAttack;

    void Update()
    {
        if (timeSinceAttack <= 0)
        {
            //attack input

            timeSinceAttack = attackSpeed;
        }
        else
        {
            timeSinceAttack -= Time.deltaTime;
        }
    }

}
