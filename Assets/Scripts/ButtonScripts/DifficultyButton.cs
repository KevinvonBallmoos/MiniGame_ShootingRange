using Difficulty;
using Manager;
using UnityEngine;

namespace ButtonScripts
{
    /// <summary>
    /// This class handles the hover over UI elements
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">16.11.2024</para>
    public class DifficultyButton : MonoBehaviour
    {
        /// <summary>
        /// Sets the difficulty
        /// </summary>
        public void SetDifficulty_Click()
        {
            if (gameObject.name.ToLower().Contains("easy"))
            {
                LevelManager.Instance.Difficulty = EDifficulty.Easy;
            }
            else if (gameObject.name.ToLower().Contains("mid"))
            {
                LevelManager.Instance.Difficulty = EDifficulty.Mid;
            }
        }
    }
}