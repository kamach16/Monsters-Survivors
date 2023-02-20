using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemy
{
    public class EnemyFighter : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float attackDamage;
        [SerializeField] public float attackRange;
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private float rotationToTargetSpeed;
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private bool hasRangeAttacks;

        [Header("If enemy has only range attack")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnPosition;

        private float timeSinceLastAttack;

        private GameObject player;
        private Animator animator;
        private PlayerHealth playerHealth;

        enum EnemyType
        {
            skeleton,
            slime,
            spider,
            frog,
            wasp,
            snake,
            rat,
            dragon,
            triceratopsDinosaur
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        private void Start()
        {
            SetVariables();
        }

        private void SetVariables()
        {
            player = enemyMovement.GetPlayer();
            animator = enemyMovement.GetAnimator();
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        public float GetAttackRange()
        {
            return attackRange;
        }

        // animation event
        public void AttackHit()
        {
            if (!hasRangeAttacks)
            {
                playerHealth.DealDamage(attackDamage);
            }
            else
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);

                Transform targetToAim = player.transform.GetChild(2); // target to aim is a third child of player gameobject

                EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();
                enemyProjectile.AimTarget(targetToAim);
                enemyProjectile.SetDamage(attackDamage);

                Destroy(projectile, 5f);
            }
        }

        public void Attack()
        {
            if (playerHealth.GetIsDead()) return;

            enemyMovement.Stop();
            LookAtTarget();

            switch (enemyType)
            {
                case EnemyType.skeleton:
                    if(timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.slime:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.spider:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.frog:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.wasp:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.snake:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.rat:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.dragon:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                case EnemyType.triceratopsDinosaur:
                    if (timeSinceLastAttack > timeBetweenAttacks)
                    {
                        animator.SetTrigger("attack");
                        timeSinceLastAttack = 0;
                    }
                    break;

                default:
                    break;
            }
        }

        private void LookAtTarget()
        {
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.y = 0.0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), Time.time * rotationToTargetSpeed);
        }
    }
}
