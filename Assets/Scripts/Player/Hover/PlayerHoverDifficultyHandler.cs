using Difficulty;
using InfoLabel;
using Player.Hover.Base;
using UnityEngine;

namespace Player.Hover
{
    /// <summary>
    /// This class handles the hover over game objects
    /// Difficulty
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>
    public class PlayerHoverDifficultyHandler : PlayerHoverHandlerBase
    {
        #region OnPointer Methods
        
        /// <summary>
        /// Mouse entered the object
        /// Updates the according label
        /// </summary>
        /// <param name="hitObject">The object that was hit</param>
        protected override void OnPointerEnter(GameObject hitObject)
        {
            switch (hitObject.name)
            {
                case "ButtonEasy": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Front", $"Targets: {DifficultySettings.GetTargetCount(EDifficulty.Easy)} " +
                                                                        $"\nTime:  {DifficultySettings.GetTimeLeft(EDifficulty.Easy)}s " +
                                                                        $"\nAmmo: Infinite");
                    break;
                case "ButtonMid": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Front", $"Targets: {DifficultySettings.GetTargetCount(EDifficulty.Mid)} " +
                                                                        $"\nTime: {DifficultySettings.GetTimeLeft(EDifficulty.Mid)}s " +
                                                                        $"\nAmmo: {DifficultySettings.GetBulletCount(EDifficulty.Mid)} Shots");
                    break;
            }
        }

        /// <summary>
        /// Mouse exited the object
        /// Updates the according label
        /// </summary>
        /// <param name="hitObject">The object that was hit</param>
        protected override void OnPointerExit(GameObject hitObject)
        {
            if (hitObject.name.Equals("ButtonEasy") || hitObject.name.Equals("ButtonMid"))
            {
                InfoLabelHandler.Instance.SetInfoLabelText("Front","Choose a difficulty");
            }
        }
        
        #endregion
    }
}