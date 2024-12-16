using System.Collections.Generic;
using Player;
using Target;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// UI Manager handles UI events
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">10.11.2024</para>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<Animator> _animatorController;
        [SerializeField] private GameObject _counterBullets;
        [SerializeField] private GameObject _weapons;
        // Instance
        public static UIManager Instance;

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
        /// Start method
        /// </summary>
        void Start()
        {
            // QuitButton appears
            PlayAnimation(0, "GoUp");
        }

        #endregion
        
        #region Game preparation

        /// <summary>
        /// Clears the counter when the game starts
        /// </summary>
        public void UpdateCounter()
        {
            _counterBullets.SetActive(false);
            _weapons.SetActive(false);
            TargetSpawner.Instance.ClearSpawnPoints();

            PlayAnimation(0, "GoDown");
            PlayAnimation(1, "GoDown");

            PlayAnimation(2, "GoUp");
            PlayAnimation(3, "GoUp");
        }

        /// <summary>
        /// Resets the Game after a round is done
        /// </summary>
        public void ResetGame()
        {
            _counterBullets.SetActive(true);
            _weapons.SetActive(true);
            
            TargetSpawner.Instance.ClearSpawnPoints();
            TargetSpawner.Instance.SpawnStartTargets();
     
            GameManager.Instance.IsFreeMode = true;
            PlayerScriptHandler.Instance.EnableScript(nameof(PlayerGun), false);

            PlayAnimation(0, "GoUp");
            PlayAnimation(1, "GoUp");
        }
        
        #endregion

        #region Play Animations

        /// <summary>
        /// Starts the animation
        /// animatorController[0]: QuitButton 
        /// animatorController[1]: StartButton
        /// animatorController[2]: DifficultyButtonEasy
        /// animatorController[3]: DifficultyButtonMid 
        /// </summary>
        /// <param name="animatorIndex">The animator to execute the animation</param>
        /// <param name="triggerName">Which animation to trigger</param>
        public void PlayAnimation(int animatorIndex, string triggerName)
        {
            _animatorController[animatorIndex].SetTrigger(triggerName);
        }

        #endregion

        #region Quit Game

        /// <summary>
        /// Quits the game, and returns to the desktop
        /// </summary>
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion
    }
}