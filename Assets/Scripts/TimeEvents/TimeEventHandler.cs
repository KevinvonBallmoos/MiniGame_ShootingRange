using System;
using System.Collections;
using InfoLabel;
using UnityEngine;

namespace TimeEvents
{
    /// <summary>
    /// Class handles the time events such as countdowns and time left state
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>    
    public class TimeEventHandler : MonoBehaviour
    {
        public static TimeEventHandler Instance;
        
        #region Awake

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
        
        #endregion
        
        /// <summary>
        /// Starts the timer
        /// </summary>
        /// <param name="time">How lang the time should run</param>
        /// <returns>Coroutine</returns>
        public IEnumerator StartTimer(float time)
        {
            var remainingTime = time;
            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        /// <param name="time">How lang the time should run</param>
        /// <param name="infoLabel">On which label the remainingTime should be printed</param>
        /// <param name="useCeil">True prints the time without decimal places, else with 2</param>
        /// <returns>Coroutine</returns>
        public IEnumerator StartTimer(float time, string infoLabel, bool useCeil)
        {
            var remainingTime = time;
            while (remainingTime > 0)
            {
                var text = useCeil? Mathf.Ceil(remainingTime) : Math.Round(remainingTime, 2);
                InfoLabelHandler.Instance.SetInfoLabelText(infoLabel, $"{text}");
                remainingTime -= Time.deltaTime;
                yield return null;
            }
            InfoLabelHandler.Instance.SetInfoLabelText(infoLabel, "");
        }
    }
}