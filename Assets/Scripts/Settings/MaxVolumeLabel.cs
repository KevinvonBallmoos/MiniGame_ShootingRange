using UnityEngine;

namespace Settings
{
    /// <summary>
    /// MaxVolumeLabel class handles the max volume label text
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class MaxVolumeLabel : MonoBehaviour
    {
        /// <summary>
        /// Is called once per frame
        /// </summary>
        public void UpdateMaxVolumeLabel()
        {
            SettingsHandler.Instance.UpdateMaxVolumeLabel();
        }
    }
}