using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PulseMotion.HighTechUI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public GameObject _audioPlayerPrefab = null;
        protected GlobalAudioPlayer _audioPlayer = null;
        protected bool _paused = false;

        [Header("Key Bindings")]
        [Tooltip("Name of the pause button axis configured in Project Settings > Input (optional)")]
        public string _pauseButton = "";
        [Tooltip("Name of the restart button axis configured in Project Settings > Input (optional)")]
        public string _restartButton = "";
        [Tooltip("Name of the quit button axis configured in Project Settings > Input (optional)")]
        public string _quitButton = "";

        // Use this for initialization
        protected virtual void Start()
        {
            _audioPlayer = GlobalAudioPlayer.GetInstance(_audioPlayerPrefab);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // Handle key bindings
            if (!string.IsNullOrEmpty(_quitButton))
            {
                if (Input.GetButtonDown(_quitButton))
                {
                    Quit();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(_restartButton))
            {
                if (Input.GetButtonDown(_restartButton))
                {
                    RestartScene();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(_pauseButton))
            {
                if (Input.GetButtonDown(_pauseButton))
                {
                    TogglePause();
                }
            }
        }

        protected void PlayClick()
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.PlayClick();
            }
        }

        /*
         * Toggle the current paused state
         */
        public void TogglePause()
        {
            PlayClick();
            this.paused = !_paused;
        }

        /**
         * Whether or not the game is currently paused
         */
        public bool paused
        {
            get
            {
                return _paused;
            }
            set
            {
                _paused = value;
                OnPause(_paused);
                Time.timeScale = _paused ? 0.0f : 1.0f;
            }
        }

        protected abstract void OnPause(bool paused);

        /**
         * Restart the current scene
         */
        public void RestartScene()
        {
            PlayClick();
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /**
         * Load a new scene
         */
        public void LoadScene(string sceneName)
        {
            PlayClick();

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogErrorFormat("LoadScene({0}): scene name not specified", sceneName);
            }
            else if (!Application.CanStreamedLevelBeLoaded(sceneName))
            {
                Debug.LogErrorFormat("LoadScene({0}): scene {0} not found", sceneName);
            }
            else
            {
                Debug.LogFormat("LoadScene({0})", sceneName);
                OnLoadScene(sceneName);
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(sceneName);
            }
        }

        protected abstract void OnLoadScene(string sceneName);

        /**
         * Exit the game and return to the operating system
         */
        public void Quit()
        {
            PlayClick();
            Application.Quit();
        }
    }
}
