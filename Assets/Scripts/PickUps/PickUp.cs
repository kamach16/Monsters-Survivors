using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Audio;

public class PickUp : MonoBehaviour
{
    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float healthToAdd;
    [SerializeField] private float timeToActive;
    [SerializeField] private GameObject model;
    [SerializeField] private ParticleSystem pickUpEffect;
    [SerializeField] private ObjectAudio objectAudio;

    private float timeSinceDeactivate;
    private bool isActive = true;

    private PlayerHealth playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" || !isActive) return;

        if (playerHealth == null) playerHealth = other.GetComponent<PlayerHealth>();
        PickUpObject();
    } 

    private void Update()
    {
        ActivateAndDeactivatePickUp();
    }

    private void ActivateAndDeactivatePickUp()
    {
        if (!isActive) timeSinceDeactivate += Time.deltaTime;

        if (timeSinceDeactivate >= timeToActive)
        {
            model.SetActive(true);
            isActive = true;
            timeSinceDeactivate = 0;
        }
    }

    private void PickUpObject()
    {
        switch (pickUpType)
        {
            case PickUpType.health:
                playerHealth.IncreaseCurrentHealth(healthToAdd);
                model.SetActive(false);
                isActive = false;
                pickUpEffect.Play();
                objectAudio.PlayHPGainedSound();
                break;

            default:
                break;
        }
    }
}
