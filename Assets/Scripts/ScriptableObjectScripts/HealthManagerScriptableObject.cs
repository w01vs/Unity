using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerHealthManagerScriptableObject", menuName = "ScriptableObjects/PlayerHealthManager")]
public class PlayerHealthManagerScriptableObject : ScriptableObject
{
    [SerializeField] private float health = 100;
    public float MaxHealth { get { return 100; } private set { } }

    [System.NonSerialized] public UnityEvent<float> healthChangeEvent;

    private void OnEnable()
    {
        health = MaxHealth;
        if (healthChangeEvent == null)
        {
            healthChangeEvent = new UnityEvent<float>();
        }
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        Mathf.Clamp(health, 0, MaxHealth);
        healthChangeEvent.Invoke(health);
    }

    public void IncreaseHealth(int amount)
    {
        health += amount;
        Mathf.Clamp(health, 0, MaxHealth);
        healthChangeEvent.Invoke(health);
    }
}
