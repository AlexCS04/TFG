using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float regenHealth;
    public float bDefense;
    public float cDefense;
    
    
    public Slider healthSlider;
    [SerializeField] protected Slider healthSlider2;
    public List<SCT> pool;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        regenHealth = maxHealth;
        cDefense = bDefense;
        ActHealthVisual();
        
    }
    public virtual void TakeDamage(float tHealth, float rHealth) //tHealth >= rHealth
    {
        
        rHealth = tHealth * rHealth;
        currentHealth = Mathf.Clamp(currentHealth - (tHealth - cDefense), 0, currentHealth);
        // currentHealth -= tHealth

        regenHealth = Mathf.Clamp(regenHealth - (rHealth - cDefense), 0, regenHealth);
        // regenHealth -= rHealth;
        ActHealthVisual();
        
        if (currentHealth <= 0) Die();
    }
    public virtual void Die()
    {
        ItemSpwnManager.instance.SpawnItem(pool, transform.position);
        Destroy(gameObject);
    }

    public virtual void Heal(float tHealth, float rHealth) //active heal
    {
        currentHealth = Mathf.Clamp(currentHealth+tHealth, 0, maxHealth);
        regenHealth = Mathf.Clamp(regenHealth+rHealth, 0, maxHealth);
        ActHealthVisual();
    }
    public void ActHealthVisual()
    {
        if (currentHealth > regenHealth) regenHealth = currentHealth;
        if (healthSlider != null) healthSlider.value = Mathf.InverseLerp(0, maxHealth, currentHealth);
        if (healthSlider2 != null) healthSlider2.value = Mathf.InverseLerp(0, maxHealth, regenHealth);
    }
    
    

}
