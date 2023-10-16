using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public float deathTimer;
    public TextMeshProUGUI healthUI;

    public float timeUntilDeath = 2f;
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
