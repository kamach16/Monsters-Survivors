using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] public bool isDead = false;
        [SerializeField] public float maxHealth;
        [SerializeField] private float currentHealth;

        [SerializeField] private ParticleSystem hitVFX;
        [SerializeField] private HealthDisplay healthDisplay;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ObjectAudio objectAudio;

        private Animator animator;

        private void Awake()
        {
            SetVariables();
        }

        private void SetVariables()
        {
            animator = GetComponent<Animator>();

            currentHealth = maxHealth;
            UpdateHealth();
        }

        public bool GetIsDead()
        {
            return isDead;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetMaxHealth(float newMaxHealth)
        {
            maxHealth = newMaxHealth;
            UpdateHealth();
        }

        public void IncreaseCurrentHealth(float healthToAdd)
        {
            currentHealth = Mathf.Min(currentHealth + healthToAdd, maxHealth); // if health will be above maxHealth, then return maxHealth
            UpdateHealth();
        }

        public void UpdateHealth()
        {
            healthDisplay.UpdateHealth(currentHealth, maxHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("EnemyProjectile")) return; // if collided object is diffrent than player's weapon bullets

            EnemyProjectile enemyProjectile = other.GetComponent<EnemyProjectile>();

            float damageToDeal = enemyProjectile.GetDamage();
            DealDamage(damageToDeal);
        }

        public void DealDamage(float damage)
        {
            if (isDead) return;

            currentHealth = Mathf.Max(currentHealth - damage, 0); // if health will be below 0, then return 0
            UpdateHealth();

            hitVFX.Play();
            objectAudio.PlayGetDamageSound();

            if (currentHealth <= 0)
            {
                isDead = true;
                animator.SetTrigger("die");
                gameManager.ShowDeathScreen();
            }
        }
    }
}
