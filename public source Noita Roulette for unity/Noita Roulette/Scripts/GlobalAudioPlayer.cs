using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PulseMotion.HighTechUI
{
    public class GlobalAudioPlayer : MonoBehaviour
    {
        public static GlobalAudioPlayer GetInstance(GameObject prefab)
        {
            var p = GameObject.FindObjectOfType<GlobalAudioPlayer>();
            if (p != null)
            {
                return p;
            }
            else if (prefab != null)
            {
                GameObject obj = GameObject.Instantiate<GameObject>(prefab);
                return obj.GetComponent<GlobalAudioPlayer>();
            }
            else
            {
                return null;
            }
        }

        public AudioSource _audioSource = null;
        public AudioClip _clickSound = null;
        public float _clickVolume = 1.0f;
        
        void Start()
        {
            if (_audioSource == null)
            {
                _audioSource = gameObject.GetComponent<AudioSource>();
            }
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_audioSource != null)
            {
                Debug.Log("GlobalAudioPlayer::PlayMusic");
                _audioSource.Play();
            }
        }

        /*
         * The current volume of the music
         */
        public float musicVolume
        {
            get
            {
                return _audioSource != null ? _audioSource.volume : 1;
            }
            set
            {
                if (_audioSource != null)
                {
                    _audioSource.volume = value;
                }
            }
        }

        /*
         * Switch to a new music track
         */
        public void PlayMusic(AudioClip music)
        {
            if (_audioSource != null)
            {
                Debug.Log("GlobalAudioPlayer::PlayMusic");
                _audioSource.Stop();
                _audioSource.clip = music;
                _audioSource.Play();
            }
        }

        /*
         * Play a click sound
         */
        public void PlayClick()
        {
            if (_audioSource != null && _clickSound != null)
            {
                Debug.Log("GlobalAudioPlayer::PlayClick");
                _audioSource.PlayOneShot(_clickSound, _clickVolume);
            }
        }
    }
}
