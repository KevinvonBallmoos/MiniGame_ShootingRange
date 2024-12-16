using System.Collections.Generic;
using System.Linq;
using Difficulty;
using InfoLabel;
using Manager;
using UnityEngine;

namespace HighScore
{
    /// <summary>
    /// HighScoreHandler handles the score and win or loose situation
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>
    public class HighScoreHandler : MonoBehaviour
    {
        // High score lists
        private List<HighScoreObject> HighScoreListEasy;
        private List<HighScoreObject> HighScoreListMid;
        // Instance
        public static HighScoreHandler Instance;

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
        /// </summary>
        private void Start()
        {
            HighScoreListEasy = HighScoreJsonController.LoadHighScore().HighScoreListEasy ?? new List<HighScoreObject>();
            HighScoreListMid = HighScoreJsonController.LoadHighScore().HighScoreListMid ?? new List<HighScoreObject>();

            LoadHighScoreListsIntoLabels();
        }

        #endregion

        #region Load HighScore Lists

        /// <summary>
        /// Loads the lists into the labels
        /// </summary>
        public void LoadHighScoreListsIntoLabels()
        {
            var text = "";
            for (int i = 0; i < HighScoreListEasy.Count; i++)
            {
                if (i < 5)
                {
                    text += $"\n{i + 1}. {HighScoreListEasy[i].Player} - Time left {HighScoreListEasy[i].TimeLeft}\n";
                }
            }

            text = text == "" ? "NoScore" : text;
            InfoLabelHandler.Instance.SetInfoLabelText("EasyDiff", text);

            text = "";
            for (int i = 0; i < HighScoreListMid.Count; i++)
            {
                if (i < 5)
                {
                    text += $"\n{i + 1}. {HighScoreListMid[i].Player} - Time left {HighScoreListMid[i].TimeLeft}\n";
                }
            }

            text = text == "" ? "NoScore" : text;
            InfoLabelHandler.Instance.SetInfoLabelText("MidDiff", text);
        }

        #endregion

        #region Save HighScore Lists

        /// <summary>
        /// Saves the high score lists
        /// </summary>
        private void SaveHighScoreLists()
        {
            HighScoreJsonController.SaveHighScore(HighScoreListEasy, HighScoreListMid);
        }

        #endregion

        #region Add HighScore

        /// <summary>
        /// Adds a new high score to the current difficulty list
        /// </summary>
        /// <param name="difficulty">The difficulty the user chose</param>
        /// <param name="highScoreObject">Adds a new high score to the desired list</param>
        public void AddNewHighScore(EDifficulty difficulty, HighScoreObject highScoreObject)
        {
            switch (difficulty)
            {
                case EDifficulty.Easy:
                    HighScoreListEasy.Add(highScoreObject);
                    HighScoreListEasy.Sort((a, b) => a.TimeLeft.CompareTo(b.TimeLeft));
                    HighScoreListEasy.Reverse();
                    break;
                case EDifficulty.Mid:
                    HighScoreListMid.Add(highScoreObject);
                    HighScoreListMid.Sort((a, b) => a.TimeLeft.CompareTo(b.TimeLeft));
                    HighScoreListMid.Reverse();
                    break;
            }

            SaveHighScoreLists();
        }

        #endregion

        #region Is Top 5

        /// <summary>
        /// Check if the score is under the top 5
        /// </summary>
        /// <param name="difficulty">The difficulty the user chose</param>
        /// <param name="highScoreObject">Adds a new high score to the desired list</param>
        /// <param name="rank">Gets the rank of the player</param>
        /// <returns>True when the new high score is under the top 5, else false</returns>
        public bool IsHighScoreTop5(EDifficulty difficulty, HighScoreObject highScoreObject, out int rank)
        {
            var highScoreList = difficulty switch
            {
                EDifficulty.Easy => HighScoreListEasy,
                EDifficulty.Mid => HighScoreListMid,
                _ => null
            };
            rank = 0;
            if (highScoreList == null)
                return false;

            rank = highScoreList.IndexOf(highScoreObject) + 1;
            if (rank > 5)
            {
                var label = difficulty == EDifficulty.Easy ? "EasyDiff" : "MidDiff";
                InfoLabelHandler.Instance.SetInfoLabelText(label,
                    $"\nYour rank: {rank}. \n{highScoreObject.Player} - Time left {highScoreObject.TimeLeft}\n", true);
            }

            return rank is > 0 and <= 5;
        }

        #endregion

        #region Reset Score

        /// <summary>
        /// Resets the high score
        /// </summary>
        public void ResetScore()
        {
            HighScoreJsonController.SaveHighScore(new List<HighScoreObject>(), new List<HighScoreObject>());
            HighScoreListEasy = HighScoreJsonController.LoadHighScore().HighScoreListEasy ?? new List<HighScoreObject>();
            HighScoreListMid = HighScoreJsonController.LoadHighScore().HighScoreListMid ?? new List<HighScoreObject>();
            LoadHighScoreListsIntoLabels();
        }

        #endregion
    }
}