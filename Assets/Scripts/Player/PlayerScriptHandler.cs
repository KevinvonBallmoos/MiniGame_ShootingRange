using Player.Hover;
using UnityEngine;

namespace Player
{
    public class PlayerScriptHandler : MonoBehaviour
    {
        // Player
        [SerializeField] private GameObject _player;
        // Scripts
        private PlayerGun _playerGunScript;
        private PlayerCameraView _playerCameraView;
        private PlayerHoverPreGameHandler _playerHoverPreGameHandlerScript;
        private PlayerHoverDifficultyHandler _playerHoverDifficultyHandlerScript;
        // Instance
        public static PlayerScriptHandler Instance;

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
        /// Gets the scripts from the player
        /// </summary>
        private void Start()
        {
            _playerGunScript = _player.GetComponent<PlayerGun>();
            _playerCameraView = _player.GetComponentInChildren<PlayerCameraView>();
            _playerHoverPreGameHandlerScript = _player.GetComponent<PlayerHoverPreGameHandler>();
            _playerHoverDifficultyHandlerScript = _player.GetComponent<PlayerHoverDifficultyHandler>();
        }

        #endregion

        /// <summary>
        /// Enables or disables scripts on the player
        /// </summary>
        /// <param name="scriptName">The name of the script to enable or disable</param>
        /// <param name="enable">Determines if a script has to be enabled or disabled</param>
        public void EnableScript(string scriptName, bool enable)
        {
            switch (scriptName)
            {
                case nameof(PlayerGun):
                    _playerGunScript.enabled = enable;
                    break;
                case nameof(PlayerCameraView):
                    _playerCameraView.enabled = enable;
                    break;
                case nameof(PlayerHoverPreGameHandler):
                    _playerHoverPreGameHandlerScript.enabled = enable;
                    break;
                case nameof(PlayerHoverDifficultyHandler):
                    _playerHoverDifficultyHandlerScript.enabled = enable;
                    break;
            }
        }
    }
}