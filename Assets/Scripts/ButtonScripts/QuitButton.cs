using Manager;
using UnityEngine;

namespace ButtonScripts
{
    /// <summary>
    /// This class handles the quit button
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">16.11.2024</para>
    public class QuitButton : MonoBehaviour
    {
        /// <summary>
        /// Method to quit the game
        /// </summary>
        public void QuitGame_Click()
        {
            UIManager.Instance.QuitGame();
        }
    }
}