using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image playerHealthSliderImageFill;

    public float health = 1;
    private float maxHp = 1;
    
    void Update()
    {
        if (health>= maxHp)
        {
            health = 1;
        }
    }

    public void TakeDamage()
    {
        // Play sound
        // Particle effect
        health -= 0.03f;
        
        playerHealthSliderImageFill.fillAmount = health;
    }
}