using System;
using ButtonScripts;
using InfoLabel;
using Manager;
using Settings;
using UnityEngine;

namespace Player.Interact
{
    /// <summary>
    /// This class helps the player to interact with game objects
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">10.11.2024</para>
    public class PlayerObjectInteractor : MonoBehaviour
    {
        // Gun prefabs
        [SerializeField] private GameObject _cubeGunPrefab;
        [SerializeField] private GameObject _sphereGunPrefab;
        // Counter weapons
        [SerializeField] private GameObject[] _weapons;
        // Gold material
        [SerializeField] private Material _goldMaterial;
        
        [NonSerialized] private bool IsWeaponEquipped;
        
        private GameObject _cameraObject;
        private Camera _camera;
        
        private GameObject _equippedWeapon;
        private GameObject _selectedGunFromCounter;
        
        private const string CubeGunName = "CubeGun";
        private const string SphereGunName = "SphereGun";
        
        // Instance
        public static PlayerObjectInteractor Instance;

        #region Instance
        
        /// <summary>
        /// Start is called on the first frame
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        #endregion
        
        #region Start and Update
        
        /// <summary>
        /// Start is called on the first frame
        /// Initializes the camera object and component
        /// </summary>
        private void Start()
        {
            _cameraObject = gameObject.transform.GetChild(0).gameObject;
            _camera = GetComponentInChildren<Camera>();
        }

        /// <summary>
        /// Update is called once per frame
        /// Checks if a game object was clicked
        /// </summary>
        private void Update()
        {
            // Check if mouse button left is clicked
            if (Input.GetMouseButtonDown(0))
            {
                CastRay(false);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                CastRay(true);
            }
        }
        
        #endregion
        
        #region Cast Ray

        /// <summary>
        /// Casts a ray to hit objects
        /// </summary>
        /// <param name="isKeyPressed">Determines if a key was pressed or the mouse</param>
        private void CastRay(bool isKeyPressed)
        {
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

            if (!Physics.Raycast(ray, out var hit, 5f)) return;
            
            if (isKeyPressed)
            {
                CheckHitWeapon(hit.collider.gameObject);
            }
            else
            {
                CheckHitButton(hit.collider.gameObject);
            }
        }

        /// <summary>
        /// Checks which object has been hit by the raycast
        /// </summary>
        /// <param name="rayCastHitObject">The object hit by the raycast</param>
        private static void CheckHitButton(GameObject rayCastHitObject)
        {
            switch (rayCastHitObject.name)
            {
                case "QuitButton":
                    rayCastHitObject.GetComponent<QuitButton>()?.QuitGame_Click();
                    break;
                case "StartButton":
                    rayCastHitObject.GetComponent<StartButton>()?.StartGame_Click();
                    break;
                case "ButtonEasy":
                    rayCastHitObject.GetComponent<DifficultyButton>()?.SetDifficulty_Click();
                    break;
                case "ButtonMid":
                    rayCastHitObject.GetComponent<DifficultyButton>()?.SetDifficulty_Click();
                    break;
            }
        }

        /// <summary>
        /// Checks which object has been hit by the raycast
        /// </summary>
        /// <param name="rayCastHitObject">The object hit by the raycast</param>
        private void CheckHitWeapon(GameObject rayCastHitObject)
        {
            switch (rayCastHitObject.name)
            {
                case CubeGunName:
                    PickUpWeapon(rayCastHitObject);
                    break;
                case SphereGunName:
                    PickUpWeapon(rayCastHitObject);
                    break;
            }
        }
        
        #endregion
        
        #region Pick up weapon
        
        /// <summary>
        /// Picks up the weapon
        /// if a weapon is already equipped, it swaps the weapons
        /// </summary>
        /// <param name="weapon">The weapon object selected</param>
        private void PickUpWeapon(GameObject weapon)
        {
            if (IsWeaponEquipped)
            {
                Destroy(_equippedWeapon);
                _selectedGunFromCounter.SetActive(true);
            }
            else
            {
                UIManager.Instance.PlayAnimation(1, "GoUp");
                InfoLabelHandler.Instance.SetInfoLabelText("Back", "Either select a different weapon or\n start the game");
            }

            // Instantiate a weapon prefab
            var weaponPicked = weapon.name switch
            {
                CubeGunName => InstantiateWeapon(_cubeGunPrefab),
                SphereGunName => InstantiateWeapon(_sphereGunPrefab),
                _ => null
            };

            PlayerGun.Instance.GetWeapon(weaponPicked);

            _equippedWeapon = weaponPicked;
            IsWeaponEquipped = true;
            
            // Cache selected gun
            _selectedGunFromCounter = weapon;
            _selectedGunFromCounter.SetActive(false);
        }

        /// <summary>
        /// Instantiates a weapon and adds it to the camera object as a child
        /// </summary>
        /// <param name="weaponPrefab">The weapon prefab to instantiate</param>
        /// <returns>The instantiated game object</returns>
        private GameObject InstantiateWeapon(GameObject weaponPrefab)
        {
            var weaponPicked = Instantiate(weaponPrefab, _cameraObject.transform.position, _cameraObject.transform.rotation, _cameraObject.transform);

            switch (weaponPrefab.name)
            {
                case SphereGunName:
                    weaponPicked.transform.localPosition = new Vector3(1f, -0.8f, 1.5f);
                    weaponPicked.transform.localRotation = Quaternion.Euler(145f, 90f, 90f);
                    break;
                case CubeGunName:
                    weaponPicked.transform.localPosition = new Vector3(1.2f, -0.6f, 1.25f);
                    weaponPicked.transform.localRotation = Quaternion.Euler(-30f, 90f, 0f);
                    break;
            }
            
            weaponPicked.name = weaponPicked.name.Replace("(Clone)", "");

            if (SettingsViewModel.IsEasterEggFound)
            {
                var body = weaponPicked.transform.Find("Body").gameObject;
                var meshRenderer = body.GetComponent<MeshRenderer>();
                meshRenderer.material = _goldMaterial;
            }

            return weaponPicked;
        }
        
        #endregion
        
        #region Reset weapons

        /// <summary>
        /// Resets the weapon positions and rotations
        /// </summary>
        public void ResetWeapons()
        {
            _weapons[0].transform.localPosition = new Vector3(22.6f, 6.7f, 0.6f);
            _weapons[0].transform.localRotation = Quaternion.Euler(17f, 7.7f, 0f);
            _weapons[1].transform.localPosition = new Vector3(22.5f, 6.7f, 2.6f);
            _weapons[1].transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        
        #endregion
        
        #region Gold material

        /// <summary>
        /// Applies the gold material to the body of the counter weapons
        /// </summary>
        public void ApplyGoldMaterial()
        {
            foreach (var weapon in _weapons)
            {
                var body = weapon.transform.Find("Body").gameObject;
                var meshRenderer = body.GetComponent<MeshRenderer>();
                meshRenderer.material = _goldMaterial;
            }
        }
        
        #endregion
    }
}