using UnityEngine.Events;

namespace Difficulty
{
    /// <summary>
    /// Class handles the difficulty has changed event
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">18.11.2024</para>
    [System.Serializable]
    public class DifficultyChangedEvent : UnityEvent<EDifficulty>
    {
    }
}