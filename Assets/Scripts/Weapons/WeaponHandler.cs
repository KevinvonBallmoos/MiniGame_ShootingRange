using System.Collections;
using InfoLabel;
using Manager;
using Player.Interact;
using Settings;
using TimeEvents;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// WeaponHandler handles the weapon states and skins
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>  
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Material _goldMaterial;
        private GameObject _body;
        
        /// <summary>
        /// Start is called on the first frame
        /// </summary>
        private void Start()
        {
            if (!SettingsViewModel.IsEasterEggFound) return;
            
            _body = gameObject.transform.Find("Body").gameObject;
            var meshRenderer = _body.GetComponent<MeshRenderer>();
            meshRenderer.material = _goldMaterial;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            if (SettingsViewModel.IsEasterEggFound || !(gameObject.transform.localPosition.y < 6f)) return;
            
            SettingsViewModel.IsEasterEggFound = true;
            SettingsHandler.Instance.SaveSettings();
            InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter", "Congratulations you found the easter egg!\nYour weapons and bullets are now golden\nHave Fun :)");
            StartCoroutine(StartTimerCoroutine());
            
            gameObject.transform.localPosition = gameObject.name.Equals("SphereGun")? new Vector3(22.5f, 6.7f, 2.6f) : new Vector3(22.6f, 6.7f, 0.6f);
            PlayerObjectInteractor.Instance.ApplyGoldMaterial();
        }
        
        /// <summary>
        /// Starts a 4-second timer before the round
        /// </summary>
        /// <returns>Coroutine</returns>
        private IEnumerator StartTimerCoroutine()
        {
            yield return StartCoroutine(TimeEventHandler.Instance.StartTimer(4f));
            InfoLabelHandler.Instance.SetInfoLabelText("CanvasCenter", "");
        }
    }
}
