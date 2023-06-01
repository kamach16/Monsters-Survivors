using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerRfileProjectile : MonoBehaviour
{
    [SerializeField] private float maxDistanceToEnemyThatAllowsToBounceTheBullet;
    [SerializeField] private int maxBouncies;
    [SerializeField] private LayerMask layersToIgnore;

    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public bool bouncingBullet = true;
    [HideInInspector] public Vector3 projectleMoveDirectionOffset;

    private int bounciesAmount = 1;

    private List<Collider> enemies = new List<Collider>();
    private Transform nearbyEnemy;

    private void Update()
    {
        MoveProjectile();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
        else
        {
            BulletBehaviourAfterCollide(collider);
        }
    }

    public void SetBouncingBullet(bool isBouncingON)
    {
        bouncingBullet = isBouncingON;
    }

    public void SetProjectileSpeed(float newProjectileSpeed)
    {
        projectileSpeed = newProjectileSpeed;
    }

    public void SetProjectleMoveDirectionOffset(Vector3 newProjectleMoveDirectionOffset)
    {
        projectleMoveDirectionOffset = newProjectleMoveDirectionOffset;
    }

    private void MoveProjectile()
    {
        transform.Translate((Vector3.forward + projectleMoveDirectionOffset) * Time.deltaTime * projectileSpeed);
    }

    private void BulletBehaviourAfterCollide(Collider collider)
    {
        if (bouncingBullet)
        {
            enemies.Clear();
            enemies.AddRange(Physics.OverlapSphere(collider.transform.position, maxDistanceToEnemyThatAllowsToBounceTheBullet, ~layersToIgnore));
        }

        if (bouncingBullet && (enemies.Count > 1 && enemies != null) && bounciesAmount < maxBouncies)
        {
            for (int i = 0; i < enemies.Count; i++) // prevents setting the collided enemy as a next target
            {
                if(enemies[i].transform.parent == collider.transform.parent)
                {
                    enemies.RemoveAt(i);
                    nearbyEnemy = enemies[Random.Range(0, enemies.Count)].transform.parent.GetChild(0); // hit direction is a first child of enemy
                    break;
                }
            }
            RotateToEnemy(nearbyEnemy.position);
            bounciesAmount++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RotateToEnemy(Vector3 enemyPosition)
    {
        Quaternion lookRotation = Quaternion.LookRotation((enemyPosition - transform.position).normalized);
        transform.rotation = lookRotation;
    }
}
