using System;
using System.Collections;
using Manager;
using TimeEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    /// <summary>
    /// SettingsHandler handles the load and save of the settings
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class SettingsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _settings;
        [SerializeField] private TextMeshProUGUI _textMaxVolume;
        private Slider _maxVolume;
        private Toggle _backGroundMusic;
        private Toggle _bulletSound;
        // Coroutine
        private Coroutine _coroutine;
        // Instance
        public static SettingsHandler Instance;

        #region Awake and Start

        /// <summary>
        /// Initializes the instance
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        /// <summary>
        /// Start is called on the first frame
        /// </summary>
        private void Start()
        {
            GetSettingsComponents();
            LoadSettings();
        }

        #endregion

        #region Load Settings

        /// <summary>
        /// Loads the lists into the labels
        /// </summary>
        private void LoadSettings()
        {
            var settings = SettingsViewModel.Settings;
             _maxVolume.value = settings.MaxVolume;
             _backGroundMusic.isOn = settings.BackGroundMusicEnabled;
             _bulletSound.isOn = settings.BulletSoundEnabled;
        }

        #endregion

        #region Save Settings

        /// <summary>
        /// Saves the settings
        /// </summary>
        /// <param name="infoText">The saved Text displayed after Save was clicked</param>
        public void SaveSettings(GameObject infoText = null)
        {
            GetSettingsComponents();
            
            SettingsViewModel.Settings = new SettingsObject{ 
                MaxVolume = (float)Math.Round(_maxVolume.value, 2),
                BackGroundMusicEnabled = _backGroundMusic.isOn, 
                BulletSoundEnabled = _bulletSound.isOn };
            SettingsJsonController.SaveSettings();

            SettingsViewModel.LoadSettings();
            
            if (infoText == null) return;
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            StartCoroutine(StartTimerCoroutine(infoText));
        }

        #endregion

        #region Helper

        /// <summary>
        /// Gets the current state of the settings components
        /// </summary>
        private void GetSettingsComponents()
        {
            _maxVolume = _settings.GetComponentInChildren<Slider>();
            _backGroundMusic = _settings.GetComponentsInChildren<Toggle>()[0];
            _bulletSound = _settings.GetComponentsInChildren<Toggle>()[1];
        }

        /// <summary>
        /// Updates the max volume label when the slider is moved
        /// </summary>
        public void UpdateMaxVolumeLabel()
        {
            _maxVolume = _settings.GetComponentInChildren<Slider>();
            AudioListener.volume = _maxVolume.value;
            
            var volume = Math.Round(_maxVolume.value * 100);
            _textMaxVolume.text = $"Audio Volume\n{volume}%";
        }
        
        /// <summary>
        /// Starts a 3-second timer
        /// </summary>
        /// <param name="infoText">The saved Text displayed after Save was clicked</param>
        /// <returns>Coroutine</returns>
        private IEnumerator StartTimerCoroutine(GameObject infoText)
        {
            infoText.SetActive(true);
            yield return _coroutine = StartCoroutine(TimeEventHandler.Instance.StartTimer(2f));
            infoText.SetActive(false);
        }
        
        
        #endregion
    }
}