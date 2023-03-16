using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] public PlayerWeapon currentWeapon;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private GameManager gameManager;

        private void Update()
        {
            Shooting();
        }

        public PlayerWeapon GetPlayerWeapon()
        {
            return currentWeapon;
        }

        private void Shooting()
        {
            if (playerHealth.GetIsDead())
            {
                currentWeapon.StopShooting();
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !gameManager.GetGamePaused())
            {
                currentWeapon.StartShooting();
            }
            else if (!Input.GetKey(KeyCode.LeftControl))
            {
                currentWeapon.StopShooting();
                print("stop");
            }
        }
    }
}
