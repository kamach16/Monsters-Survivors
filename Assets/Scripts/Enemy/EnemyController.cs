using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyFighter enemyFighter;

        private GameObject player;

        private void Start()
        {
            SetVariables();
        }
    
        private void Update()
        {
            ControlEnemy();
        }

        private void ControlEnemy()
        {
            if(InteractWithFighter()) return;
            InteractWithMovement();
        }

        private void SetVariables()
        {
            player = enemyMovement.GetPlayer();
        }

        private void InteractWithMovement()
        {
            enemyMovement.MoveTo(player.transform.position);
        }

        private bool InteractWithFighter()
        {
            float attackRange = enemyFighter.GetAttackRange();

            if (DistanceToPlayer() <= attackRange)
            {
                enemyFighter.Attack();
                return true;
            }
            else
            {
                return false;
            }
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}
