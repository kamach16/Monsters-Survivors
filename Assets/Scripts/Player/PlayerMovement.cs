using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private LayerMask layersToIgnore;

        private Vector3 cursorPosition;
        
        private Animator animator;
        private Rigidbody rb;

        private void Awake()
        {
            SetVariables();
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        public void SetMoveSpeed(float newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
        }

        private void SetVariables()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        private void Move()
        {
            if (playerHealth.GetIsDead()) return;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Camera camera = Camera.main;
            Vector3 forward = camera.transform.forward; // camera forward vector
            Vector3 right = camera.transform.right; // camera right vector

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 movement = (forward * vertical + right * horizontal).normalized; // move player relative to the camera
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
            
            AnimatePlayer(horizontal, vertical);
        }

        private void AnimatePlayer(float horizontalInput, float verticalInput)
        {
            if (horizontalInput != 0 || verticalInput != 0) animator.SetFloat("speed", 1f, 0.05f, Time.deltaTime); // if player uses move key, smoothly change animation to run
            else animator.SetFloat("speed", 0);            
        }

        private void Rotate() // rotate player to cursor direction
        {
            if (playerHealth.GetIsDead()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 500, ~layersToIgnore))
            {
                cursorPosition = hit.point;
            }

            Vector3 lookDirection = cursorPosition - transform.position;
            lookDirection.y = 0; // it prevents player to rotate vertically 

            transform.LookAt(transform.position + lookDirection, Vector3.up);
        }
    }
}
