using TMPro;
using UnityEngine;

namespace InfoLabel
{
    /// <summary>
    /// This class handles all info label objects
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">18.11.2024</para>
    public class InfoLabelHandler : MonoBehaviour
    {
        // Info labels
        [Header("InfoLabels")]
        [SerializeField] private TextMeshProUGUI _infoLabelBack;
        [SerializeField] private TextMeshProUGUI _infoLabelFront;
        [SerializeField] private TextMeshProUGUI _infoLabelLeft;
        [SerializeField] private TextMeshProUGUI _infoLabelRight;
        // High score labels
        [Header("HighScoreLabels")]
        [SerializeField] private TextMeshProUGUI _highScoreLabelEasy;
        [SerializeField] private TextMeshProUGUI _highScoreLabelMid;
        // Canvas labels
        [Header("CanvasLabels")] [SerializeField] private TextMeshProUGUI _canvasLabelCenter;
        // Weapon labels
        [Header("WeaponLabels")]
        
        // Instance
        public static InfoLabelHandler Instance;
        
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
        
        #region Get InfoLabels

        /// <summary>
        /// Gets the text of specified info label 
        /// </summary>
        /// <param name="labelName">The label to display the text</param>
        public string GetInfoLabelText(string labelName)
        {
            switch (labelName)
            {
                case "Right":
                    return _infoLabelRight.text;
                default:
                    return "";
            }
        }

        #endregion

        #region Set InfoLabels

        /// <summary>
        /// Sets the text on the specified info label 
        /// </summary>
        /// <param name="textComponent">The text mesh component to set the text</param>
        /// <param name="infoText">The text to display</param>
        public void SetInfoLabelText(TextMeshProUGUI textComponent, string infoText)
        {
            textComponent.text = infoText;
        }
        
        /// <summary>
        /// Sets the text on the specified info label 
        /// </summary>
        /// <param name="labelName">The label to display the text</param>
        /// <param name="infoText">The text to display</param>
        /// <param name="isAppend">Determines if the text should be appended or not</param>
        public void SetInfoLabelText(string labelName, string infoText, bool isAppend = false)
        {
            switch (labelName)
            {
                case "Back":
                    _infoLabelBack.text = infoText;
                    break;
                case "Front":
                    _infoLabelFront.text = infoText;
                    break;  
                case "Left":
                    _infoLabelLeft.text = infoText;
                    break;
                case "Right":
                    _infoLabelRight.text = infoText;
                    break;
                case "EasyDiff":
                    if (isAppend)
                        _highScoreLabelEasy.text += infoText;
                    else
                        _highScoreLabelEasy.text = infoText;
                    break;
                case "MidDiff":
                    if (isAppend)
                        _highScoreLabelMid.text += infoText;
                    else
                        _highScoreLabelMid.text = infoText;
                    break;
                case "CanvasCenter":
                    _canvasLabelCenter.enabled = true;
                    _canvasLabelCenter.text = infoText;
                    break;
            }
        }
        
        #endregion
    }
}