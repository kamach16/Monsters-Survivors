using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Audio;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpsContainer;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ObjectAudio objectAudio;

    [Header("Attack speed boost properties")]
    [SerializeField] private float attackSpeedToIncreaseInPercentage;
    [SerializeField] private int maxAttackSpeedBoostUpgradesAmount;

    [Header("Attack damage boost properties")]
    [SerializeField] private float attackDamageToIncreaseInPercentage;

    [Header("Sprinter properties")]
    [SerializeField] private float moveSpeedToIncreaseInPercentage;
    [SerializeField] private int maxSprinterUpgradesAmount;

    [Header("XP multipler properties")]
    [SerializeField] private float xpToIncreaseInPercentage;
    [SerializeField] private int maxXpMultiplerUpgradesAmount;

    [Header("HP boost properties")]
    [SerializeField] private float maxHealthToIncreaseInPercentage;

    [SerializeField] public bool attackSpeedBoostUpgradeAvailable = true;
    [SerializeField] public bool sprinterUpgradeAvailable = true;
    [SerializeField] public bool xpMultiplerUpgradeAvailable = true;
    [SerializeField] public bool bouncingBulletsUpgradeAvailable = true;

    private PlayerWeapon playerWeapon;

    private float initialPlayerWeaponTimeBetweenProjectiles;
    private float initialPlayerWeaponDamage;
    private float initialPlayerMoveSpeed;
    private float initialPlayerMaxHealth;

    private int attackSpeedBoostUpgradesAmount = 0; // max 5 
    private int sprinterUpgradesAmount = 0; // max 5 
    private int xpMultiplerUpgradesAmount = 0; // max 5 

    private bool xpMultiplerActive;

    private void Awake()
    {
        SetVariables();
    }

    public bool GetIsAttackSpeedBoostUpgradeAvailable()
    {
        return attackSpeedBoostUpgradeAvailable;
    }

    public bool GetIsSprinterUpgradeAvailable()
    {
        return sprinterUpgradeAvailable;
    }

    public bool GetIsXpMultiplerUpgradeAvailable()
    {
        return xpMultiplerUpgradeAvailable;
    }

    public bool GetIsBouncingBulletsUpgradeAvailable()
    {
        return bouncingBulletsUpgradeAvailable;
    }

    private void SetVariables()
    {
        playerWeapon = playerShooting.GetPlayerWeapon();
        
        initialPlayerWeaponTimeBetweenProjectiles = playerWeapon.GetTimeBetweenProjectiles();
        initialPlayerWeaponDamage = playerWeapon.GetWeaponDamage();
        initialPlayerMoveSpeed = playerMovement.GetMoveSpeed();
        initialPlayerMaxHealth = playerHealth.GetMaxHealth();
    }

    public void AddPowerUp(PowerUpType powerUpType)
    {
        gameManager.ResumeGame(powerUpsContainer);
        levelUpEffect.Play();
        objectAudio.PlayPowerUpGainedSound();

        switch (powerUpType)
        {
            case PowerUpType.attackSpeedBoost:
                float currentPlayerWeaponTimeBetweenProjectiles = playerWeapon.GetTimeBetweenProjectiles();
                float newPlayerWeaponTimeBetweenProjectiles = currentPlayerWeaponTimeBetweenProjectiles - NewValueCalculatedUsingPercentages(initialPlayerWeaponTimeBetweenProjectiles, attackSpeedToIncreaseInPercentage);

                playerWeapon.SetTimeBetweenProjectiles(newPlayerWeaponTimeBetweenProjectiles);
                attackSpeedBoostUpgradesAmount++;
                if (attackSpeedBoostUpgradesAmount == maxAttackSpeedBoostUpgradesAmount) attackSpeedBoostUpgradeAvailable = false;
                break;

            case PowerUpType.attackDamageBoost:
                float currentPlayerWeaponDamage = playerWeapon.GetWeaponDamage();
                float newPlayerWeaponDamage = currentPlayerWeaponDamage + NewValueCalculatedUsingPercentages(initialPlayerWeaponDamage, attackDamageToIncreaseInPercentage);

                playerWeapon.SetWeaponDamage(newPlayerWeaponDamage);
                break;

            case PowerUpType.bouncingBullets:
                playerWeapon.SetBouncingBullets(true);
                bouncingBulletsUpgradeAvailable = false;
                break;

            case PowerUpType.sprinter:
                float currentPlayerMoveSpeed = playerMovement.GetMoveSpeed();
                float newPlayerMoveSpeed = currentPlayerMoveSpeed + NewValueCalculatedUsingPercentages(initialPlayerMoveSpeed, moveSpeedToIncreaseInPercentage);

                playerMovement.SetMoveSpeed(newPlayerMoveSpeed);
                sprinterUpgradesAmount++;
                if (sprinterUpgradesAmount == maxSprinterUpgradesAmount) sprinterUpgradeAvailable = false;
                break;

            case PowerUpType.xpMultipler:
                xpMultiplerUpgradesAmount++;
                xpMultiplerActive = true;
                if (xpMultiplerUpgradesAmount == maxXpMultiplerUpgradesAmount) xpMultiplerUpgradeAvailable = false;
                break;

            case PowerUpType.hpBoost:
                float currentPlayerMaxHealth = playerHealth.GetMaxHealth();
                float newPlayerMaxHealth = currentPlayerMaxHealth + NewValueCalculatedUsingPercentages(initialPlayerMaxHealth, maxHealthToIncreaseInPercentage);

                playerHealth.SetMaxHealth(newPlayerMaxHealth);
                break;

            default:
                break;
        }

        gameManager.SetPowerUpsContainerShowed(false);
    }

    public float ExtraXP(float initialXPToGain)
    {
        if (!xpMultiplerActive) return 0;
        else return NewValueCalculatedUsingPercentages(initialXPToGain, xpToIncreaseInPercentage) * xpMultiplerUpgradesAmount;
    }

    private float NewValueCalculatedUsingPercentages(float initialValue, float percentage)
    {
        return (initialValue * percentage / 100);
    }
}
