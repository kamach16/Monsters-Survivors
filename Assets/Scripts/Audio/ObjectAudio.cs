using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class ObjectAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip[] stepsSounds;
        [SerializeField] private AudioClip hpGainedSound;
        [SerializeField] private AudioClip powerUpGainedSound;
        [SerializeField] private AudioClip getDamageSound;

        public void PlayAttackSound()
        {
            audioSource.PlayOneShot(attackSound);
        }

        public void PlayMovementSound()
        {
            audioSource.PlayOneShot(stepsSounds[Random.Range(0, stepsSounds.Length)]);
        }

        public void PlayHPGainedSound()
        {
            audioSource.PlayOneShot(hpGainedSound);
        }

        public void PlayPowerUpGainedSound()
        {
            audioSource.PlayOneShot(powerUpGainedSound);
        }

        public void PlayGetDamageSound()
        {
            audioSource.PlayOneShot(getDamageSound);
        }
    }
}
