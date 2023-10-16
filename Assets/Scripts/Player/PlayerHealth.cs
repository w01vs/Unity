using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    private float lerpTimer;

    public Image frontHealthBar;
    public Image backHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0 , maxHealth);
        UpdateHealthUI();
        if(Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(hFraction > fillF)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);

        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
