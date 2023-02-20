using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    [Tooltip("Ignore it when script is attached at enemy")]
    [SerializeField] private TextMeshProUGUI healthText;

    private Camera mainCamera;

    private void Awake()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    public void UpdateHealth(float currentHealth, float maxHealth) // execute when health value is changing
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (healthText != null) healthText.text = currentHealth.ToString();
    }        

    private void LookAtPlayer() // it executes only when script is attached at enemy
    {
        if (healthText == null)
        {
            transform.LookAt(mainCamera.transform.position);
        }
    }
}
