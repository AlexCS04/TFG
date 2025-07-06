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
    [SerializeField] protected Slider defenseSlider;
    public List<SCT> pool;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        regenHealth = maxHealth;
        cDefense = bDefense;
        ActHealthVisual();

    }
    public virtual void TakeDamage(float tHealth, float rHealth)
    {
        float t = Mathf.InverseLerp(0, cDefense, cDefense - tHealth);
        if (t == 0)
        {
            tHealth -= cDefense;
            rHealth = Mathf.Clamp(rHealth - cDefense, 0, rHealth);
            cDefense = 0;
        }
        else
        {
            cDefense -= tHealth;
            tHealth = 0;
            rHealth = 0;
        }
        currentHealth = Mathf.Clamp(currentHealth - tHealth, 0, currentHealth);
        regenHealth = Mathf.Clamp(regenHealth - rHealth, 0, regenHealth);
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
        currentHealth = Mathf.Clamp(currentHealth + tHealth, 0, maxHealth);
        regenHealth = Mathf.Clamp(regenHealth + rHealth, 0, maxHealth);
        ActHealthVisual();
    }
    public void ActHealthVisual()
    {
        if (currentHealth > regenHealth) regenHealth = currentHealth;
        if (regenHealth > maxHealth) regenHealth = maxHealth;
        if (healthSlider != null) healthSlider.value = Mathf.InverseLerp(0, maxHealth, currentHealth);
        if (healthSlider2 != null) healthSlider2.value = Mathf.InverseLerp(0, maxHealth, regenHealth);
        if (defenseSlider != null) defenseSlider.value = Mathf.InverseLerp(0, bDefense, cDefense);

    }
    public void defenseRegen()
    {
        cDefense = bDefense;
        if (defenseSlider != null) defenseSlider.value = Mathf.InverseLerp(0, bDefense, cDefense);
    }
    
    

}
