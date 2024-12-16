using Menu;
using Player.Interact;
using UnityEngine;

namespace ButtonScripts
{
    /// <summary>
    /// This class handles the menu buttons settings and tutorial
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class MenuButton : MonoBehaviour
    {
        /// <summary>
        /// Handles the close event of the windows
        /// </summary>
        public void HandleEvent_Click()
        {
            if (gameObject.name.Contains("Settings"))
            {
                MenuHandler.Instance.HandleSettingsWindowState(true);
            }
            else if (gameObject.name.Contains("Tutorial"))
            {
                MenuHandler.Instance.HandleTutorialWindowState(true);
            }
            else if (gameObject.name.Contains("Weapons"))
            {
                PlayerObjectInteractor.Instance.ResetWeapons();
            }
        }
    }
}