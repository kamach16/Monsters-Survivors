using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class CharacterAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip attackSound;


        public void PlayAttackSound()
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
