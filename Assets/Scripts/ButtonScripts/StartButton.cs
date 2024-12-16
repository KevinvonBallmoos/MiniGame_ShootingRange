using Manager;
using UnityEngine;

namespace ButtonScripts
{
    /// <summary>
    /// This class handles the start button
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">16.11.2024</para>
    public class StartButton : MonoBehaviour
    {
        /// <summary>
        /// Method to quit the game
        /// </summary>
        public void StartGame_Click()
        {
            GameManager.Instance.StartGamePreparation();
        }
    }
}
