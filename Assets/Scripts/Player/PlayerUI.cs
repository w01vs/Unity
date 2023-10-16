using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    [SerializeField]
    private PlayerHealthManagerScriptableObject healthManager;

    private float health;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    public float chipSpeed = 2f;
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

    private void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / healthManager.MaxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (hFraction > fillF)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
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
