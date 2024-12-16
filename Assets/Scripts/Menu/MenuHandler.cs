using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HighScore;
using Manager;
using Player;
using Settings;
using TimeEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    /// <summary>
    /// MenuHandler handles UI events of the menu
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">30.11.2024</para>
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _inGameMenu;
        [SerializeField] private AudioSource _bgmSound;
        private GameObject _settings;
        private GameObject _tutorial;
        
        private TextMeshProUGUI _inputField;
        private Coroutine _coroutine;
        // Instance
        public static MenuHandler Instance;
        
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
            HandleMenuWindowState(false);
            AudioListener.volume = SettingsViewModel.Settings.MaxVolume;
            if (SettingsViewModel.Settings.BackGroundMusicEnabled)
            {
                _bgmSound.Play();
            }
            
            _settings = _inGameMenu.transform.Find("Settings").gameObject;
            _tutorial = _inGameMenu.transform.Find("Tutorial").gameObject;
            var inputField = _inGameMenu.GetComponentInChildren<TMP_InputField>();
            inputField.text = SettingsViewModel.PlayerName;
        }

        #endregion
        
        #region Update

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            HandleMenuWindowState(_inGameMenu.activeSelf);
        }

        #endregion
        
        #region Menu State
        
        /// <summary>
        /// Method to open the menu
        /// </summary>
        /// <param name="isActive">True if the state should be active, false when not</param>
        public void HandleMenuWindowState(bool isActive)
        {
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerCameraView), isActive);
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerGun), isActive);
            _inGameMenu.SetActive(!isActive);

            if (isActive)
            {
                HandleSettingsWindowState(false);
                HandleTutorialWindowState(false);
            }

            Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
        }
        
        #endregion
        
        #region Set Name

        /// <summary>
        /// Sets the player name
        /// </summary>
        /// <param name="infoText">The saved Text game object</param>
        public void SetName(GameObject infoText)
        {
            var gameObjects = _inGameMenu.GetComponentsInChildren<TextMeshProUGUI>();
            _inputField = gameObjects.First(g => g.name.Equals("Input"));
            SettingsViewModel.PlayerName = _inputField.text;
            SettingsJsonController.SaveSettings();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            StartCoroutine(StartTimerCoroutine(infoText));
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
        
        #region ResetScore

        /// <summary>
        /// Method to reset the score
        /// </summary>
        /// <param name="infoText">The saved Text game object</param>
        public void ResetScore_Click(GameObject infoText)
        {
            HighScoreHandler.Instance.ResetScore();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            StartCoroutine(StartTimerCoroutine(infoText));
        }
        
        #endregion
        
        #region Settings
        
        /// <summary>
        /// Closes the tutorial window
        /// </summary>
        /// <param name="isActive">True if the state should be active, false when not</param>
        public void HandleSettingsWindowState(bool isActive)
        {
            _settings.SetActive(isActive);   
        }
        
        #endregion
        
        #region Tutorial
        
        /// <summary>
        /// Closes the tutorial window
        /// </summary>
        /// <param name="isActive">True if the state should be active, false when not</param>
        public void HandleTutorialWindowState(bool isActive)
        {
            _tutorial.SetActive(isActive);   
        }
        
        #endregion
    }
}