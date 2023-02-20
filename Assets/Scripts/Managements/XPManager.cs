using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    [SerializeField] private int currentLvl;
    [SerializeField] public float currentXP;
    [SerializeField] public float xpToLevelUp;
    [SerializeField] private XPDisplay xpDisplay;
    [SerializeField] private GameObject powerUpsContainer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PowerUpsManager powerUpsManager;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Awake()
    {
        xpDisplay.UpdateXP(0, xpToLevelUp);
    }

    public void GainXP(float xpToGain)
    {
        xpToGain += powerUpsManager.ExtraXP(xpToGain);
        currentXP += xpToGain;

        if(currentXP >= xpToLevelUp)
        {
            currentXP = XPAfterLevelUp();
            LevelUp();
        }

        UpdateXP();
    }

    private void UpdateXP()
    {
        xpDisplay.UpdateXP(currentXP, xpToLevelUp);
    }

    private float XPAfterLevelUp()
    {
        float xpToGain;
        xpToGain = currentXP - xpToLevelUp;

        return xpToGain;
    }

    private void LevelUp()
    {
        gameManager.StopGame();
        powerUpsContainer.SetActive(true);
        currentLvl++;
        UpdateEnemySpawnerValues();
    }

    private void UpdateEnemySpawnerValues()
    {
        if (currentLvl >= enemySpawner.GetRequiredLevelToSpawnMidGameEnemies()) enemySpawner.SetCanSpawnMidGameEnemies(true);
        if (currentLvl >= enemySpawner.GetRequiredLevelToSpawnLateGameEnemies()) enemySpawner.SetCanSpawnLateGameEnemies(true);

        enemySpawner.ChangeTimeBetweenSpawns();
    }
}
