using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    /// <summary>
    /// Background music handler class
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">30.11.2024</para>
    public class BackGroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgmSound;
        
        /// <summary>
        /// Toggles the background music
        /// </summary>
        public void OnToggleSound()
        {
            var toggle = gameObject.GetComponent<Toggle>();
            if (toggle.isOn)
            {
                _bgmSound.Play();
            }
            else
            {
                _bgmSound.Stop();
            }
        }
    }
}
