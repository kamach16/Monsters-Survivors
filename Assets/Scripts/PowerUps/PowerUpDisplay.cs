using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpDisplay : MonoBehaviour
{
    [SerializeField] private PowerUp powerUp;

    [SerializeField] private TextMeshProUGUI powerUpName;
    [SerializeField] private TextMeshProUGUI powerUpDescription;
    [SerializeField] private Image powerUpArtwork;
    [SerializeField] private PowerUpsManager powerUpsManager;
    [SerializeField] private List<PowerUp> allPowerUpsAvailable = new List<PowerUp>();

    private void OnEnable()
    {
        SetPowerUp();
    }

    private void SetPowerUp()
    {
        PowerUp powerUpToRemove = PowerUpToRemove();
        allPowerUpsAvailable.Remove(powerUpToRemove);

        PowerUp powerUpToSet = allPowerUpsAvailable[Random.Range(0, allPowerUpsAvailable.Count)];
        powerUp = powerUpToSet;

        SetValues();
    }
    private PowerUp PowerUpToRemove()
    {
        PowerUp tempPowerUp = null;

        for (int i = 0; i < allPowerUpsAvailable.Count; i++)
        {
            if(allPowerUpsAvailable[i].powerUpType == PowerUpType.attackSpeedBoost)
            {
                if (!powerUpsManager.GetIsAttackSpeedBoostUpgradeAvailable())
                {
                    tempPowerUp = allPowerUpsAvailable[i];
                    break;
                }
            }
            else if (allPowerUpsAvailable[i].powerUpType == PowerUpType.bouncingBullets)
            {
                if (!powerUpsManager.GetIsBouncingBulletsUpgradeAvailable())
                {
                    tempPowerUp = allPowerUpsAvailable[i];
                    break;
                }
            }
            else if (allPowerUpsAvailable[i].powerUpType == PowerUpType.sprinter)
            {
                if (!powerUpsManager.GetIsSprinterUpgradeAvailable())
                {
                    tempPowerUp = allPowerUpsAvailable[i];
                    break;
                }
            }
            else if (allPowerUpsAvailable[i].powerUpType == PowerUpType.xpMultipler)
            {
                if (!powerUpsManager.GetIsXpMultiplerUpgradeAvailable())
                {
                    tempPowerUp = allPowerUpsAvailable[i];
                    break;
                }
            }
        }

        return tempPowerUp;
    }

    private void SetValues()
    {
        powerUpName.text = powerUp.powerUpName.ToString();
        powerUpDescription.text = powerUp.description.ToString();
        powerUpArtwork.sprite = powerUp.artwork;
    }

    public void AddPowerUp()
    {
        powerUpsManager.AddPowerUp(powerUp.powerUpType);
    }
}
