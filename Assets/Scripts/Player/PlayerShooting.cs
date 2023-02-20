using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] public PlayerWeapon currentWeapon;
        [SerializeField] private PlayerHealth playerHealth;

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

            if (Input.GetButtonDown("Fire1"))
            {
                currentWeapon.StartShooting();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                currentWeapon.StopShooting();
            }
        }
    }
}
