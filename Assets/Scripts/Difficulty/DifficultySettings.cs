namespace Difficulty
{
    /// <summary>
    /// This class holds the difficulty settings
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>
    public static class DifficultySettings
    {
        #region Difficulty settings

        /// <summary>
        /// Gets the target count, according to the difficulty
        /// </summary>
        /// <param name="difficulty">The difficulty level</param>
        /// <returns>Count of targets</returns>
        public static int GetTargetCount(EDifficulty difficulty)
        {
            return difficulty switch
            {
                EDifficulty.Easy => 15,
                EDifficulty.Mid => 20,
                _ => 0
            };
        }

        /// <summary>
        /// Gets the time count, according to the difficulty
        /// </summary>
        /// <param name="difficulty">The difficulty level</param>
        /// <returns>Time count</returns>
        public static float GetTimeLeft(EDifficulty difficulty)
        {
            return difficulty switch
            {
                EDifficulty.Easy => 23f,
                EDifficulty.Mid => 20f,
                _ => 0f
            };
        }
        
        /// <summary>
        /// Gets the amount of bullets, according to the difficulty
        /// </summary>
        /// <param name="difficulty">The difficulty level</param>
        /// <returns>Amount of bullets</returns>
        public static string GetBulletCount(EDifficulty difficulty)
        {
            return difficulty switch
            {
                EDifficulty.Easy => "∞",
                EDifficulty.Mid => "30",
                _ => "0"
            };
        }

        #endregion
    }
}