using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;

    [HideInInspector] public float damage;

    private void Update()
    {
        MoveProjectile();
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void AimTarget(Transform target)
    {
        transform.LookAt(target);
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
