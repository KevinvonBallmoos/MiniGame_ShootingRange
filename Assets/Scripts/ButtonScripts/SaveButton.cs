using Menu;
using Settings;
using UnityEngine;

namespace ButtonScripts
{
    /// <summary>
    /// This class handles the save buttons
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class SaveButton : MonoBehaviour
    {
        /// <summary>
        /// Handles the close event of the windows
        /// </summary>
        public void SaveState_Click(GameObject infoLabel)
        {
            if (gameObject.name.Contains("Name"))
            {
                MenuHandler.Instance.SetName(infoLabel);
            }
            else if (gameObject.name.Contains("Settings"))
            {
                SettingsHandler.Instance.SaveSettings(infoLabel);
            }
        }
    }
}