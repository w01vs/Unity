using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    [SerializeField]
    private PlayerHealthManagerScriptableObject healthManager;

    private float health;
    public Image frontHealthBar;
    public Image backHealthBar;
    public float CHIPSPEED = 2f;
    private float lerpTimer;

    public void Update()
    {
        UpdateHealthUI();
    }

    public void OnEnable()
    {
        healthManager.healthChangeEvent.AddListener(ChangeHealth);
        health = healthManager.MaxHealth;
    }

    public void OnDisable()
    {
        healthManager.healthChangeEvent.RemoveListener(ChangeHealth);
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / healthManager.MaxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / CHIPSPEED;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (hFraction > fillF)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / CHIPSPEED;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    private void ChangeHealth(float health)
    {
        this.health = health;
        lerpTimer = 0;
    }
}
