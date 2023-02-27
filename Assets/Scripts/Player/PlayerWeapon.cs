using System.Collections;
using UnityEngine;
using Audio;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] public float weaponDamage;
        [SerializeField] public float timeBetweenProjectiles;
        [SerializeField] private float projectileSpeed;
        [SerializeField] public bool bouncingBullets = false;
        [SerializeField] private LayerMask layersToIgnore;

        [SerializeField] private Vector3 weaponRecoil;

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnPosition;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private CharacterAudio characterAudio;

        private Vector3 cursorPosition;

        private Coroutine spawningProjectilesCoroutine;

        private void FixedUpdate()
        {
            Rotate();
        }

        public void SetBouncingBullets(bool isBouncingON)
        {
            bouncingBullets = isBouncingON;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

        public void SetWeaponDamage(float newWeaponDamage)
        {
            weaponDamage = newWeaponDamage;
        }

        public float GetTimeBetweenProjectiles()
        {
            return timeBetweenProjectiles;
        }

        public void SetTimeBetweenProjectiles(float newTimeBetweenProjectiles)
        {
            timeBetweenProjectiles = newTimeBetweenProjectiles;
        }

        private void Rotate() // rotate weapon to cursor direction
        {
            if (playerHealth.GetIsDead()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, ~layersToIgnore))
            {
                cursorPosition = hit.point;
            }

            transform.LookAt(cursorPosition);
            transform.Rotate(0, 0, -90); // it prevents weapon to rotate on z axis 
        }

        public void StartShooting()
        {
            spawningProjectilesCoroutine = StartCoroutine(StartSpawningProjectiles()); // this allows player to fire continously 
        }

        public void StopShooting()
        {
            StopCoroutine(spawningProjectilesCoroutine);
        }

        private IEnumerator StartSpawningProjectiles()
        {
            while (true)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
                characterAudio.PlayAttackSound();

                PlayerRfileProjectile playerRfileProjectile = projectile.GetComponent<PlayerRfileProjectile>();
                playerRfileProjectile.SetProjectileSpeed(projectileSpeed);
                playerRfileProjectile.SetProjectleMoveDirectionOffset(weaponRecoil);
                playerRfileProjectile.SetBouncingBullet(bouncingBullets);

                Destroy(projectile, 10f);
                yield return new WaitForSeconds(timeBetweenProjectiles);
            }
        }
    }
}
