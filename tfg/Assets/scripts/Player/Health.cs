using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float regenHealth;
    protected float invuFrames;
    public float bDefense;
    public float cDefense;
    public float rQuant;
    [SerializeField] protected float invuFramesCount;
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Slider healthSlider2;
    private float food;
    private float maxFood;
    private float startFoodTime;
    private float startRegenTime;

    public float RegenTime => Time.time - startRegenTime;
    public float FoodTime => Time.time - startFoodTime;

    public float regenTimer;
    public float foodTimer;

    public List<SCT> pool;


    void Start()
    {
        currentHealth = maxHealth;
        regenHealth = maxHealth;
        cDefense = bDefense;
        startFoodTime = Time.time;
        startFoodTime = Time.time;
    }
    public virtual void TakeDamage(float tHealth, float rHealth) //tHealth >= rHealth
    {
        if (invuFrames > 0) return;
        // Debug.Log("damage");

        // Mathf.Clamp(tHealth, 0, maxHealth);
        currentHealth -= tHealth;
        regenHealth -= rHealth;
        ActHealthVisual();
        invuFrames = invuFramesCount;
        if (currentHealth <= 0) Die();
    }
    public virtual void Die()
    {
        ItemSpwnManager.instance.SpawnItem(pool, transform.position);
        Destroy(gameObject);
    }
    void Update()
    {
        if (invuFrames > 0) invuFrames -= Time.deltaTime;
        if (RegenTime > regenTimer) Regen();
        if (FoodTime > foodTimer) Hungry();
        if(currentHealth==regenHealth) startRegenTime = Time.time;
    }
    public void Heal(float tHealth, float rHealth)
    {
        currentHealth += tHealth;
        regenHealth += rHealth;
        ActHealthVisual();
    }
    public void ActHealthVisual()
    {
        if (healthSlider != null) healthSlider.value = Mathf.InverseLerp(0, maxHealth, currentHealth);
        if (healthSlider2 != null) healthSlider2.value = Mathf.InverseLerp(0, maxHealth, regenHealth);
    }
    private void Regen()
    {
        currentHealth = Mathf.Clamp(rQuant + currentHealth, currentHealth, regenHealth);
        startRegenTime = Time.time;
        ActHealthVisual();
    }
    private void Hungry()
    {
        
    }

}
