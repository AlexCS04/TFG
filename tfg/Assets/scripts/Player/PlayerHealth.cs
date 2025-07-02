using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private float food;
    [SerializeField] private float maxFood;
    [SerializeField] private Slider foodSlider;
    private float startFoodTime;
    private float startRegenTime;
    public float RegenTime => Time.time - startRegenTime;
    public float FoodTime => Time.time - startFoodTime;
    public float regenTimer;
    public float foodTimer;
    private float invuFrames;
    public float rQuant;
    [SerializeField] protected float invuFramesCount;
    protected override void Start()
    {
        base.Start();
        startRegenTime = Time.time;
        startFoodTime = Time.time;
        food = maxFood;
        ActualizarFood();
    }
    public override void TakeDamage(float tHealth, float rHealth)
    {
        if (invuFrames > 0) return;
        base.TakeDamage(tHealth, rHealth);
        invuFrames = invuFramesCount;
        AudioManager.PlaySound(EffectTypes.hitted);
        StartCoroutine("InvuFeel");
    }

    public override void Die()
    {
        PlayerPrefs.SetInt("Salir", -1);
        SceneManager.LoadScene("Huida");
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
    void Update()
    {
        if (invuFrames > 0) invuFrames -= Time.deltaTime;
        if (RegenTime > regenTimer) Regen();
        if (FoodTime > foodTimer) Hungry();
        if (currentHealth == regenHealth && rQuant>0) startRegenTime = Time.time;
    }
    private void Regen() //pasive heal
    {
        if (food < maxFood * .65f && rQuant>0) return;
        currentHealth = Mathf.Clamp(rQuant + currentHealth, 0, regenHealth);
        startRegenTime = Time.time;
        ActHealthVisual();
        if (currentHealth == 0) Die();
    }
    private void Hungry()
    {
        if (food > 0)
        {
            food = food - maxFood * .05f;
            ActualizarFood();
        }
        else
        {
            invuFrames = invuFramesCount;
            AudioManager.PlaySound(EffectTypes.hitted);
            StartCoroutine("InvuFeel");
            currentHealth -= maxHealth * .1f;
            ActHealthVisual();
        }
        startFoodTime = Time.time;
    }
    public bool Consume(float tHealth, float rHealth, float fill)
    {
        if (fill > 0 && food == maxFood) return false;
        Heal(tHealth, rHealth);
        food = Mathf.Clamp(food + fill, 0, maxFood);

        ActualizarFood();
        return true;
    }
    private void ActualizarFood() {
        if (foodSlider != null) foodSlider.value = Mathf.InverseLerp(0, maxFood, food);
    }
}
