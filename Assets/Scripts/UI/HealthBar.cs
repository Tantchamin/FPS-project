using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerStatus playerStatus;

    public void SetMaxHealth()
    {
        healthSlider.maxValue = playerStatus.maxHealth;
        healthSlider.value = playerStatus.currentHealth;
    }

    public void SetHealth()
    {
        healthSlider.value = playerStatus.currentHealth;
    }
}
