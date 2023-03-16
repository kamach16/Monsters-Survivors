using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenEarlyGameEnemiesSpawns;
    [SerializeField] private float timeBetweenMidGameEnemiesSpawns;
    [SerializeField] private float timeBetweenLateGameEnemiesSpawns;
    [SerializeField] private float minTimeBetweenEarlyGameEnemiesSpawns;
    [SerializeField] private float minTimeBetweenMidGameEnemiesSpawns;
    [SerializeField] private float minTimeBetweenLateGameEnemiesSpawns;
    [SerializeField] private GameObject[] earlyGameEnemies;
    [SerializeField] private GameObject[] midGameEnemies;
    [SerializeField] private GameObject[] lateGameEnemies;
    [SerializeField] public int requiredLevelToSpawnMidGameEnemies;
    [SerializeField] public int requiredLevelToSpawnLateGameEnemies;
    [SerializeField] private float timeToReduceBetweenEnemiesSpawns;
    [SerializeField] private PlayerHealth playerHealth;

    // values are in pixels of camera's resolution
    [Header("Spawn positons")]
    [SerializeField] private float spawnPositionOnTopSideYOffset;
    [SerializeField] private float spawnPositionOnBottomSideYOffset;
    [SerializeField] private float spawnPositionOnLeftAndRightSideXOffset;

    [SerializeField] private int spawnLayerIndex; // main ground layer has index 11
    [SerializeField] private float numberOfSpawnPositionsOnEachSide;

     public bool canSpawnMidGameEnemies = false;
     public bool canSpawnLateGameEnemies = false;

    // values are in pixels of camera's resolution
    private List<Vector3> spawnPositionsOnTopSide = new List<Vector3>();
    private List<Vector3> spawnPositionsOnBottomSide = new List<Vector3>();
    private List<Vector3> spawnPositionsOnLeftSide = new List<Vector3>();
    private List<Vector3> spawnPositionsOnRightSide = new List<Vector3>();
    private List<Vector3> allSpawnPositions = new List<Vector3>(); 

    private Camera mainCamera;

    private void Start()
    {
        SetVariables();
        ClearSpawnPositions();
        SetAllSpawnPositions();
        StartCoroutine("SpawnEarlyGameEnemy");
        StartCoroutine("SpawnMidGameEnemy");
        StartCoroutine("SpawnLateGameEnemy");
    }

    private void OnDrawGizmos()
    {
        ShowSpawnPositions();
    }

    private void SetVariables()
    {
        mainCamera = Camera.main;
    }

    private void ClearSpawnPositions()
    {
        spawnPositionsOnTopSide.Clear();
        spawnPositionsOnBottomSide.Clear();
        spawnPositionsOnLeftSide.Clear();
        spawnPositionsOnRightSide.Clear();
    }

    private void SetAllSpawnPositions()
    {
        SetTopSideSpawnPositions();
        SetBottomSideSpawnPositions();
        SetLeftSideSpawnPositions();
        SetRightSideSpawnPositions();

        // it sums up all lists of spawn positions into one list
        allSpawnPositions.AddRange(spawnPositionsOnTopSide);
        allSpawnPositions.AddRange(spawnPositionsOnBottomSide);
        allSpawnPositions.AddRange(spawnPositionsOnLeftSide);
        allSpawnPositions.AddRange(spawnPositionsOnRightSide);
    }

    public void SetCanSpawnMidGameEnemies(bool canSpawn)
    {
        canSpawnMidGameEnemies = canSpawn;
    }

    public void SetCanSpawnLateGameEnemies(bool canSpawn)
    {
        canSpawnLateGameEnemies = canSpawn;
    }

    public int GetRequiredLevelToSpawnMidGameEnemies()
    {
        return requiredLevelToSpawnMidGameEnemies;
    }

    public int GetRequiredLevelToSpawnLateGameEnemies()
    {
        return requiredLevelToSpawnLateGameEnemies;
    }

    public void ChangeTimeBetweenSpawns()
    {
        if (canSpawnLateGameEnemies) // from late game, reduce the time between spawns
        {
            timeBetweenEarlyGameEnemiesSpawns -= timeToReduceBetweenEnemiesSpawns;
            timeBetweenMidGameEnemiesSpawns -= timeToReduceBetweenEnemiesSpawns;
            timeBetweenLateGameEnemiesSpawns -= timeToReduceBetweenEnemiesSpawns;

            timeBetweenEarlyGameEnemiesSpawns = Mathf.Max(timeBetweenEarlyGameEnemiesSpawns, minTimeBetweenEarlyGameEnemiesSpawns);
            timeBetweenMidGameEnemiesSpawns = Mathf.Max(timeBetweenMidGameEnemiesSpawns, minTimeBetweenMidGameEnemiesSpawns);
            timeBetweenLateGameEnemiesSpawns = Mathf.Max(timeBetweenLateGameEnemiesSpawns, minTimeBetweenLateGameEnemiesSpawns);
        }
    }

    private IEnumerator SpawnEarlyGameEnemy()
    {
        while (true)
        {
            if (!playerHealth.GetIsDead())
            {
                int randomSpawnPosition = Random.Range(0, allSpawnPositions.Count);
                int randomEnemyToSpawn = Random.Range(0, earlyGameEnemies.Length);

                Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(allSpawnPositions[randomSpawnPosition]) - mainCamera.transform.position;
                RaycastHit hit;

                if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
                {
                    if (hit.transform.gameObject.layer == spawnLayerIndex)
                    {
                        Vector3 worldSpawnPosition = hit.point;
                        Instantiate(earlyGameEnemies[randomEnemyToSpawn], worldSpawnPosition, Quaternion.identity);
                    }
                }
            }

            yield return new WaitForSeconds(timeBetweenEarlyGameEnemiesSpawns);
        }
    }

    private IEnumerator SpawnMidGameEnemy()
    {
        while (true)
        {
            if (canSpawnMidGameEnemies && !playerHealth.GetIsDead())
            {
                int randomSpawnPosition = Random.Range(0, allSpawnPositions.Count);
                int randomEnemyToSpawn = Random.Range(0, midGameEnemies.Length);

                Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(allSpawnPositions[randomSpawnPosition]) - mainCamera.transform.position;
                RaycastHit hit;

                if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
                {
                    if (hit.transform.gameObject.layer == spawnLayerIndex)
                    {
                        Vector3 worldSpawnPosition = hit.point;
                        Instantiate(midGameEnemies[randomEnemyToSpawn], worldSpawnPosition, Quaternion.identity);
                    }
                }
            }

            yield return new WaitForSeconds(timeBetweenMidGameEnemiesSpawns);
        }
    }

    private IEnumerator SpawnLateGameEnemy()
    {
        while (true)
        {
            if (canSpawnLateGameEnemies && !playerHealth.GetIsDead())
            {
                int randomSpawnPosition = Random.Range(0, allSpawnPositions.Count);
                int randomEnemyToSpawn = Random.Range(0, lateGameEnemies.Length);

                Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(allSpawnPositions[randomSpawnPosition]) - mainCamera.transform.position;
                RaycastHit hit;

                if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
                {
                    if (hit.transform.gameObject.layer == spawnLayerIndex)
                    {
                        Vector3 worldSpawnPosition = hit.point;
                        Instantiate(lateGameEnemies[randomEnemyToSpawn], worldSpawnPosition, Quaternion.identity);
                    }
                }
            }

            yield return new WaitForSeconds(timeBetweenLateGameEnemiesSpawns);
        }
    }

    private void SetTopSideSpawnPositions() // relative to camera's resolution
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenHorizontalSpawnpoints = (cameraWidth / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = cameraWidth; // value is in pixels of camera's resolution
        float spawnPositionY = cameraHeight + spawnPositionOnTopSideYOffset; // value is in pixels of camera's resolution

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnTopSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnTopSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                }
            }

            spawnPositionX -= distanceBetweenHorizontalSpawnpoints;
        }
    } 

    private void SetBottomSideSpawnPositions() // relative to camera's resolution
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenHorizontalSpawnpoints = (cameraWidth / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = cameraWidth; // value is in pixels of camera's resolution
        float spawnPositionY = 0 - spawnPositionOnBottomSideYOffset; // value is in pixels of camera's resolution. 0 = bottom part of screen

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnBottomSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnBottomSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                }
            }

            spawnPositionX -= distanceBetweenHorizontalSpawnpoints;
        }
    } 

    private void SetLeftSideSpawnPositions() // relative to camera's resolution
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenVerticalSpawnpoints = ((cameraHeight) / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = 0 - spawnPositionOnLeftAndRightSideXOffset; // value is in pixels of camera's resolution. 0 = left part of screen
        float spawnPositionY = cameraHeight; // value is in pixels of camera's resolution

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnLeftSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnLeftSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                }
            }

            spawnPositionY -= distanceBetweenVerticalSpawnpoints;
        }
    } 

    private void SetRightSideSpawnPositions() // relative to camera's resolution
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenVerticalSpawnpoints = ((cameraHeight) / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = cameraWidth + spawnPositionOnLeftAndRightSideXOffset; // value is in pixels of camera's resolution
        float spawnPositionY = cameraHeight; // value is in pixels of camera's resolution

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnRightSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnRightSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                }
            }

            spawnPositionY -= distanceBetweenVerticalSpawnpoints;
        }
    }

    /////////////// ONLY FOR TESTS //////////////

    private void ShowSpawnPositions() // only for tests, call it in OnDrawGizmos
    {
        mainCamera = Camera.main;

        ShowRightSideSpawnPositions();
        ShowLeftSideSpawnPositions();
        ShowBottomSideSpawnPositions();
        ShowTopSideSpawnPositions();
    }

    private void ShowRightSideSpawnPositions()
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenVerticalSpawnpoints = ((cameraHeight) / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = cameraWidth + spawnPositionOnLeftAndRightSideXOffset; // value is in pixels of camera's resolution
        float spawnPositionY = cameraHeight; // value is in pixels of camera's resolution

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnRightSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnRightSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                    Gizmos.DrawWireSphere(hitPosition, 1);
                }
            }

            spawnPositionY -= distanceBetweenVerticalSpawnpoints;
        }
    }

    private void ShowLeftSideSpawnPositions()
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenVerticalSpawnpoints = ((cameraHeight) / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = 0 - spawnPositionOnLeftAndRightSideXOffset; // value is in pixels of camera's resolution. 0 = left part of screen
        float spawnPositionY = cameraHeight; // value is in pixels of camera's resolution

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnLeftSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnLeftSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                    Gizmos.DrawWireSphere(hitPosition, 1);
                }
            }

            spawnPositionY -= distanceBetweenVerticalSpawnpoints;
        }
    }

    private void ShowBottomSideSpawnPositions()
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenHorizontalSpawnpoints = (cameraWidth / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = cameraWidth; // value is in pixels of camera's resolution
        float spawnPositionY = 0 - spawnPositionOnBottomSideYOffset; // value is in pixels of camera's resolution. 0 = bottom part of screen

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnBottomSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnBottomSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                    Gizmos.DrawWireSphere(hitPosition, 1);
                }
            }

            spawnPositionX -= distanceBetweenHorizontalSpawnpoints;
        }
    }

    private void ShowTopSideSpawnPositions()
    {
        float cameraWidth = mainCamera.pixelWidth;
        float cameraHeight = mainCamera.pixelHeight;

        float distanceBetweenHorizontalSpawnpoints = (cameraWidth / numberOfSpawnPositionsOnEachSide) * 1.125f; // multiple by 1.125 to center spawn positions

        float spawnPositionX = cameraWidth; // value is in pixels of camera's resolution
        float spawnPositionY = cameraHeight + spawnPositionOnTopSideYOffset; // value is in pixels of camera's resolution

        for (int i = 0; i < numberOfSpawnPositionsOnEachSide; i++)
        {
            spawnPositionsOnTopSide.Add(new Vector3(spawnPositionX, spawnPositionY, mainCamera.nearClipPlane));

            Vector3 rayCastDirection = mainCamera.ScreenToWorldPoint(spawnPositionsOnTopSide[i]) - mainCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.localPosition, rayCastDirection, out hit, 500))
            {
                if (hit.transform.gameObject.layer == spawnLayerIndex)
                {
                    Vector3 hitPosition = hit.point;
                    Gizmos.DrawWireSphere(hitPosition, 1);
                }
            }

            spawnPositionX -= distanceBetweenHorizontalSpawnpoints;
        }
    }
}
