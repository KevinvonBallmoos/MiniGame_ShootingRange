using System.Collections.Generic;
using Manager;

namespace HighScore
{
    /// <summary>
    /// Object class to cache file objects for serializing it into a json file
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">24.11.2024</para>
    public class FileSerializeObject
    {
        public readonly List<HighScoreObject> HighScoreListEasy;
        public readonly List<HighScoreObject> HighScoreListMid;

        /// <summary>
        /// Constructor to set the high score list
        /// </summary>
        /// <param name="highScoreListEasy">The high score list for the easy mode</param>
        /// <param name="highScoreListMid">The high score list for the intermediate mode</param>
        public FileSerializeObject(List<HighScoreObject> highScoreListEasy, List<HighScoreObject> highScoreListMid)
        {
            HighScoreListEasy = highScoreListEasy;
            HighScoreListMid = highScoreListMid;
        }
    }
}