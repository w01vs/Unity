using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 100;
    private float deathTimer;
    public TextMeshProUGUI healthUI;

    [SerializeField] private float timeUntilDeath = 2f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if(health == 0)
        {
            deathTimer += Time.deltaTime;
        }

        UpdateUI();

        if(deathTimer >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateUI()
    {
        healthUI.text = health + " / " + maxHealth;
    } 

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
