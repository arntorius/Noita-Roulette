using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PulseMotion.HighTechUI
{
    public class HudUI : BaseUI
    {
        private int _selectedLevel = 0;

        [Header("UI Controls")]
        [Tooltip("The pause window UI Panel")]
        public GameObject _pauseWindow = null;
        [Tooltip("The UI Text control to display the loading message with (optional)")]
        public GameObject _loadingText = null;
        [Tooltip("The UI Text control to display the selected level with (optional)")]
        public Text _selectedLevelText = null;
        [Tooltip("The list of player stats to display on the HUD and keep updated (optional)")]
        public PlayerStat[] _playerStats = new PlayerStat[0];
        [Tooltip("Configuration for how to diplay the time in the HUD")]
        public TimeStat _timeStat = new TimeStat();

        [Header("Game State")]
        [Tooltip("GameObject which contains a script that implements IPlayer")]
        public GameObject _player = null;

        [Serializable]
        public class PlayerStat
        {
            [Tooltip("The key to pass to IPlayer.GetPlayerStat")]
            public string key = null;
            [Tooltip("The UI Text control for displaying this stat")]
            public Text text = null;
            [Tooltip("The UI Image control for displaying this stat")]
            public Image image = null;
        }

        [Serializable]
        public class TimeStat
        {
            [Tooltip("The the time limit for the current scene (zero means no limit)")]
            public int timeLimitInSeconds = 0;
            [Tooltip("Whether to count up or down if using a time limit")]
            public bool countDown = true;
            [Tooltip("The UI Text control for displaying this stat")]
            public Text text = null;
            [Tooltip("The UI Image control for displaying this stat")]
            public Image image = null;
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            // Get the level that was selected on the LevelSelect scene
            try
            {
                _selectedLevel =
                    PlayerPrefs.HasKey("SelectedLevel") ?
                    PlayerPrefs.GetInt("SelectedLevel") : 0;
            }
            catch (Exception ex)
            {
                _selectedLevel = 0;
                Debug.LogErrorFormat("Failed to get SelectedLevel: {0}", ex.Message);
            }

            // Display the selected level on screen
            if (_selectedLevelText != null)
            {
                _selectedLevelText.text = _selectedLevel.ToString();
            }

            // Show/Hide the pause window
            if (_pauseWindow != null)
            {
                _pauseWindow.SetActive(_paused);
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // Update the current statistics in the HUD
            if (_player != null && _playerStats != null && _playerStats.Length > 0)
            {
                var p = _player.GetComponent<IPlayer>();
                if (p != null)
                {
                    foreach (var s in _playerStats)
                    {
                        if (s.text != null)
                        {
                            s.text.text = p.GetPlayerStat(s.key);
                        }
                    }
                }
            }
            
            // Update the current time in the HUD
            if (_timeStat != null && _timeStat.text != null)
            {
                int time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
                if (_timeStat.timeLimitInSeconds > 0)
                {
                    if (_timeStat.countDown)
                    {
                        time = Math.Max(0, _timeStat.timeLimitInSeconds - time);
                    }
                    else
                    {
                        time = Math.Min(time, _timeStat.timeLimitInSeconds);
                    }
                }
                int seconds = time % 60;
                int minutes = time / 60;
                _timeStat.text.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
        }

        protected override void OnPause(bool paused)
        {
            _pauseWindow.SetActive(paused);
        }

        protected override void OnLoadScene(string sceneName)
        {
            if (_pauseWindow != null) { _pauseWindow.SetActive(false); }
            if (_loadingText != null) { _loadingText.SetActive(true); }
        }
        
        /**
         * The level that was selected on the LevelSelect scene
         */
        public int selectedLevel
        {
            get
            {
                return _selectedLevel;
            }
        }
    }
}
