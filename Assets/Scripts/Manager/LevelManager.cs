using System.Collections;
using Difficulty;
using HighScore;
using InfoLabel;
using Player;
using Player.Hover;
using Settings;
using Target;
using TimeEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Manager
{
    /// <summary>
    /// Level Manager handles the score and win or loose situation
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        // Coroutine
        private Coroutine _timer;
        // Weapon
        private TextMeshProUGUI _weaponText;
        // Instance
        public static LevelManager Instance;
        // Remaining Time
        private float _remainingTime;
        // Has Round Begun
        private bool _hasRoundBegun;
        
        #region Game State
        
        // Game state enum
        private enum EGameState
        {
            Win,
            Loose
        }

        private EGameState GameState;
        
        #endregion
        
        #region ScoreCount property

        private int ScoreCount { get; set; }

        /// <summary>
        /// Updates the score
        /// </summary>
        public void UpdateScore()
        {
            if (!GameManager.Instance.IsFreeMode)
            {
                ScoreCount++;
                InfoLabelHandler.Instance.SetInfoLabelText("Back", $"Targets down \n{ScoreCount} / {DifficultySettings.GetTargetCount(Difficulty)}");

                // If the score count matches the target count, the player wins
                if (ScoreCount == DifficultySettings.GetTargetCount(Difficulty))
                {
                    GameState = EGameState.Win;
                    ScoreCount = 0;
                    StopCoroutine(_timer);
                    EndRound();
                }
                else if (Difficulty == EDifficulty.Mid && PlayerGun.Instance.BulletsShot == int.Parse(DifficultySettings.GetBulletCount(Difficulty)))
                {
                    GameState = EGameState.Loose;
                    ScoreCount = 0;
                    StopCoroutine(_timer);
                    InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter", $"Out of Ammo!\nYou loose :(");
                    StartCoroutine(StartTimerCoroutine(3));
                    EndRound();
                }
            }
        }
        
        #endregion

        #region Difficulty property
        
        // Difficulty changed event
        public DifficultyChangedEvent OnDifficultyChanged;

        private EDifficulty _difficulty;
        public EDifficulty Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                OnDifficultyChanged?.Invoke(_difficulty);
            }
        }

        #endregion

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

        #endregion

        #region Start Round

        /// <summary>
        /// Method starts the round
        /// </summary>
        public void ReadyRound()
        {
            if (_hasRoundBegun) return;
            
            _hasRoundBegun = true;
            GameManager.Instance.IsFreeMode = false;
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerHoverDifficultyHandler), false);

            // Difficulty Buttons go down
            UIManager.Instance.PlayAnimation(2, "GoDown");
            UIManager.Instance.PlayAnimation(3, "GoDown");

            // Add timer on right wall
            InfoLabelHandler.Instance.SetInfoLabelText("Right", $"Time left \n{DifficultySettings.GetTimeLeft(Difficulty)}s");
            // Target score on back wall
            ScoreCount = 0;
            InfoLabelHandler.Instance.SetInfoLabelText("Back", $"Targets down \n{ScoreCount} / {DifficultySettings.GetTargetCount(Difficulty)}");
            // Set Ammo amount
            PlayerGun.Instance.IsBulletCountActive = Difficulty == EDifficulty.Mid;
            _weaponText = _player.transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            InfoLabelHandler.Instance.SetInfoLabelText(_weaponText, $"∞\n---\n{DifficultySettings.GetBulletCount(Difficulty)}");
            
            // Activate Timer
            StartCoroutine(StartTimerCoroutine(0));
        }
        
        /// <summary>
        /// Starts the timer and spawns targets
        /// </summary>
        private void StartRound()
        {
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerGun), true);
            // Spawn targets
            TargetSpawner.Instance.SpawnStartTargets();
            if (_timer != null)
            {
                StopCoroutine(_timer);
            }
            StartCoroutine(StartTimerCoroutine(1));
        }
        
        #endregion
        
        #region Check Round State

        /// <summary>
        /// When the round ends
        /// </summary>
        private void EndRound()
        {
            UIManager.Instance.ResetGame();
            if (GameState == EGameState.Win)
            {
                _remainingTime = float.Parse(InfoLabelHandler.Instance.GetInfoLabelText("Right"));
                InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter", $"All targets down! \nTime remaining: {_remainingTime}s \nYou win :)");
                InfoLabelHandler.Instance.SetInfoLabelText("Back", "You WIN!");
                StartCoroutine(StartTimerCoroutine(2));

                HandleHighScore();
            }
            else
            {
                InfoLabelHandler.Instance.SetInfoLabelText("Back", "You LOOSE!");
                StartCoroutine(StartTimerCoroutine(2));
            }

            _hasRoundBegun = false;
            PlayerGun.Instance.IsBulletCountActive = false;
            PlayerGun.Instance.BulletsShot = 0;
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerGun), true);
            InfoLabelHandler.Instance.SetInfoLabelText(_weaponText, $"{DifficultySettings.GetBulletCount(EDifficulty.Easy)}\n---\n{DifficultySettings.GetBulletCount(EDifficulty.Easy)}");
        }   
        
        #endregion
        
        #region HighScore

        /// <summary>
        /// Handles the high score list
        /// </summary>
        private void HandleHighScore()
        {
            var highScoreInstance = HighScoreHandler.Instance;
            var highScoreObject = new HighScoreObject { Player = SettingsViewModel.PlayerName, TimeLeft = _remainingTime };
            
            highScoreInstance.AddNewHighScore(_difficulty, highScoreObject);
            highScoreInstance.LoadHighScoreListsIntoLabels();
            
            var isInTopFive = highScoreInstance.IsHighScoreTop5(_difficulty, highScoreObject, out int rank);

            InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter",
                isInTopFive
                    ? $"You made it under the Top 5 :) \n Your rank: #{rank}"
                    : $"You didn't make it under the Top 5 :( \n Your rank: #{rank}");

            StartCoroutine(StartTimerCoroutine(3));
        }
        
        #endregion
        
        #region Helper
        
        /// <summary>
        /// Starts different coroutines
        /// </summary>
        /// <param name="timerIndex">Which timer to start
        /// 0: Timer to start the round
        /// 1: Timer that says how much time left before the round ends</param>
        /// 2: Time that displays when the player has won
        /// 3: To empty the canvas center
        /// <returns>Coroutine</returns>
        private IEnumerator StartTimerCoroutine(int timerIndex)
        {
            switch (timerIndex)
            {
                case 0:
                    yield return StartCoroutine(TimeEventHandler.Instance.StartTimer(3f, "CanvasCenter", true));
                    StartRound();
                    break;
                case 1:
                    _timer = StartCoroutine(TimeEventHandler.Instance.StartTimer(DifficultySettings.GetTimeLeft(Difficulty), "Right", false));
                    yield return _timer;
                    GameState = EGameState.Loose;
                    InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter", "You loose :(");
                    EndRound();
                    break;
                case 2:
                    yield return StartCoroutine(TimeEventHandler.Instance.StartTimer(3f));
                    InfoLabelHandler.Instance.SetInfoLabelText("Back",
                        GameState == EGameState.Win ? "\"You WIN!\n<- Look at the HighScore list :)" : "");
                    break;
                case 3:
                    yield return StartCoroutine(TimeEventHandler.Instance.StartTimer(5f));
                    InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter", "");
                    break;
            }
        }

        /// <summary>
        /// Checks if the all bullets have been shot
        /// this ends the game and the player looses
        /// </summary>
        /// <param name="bulletsShot"></param>
        public void CheckBulletCount(int bulletsShot)
        {
            var bulletCount = DifficultySettings.GetBulletCount(Difficulty);
            InfoLabelHandler.Instance.SetInfoLabelText(_weaponText, $"{int.Parse(bulletCount) - bulletsShot}\n---\n{bulletCount}");
        }
        
        #endregion
    }
}