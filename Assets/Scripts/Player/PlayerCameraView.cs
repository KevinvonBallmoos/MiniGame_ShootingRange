using UnityEngine;

namespace Player
{
    /// <summary>
    /// Applies the pov mode for the player
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">09.11.2024</para>
    public class PlayerCameraView : MonoBehaviour
    {
        private const float MouseSensitivity = 125f;
        
        // Rotation of the camera
        private float _cameraXRotation;
        private float _cameraYRotation;

        // X Rotation to limit pov view
        private const float MinXRotation = -60f;
        private const float MaxXRotation = 35f;
        
        // Y Rotation to limit pov view
        private const float MinYRotation = -100f;
        private const float MaxYRotation = 110f;
        
        /// <summary>
        /// Start is called on the first frame
        /// Locks the cursor to the center of the view
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            var mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            var mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
            
            // X Rotation (up, down)
            _cameraXRotation -= mouseInputY;
            _cameraXRotation = Mathf.Clamp(_cameraXRotation, MinXRotation, MaxXRotation);
            
            // Y rotation (left, right)
            _cameraYRotation += mouseInputX;
            _cameraYRotation = Mathf.Clamp(_cameraYRotation, MinYRotation, MaxYRotation);
            
            // Camera
            transform.localRotation = Quaternion.Euler(_cameraXRotation,  -90f, 0f);
            // Camera parent
            transform.parent.localRotation = Quaternion.Euler(0f, _cameraYRotation, 0f);
        }
    }
}
