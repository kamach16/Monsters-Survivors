using UnityEngine;
using Player;
using Audio;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private float xpToGain;
        [SerializeField] private float scoreToGain;
        [SerializeField] private HealthDisplay healthDisplay;
        [SerializeField] private ObjectAudio objectAudio;

        [Header("Hit Effect")]
        [SerializeField] private ParticleSystem hitVFX;

        private PlayerWeapon playerWeapon;
        private XPManager xpManager;
        private ScoreManager scoreManager;

        private void Awake()
        {
            SetVariables();
        }

        private void SetVariables()
        {
            playerWeapon = FindObjectOfType<PlayerWeapon>();
            xpManager = FindObjectOfType<XPManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            healthDisplay.UpdateHealth(currentHealth, maxHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("PlayerProjectile")) return; // if collided object is diffrent than player's weapon bullets

            float damageToDeal = playerWeapon.GetWeaponDamage();
            DealDamage(damageToDeal);
        }

        private void DealDamage(float damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0); // if health will be below 0, then return 0

            if (healthDisplay.gameObject.activeInHierarchy == false) healthDisplay.gameObject.SetActive(true); // show health when enemy is hit            
            healthDisplay.UpdateHealth(currentHealth, maxHealth);

            hitVFX.Play();
            objectAudio.PlayGetDamageSound();

            if (currentHealth <= 0)
            {
                xpManager.GainXP(xpToGain);
                scoreManager.GainScore(scoreToGain);
                Destroy(gameObject);
            }
        }
    }
}
