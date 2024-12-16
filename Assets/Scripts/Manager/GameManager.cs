using System;
using InfoLabel;
using Player;
using Player.Hover;
using Settings;
using Target;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// Game manager handles the game logic
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">10.11.2024</para>
    public class GameManager : MonoBehaviour
    {
        // Free mode
        [NonSerialized] public bool IsFreeMode = true;
        // Instance
        public static GameManager Instance;
        
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
            SettingsViewModel.LoadSettings();
            TargetSpawner.Instance.SpawnStartTargets();
        }
        
        #endregion
        
        #region Game preparation
        
        /// <summary>
        /// Method to run the game
        /// </summary>
        public void StartGamePreparation()
        {
            PrepareAreaForGame();
        }
        
        /// <summary>
        /// Prepares the area for the game
        /// Clears the counter and disables scripts
        /// </summary>
        private void PrepareAreaForGame()
        {
            // Clear counter
            UIManager.Instance.UpdateCounter();
            
            // Set the text in the info labels
            InfoLabelHandler.Instance.SetInfoLabelText(
                "Back", "Choose the difficulty, the game will start instantly \n(Press Esc to open the Menu and read the rules \nor to end the game.)");
            InfoLabelHandler.Instance.SetInfoLabelText("Right", @"\ \ \ \ \ \ \ \ \ ");
            InfoLabelHandler.Instance.SetInfoLabelText("Front", "Choose a difficulty");
            
            // Enable and disable scripts
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerGun), false);
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerHoverPreGameHandler), false);
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerHoverDifficultyHandler), true);
        }
        
        #endregion
    }
}