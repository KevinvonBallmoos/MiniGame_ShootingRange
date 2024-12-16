using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace HighScore
{
    /// <summary>
    /// JsonController handles save and load of data involving Json Files
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">24.11.2024</para>
    public static class HighScoreJsonController
    {
        // Save path
        private static readonly string SavePath = Application.persistentDataPath + "/HighScore.json";

        #region HighScore

        /// <summary>
        /// Saves the high scores in a json file
        /// </summary>
        /// <param name="highScoreListEasy">The high score list for the easy mode</param>
        /// <param name="highScoreListMid">The high score list for the intermediate mode</param>
        public static void SaveHighScore(List<HighScoreObject> highScoreListEasy, List<HighScoreObject> highScoreListMid)
        {
            var fileObject = new FileSerializeObject(highScoreListEasy, highScoreListMid);
            var json = JsonConvert.SerializeObject(fileObject, Formatting.Indented);
            File.WriteAllText(SavePath, json);
        }
        
        /// <summary>
        /// Loads the high scores from the json file
        /// </summary>
        /// <returns>File serialize object</returns>
        public static FileSerializeObject LoadHighScore()
        {
            try
            {
                var json = File.ReadAllText(SavePath, Encoding.UTF8);
                return JsonConvert.DeserializeObject<FileSerializeObject>(json);
            }
            catch (Exception)
            {
                return new FileSerializeObject(new List<HighScoreObject>(), new List<HighScoreObject>());
            }
        }
        
        #endregion
    }
}