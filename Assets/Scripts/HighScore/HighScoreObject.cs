namespace HighScore
{
    /// <summary>
    /// Object class to store high score objects with Information like
    /// player and score
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">24.11.2024</para>
    [System.Serializable]
    public class HighScoreObject
    {
        public string Player { get; set; }
        public float TimeLeft { get; set; }
    }
}