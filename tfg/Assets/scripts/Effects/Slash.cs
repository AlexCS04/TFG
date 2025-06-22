using UnityEngine;

public class Slash : MonoBehaviour
{
    public ParticleSystem slashEffect;
    private float rangeMultiplier = 1f;  
    void Awake()
    {
        var main = slashEffect.main;
        rangeMultiplier = transform.parent.GetComponent<Attack>().attackRange;
        main.startSpeed = 10f * rangeMultiplier;    
        main.duration =rangeMultiplier*0.2f;
    }   
}
