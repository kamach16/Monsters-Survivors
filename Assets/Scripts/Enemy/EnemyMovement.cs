using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private GameObject player;
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        private void Awake()
        {
            SetVariables();
        }

        private void Update()
        {
            Animate();
        }

        private void SetVariables()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public GameObject GetPlayer() // call this in later method than "Awake" for example "Start"
        {
            return player;
        }

        public Animator GetAnimator() // call this in later method than "Awake" for example "Start"
        {
            return animator;
        }

        public void MoveTo(Vector3 newDestination)
        {
            animator.SetTrigger("stopAttack");
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.SetDestination(newDestination);
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void Animate()
        {
            if (animator.GetParameter(0).name != "speed") return; // if "speed" parameter doesn't exist. ("SPEED" PARAMETER HAS TO BE ON FIRST POSITION OF PARAMETERS IN ANIMATOR, EACH ANIMATOR NEEDS AT LEAST ONE ANIMATOR PARAMETER)

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = Mathf.Clamp01(localVelocity.z);
            animator.SetFloat("speed", speed, 0.05f, Time.deltaTime);
        }
    }
}