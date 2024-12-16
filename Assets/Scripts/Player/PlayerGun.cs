using Difficulty;
using Manager;
using Settings;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// This class handles the creating of bullets and shooting them.
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">11.11.2024</para>
    public class PlayerGun : MonoBehaviour
    {
        // Bullet prefabs
        [SerializeField] private GameObject _cubeBulletPrefab;
        [SerializeField] private GameObject _sphereBulletPrefab;
        // Gold material
        [SerializeField] private Material _goldMaterial;
        
        private GameObject _equippedGun;
        private Transform _bulletSpawnPoint;
        
        private const float Velocity = 33f;
        private bool _isWeaponEquipped;
        private bool _isShootingAllowed;
        public bool IsBulletCountActive { get; set; }
        public int BulletsShot { get; set; }

        // Instance
        public static PlayerGun Instance;
        
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

        /// <summary>
        /// Gets the weapon after the user equipped one
        /// </summary>
        /// <param name="weapon">The equipped weapon</param>
        public void GetWeapon(GameObject weapon)
        {
            _equippedGun = weapon;
            _bulletSpawnPoint = _equippedGun.transform.GetChild(2);
            _isWeaponEquipped = true;
            _isShootingAllowed = false;
        }

        /// <summary>
        /// Update is called once per frame
        /// Gets the gun object
        /// </summary>
        private void Update()
        {
            if (!_isWeaponEquipped) return;

            // Prevents the weapon from shooting after it was equipped
            if (!_isShootingAllowed)
            {
                _isShootingAllowed = true;
                return;
            }

            if (!Input.GetMouseButtonDown(0)) return;
            
            switch (_equippedGun.name)
            {
                case "CubeGun":
                    Shoot(_cubeBulletPrefab);
                    break;
                case "SphereGun":
                    Shoot(_sphereBulletPrefab);
                    break;
            }
        }

        /// <summary>
        /// Instantiates a bullet prefab and shoots it
        /// </summary>
        /// <param name="bulletPrefab">Prefab for the bullet</param>
        private void Shoot(GameObject bulletPrefab)
        {
            GameObject newBullet = Instantiate(bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody prefabRigidbody = newBullet.GetComponentInChildren<Rigidbody>();
            AudioSource audioSource = newBullet.GetComponent<AudioSource>();
            
            if (SettingsViewModel.IsEasterEggFound)
            {
                var meshRenderer = newBullet.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = _goldMaterial;
            }

            var prefabTransform = _bulletSpawnPoint.transform;
            var povCamera = gameObject.GetComponentInChildren<Camera>();
            
            // Raycast to the center of the camera
            Ray ray = povCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // If Ray hits a target -> target point is position of object
            // else -> target point is an unknown point 100f away
            Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit, 100f) ? hit.point : ray.GetPoint(100f);

            // Get Vector without length
            Vector3 direction = (targetPoint - prefabTransform.position).normalized;
            prefabRigidbody.AddForce(direction * Velocity, ForceMode.Impulse);

            if (LevelManager.Instance.Difficulty == EDifficulty.Mid && IsBulletCountActive) 
            {
                BulletsShot++;
                LevelManager.Instance.CheckBulletCount(BulletsShot);
            }

            if (SettingsViewModel.Settings.BulletSoundEnabled)
            {
                audioSource.Play();
            }

            Destroy(newBullet, 2f);
        }
    }
}
