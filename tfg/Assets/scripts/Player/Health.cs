using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float regenHealth;
    protected float invuFrames;
    [SerializeField] protected float invuFramesCount;
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Slider healthSlider2;


    void Start()
    {
        currentHealth = maxHealth;
        regenHealth = maxHealth;
    }
    public virtual void TakeDamage(float tHealth, float rHealth)
    {
        if (invuFrames > 0) return;
        // Debug.Log("damage");
        currentHealth -= tHealth;
        regenHealth -= rHealth;
        if (healthSlider != null) healthSlider.value = Mathf.InverseLerp(0,maxHealth,currentHealth);
        if (healthSlider2 != null) healthSlider2.value = Mathf.InverseLerp(0,maxHealth,regenHealth);
        invuFrames = invuFramesCount;
        if (currentHealth <= 0) Die();
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (invuFrames > 0) invuFrames -= Time.deltaTime;
    }
}
