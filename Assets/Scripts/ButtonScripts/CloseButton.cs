using Menu;
using UnityEngine;

namespace ButtonScripts
{
    /// <summary>
    /// This class handles the close buttons
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class CloseButton : MonoBehaviour
    {
        /// <summary>
        /// Handles the close event of the windows
        /// </summary>
        public void CloseWindow_Click()
        {
            if (gameObject.name.Contains("Menu"))
            {
                MenuHandler.Instance.HandleMenuWindowState(true);
            }
            else if (gameObject.name.Contains("Settings"))
            {
                MenuHandler.Instance.HandleSettingsWindowState(false);
            }
            else if (gameObject.name.Contains("Tutorial"))
            {
                MenuHandler.Instance.HandleTutorialWindowState(false);
            }
        }
    }
}