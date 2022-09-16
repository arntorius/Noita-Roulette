using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PulseMotion.HighTechUI
{
    public class LauncherUI : BaseUI
    {
        [Header("UI Controls")]
        [Tooltip("The main UI panel for the launcher screen")]
        public GameObject _panel = null;
        [Tooltip("The UI Text control to display the loading message with (optional)")]
        public GameObject _loadingText = null;

        protected override void OnPause(bool paused)
        {
        }

        protected override void OnLoadScene(string sceneName)
        {
            if (_panel != null) { _panel.SetActive(false); }
            if (_loadingText != null) { _loadingText.SetActive(true); }
        }
    }
}
