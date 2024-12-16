using UnityEngine;

namespace Player.Hover.Base
{
    /// <summary>
    /// This base class handles the hover over game objects
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">10.11.2024</para>
    public abstract class PlayerHoverHandlerBase : MonoBehaviour
    {
        // Camera
        private Camera _camera;
        // The game object that was hovered over
        private GameObject _hoveredObject;
        
        #region Start and Update

        /// <summary>
        /// Start is called on the first frame
        /// </summary>
        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        /// <summary>
        /// Update is called every frame
        /// </summary>
        private void Update()
        {
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

            if (!Physics.Raycast(ray, out var hit, 5f)) return;
            
            var hitObject = hit.collider.gameObject;
            if (hitObject != _hoveredObject)
            {
                OnPointerEnter(hitObject);
            }
                
            if (_hoveredObject != null && _hoveredObject != hitObject)
            {
                OnPointerExit(_hoveredObject);
                _hoveredObject = null;
            }
                
            _hoveredObject = hitObject;
        }
        
        #endregion

        #region OnPointer Abstract Methods
        
        /// <summary>
        /// Mouse entered the object
        /// Updates the according label
        /// </summary>
        /// <param name="hitObject">The object that was hit</param>
        protected abstract void OnPointerEnter(GameObject hitObject);

        /// <summary>
        /// Mouse exited the object
        /// Updates the according label
        /// </summary>
        /// <param name="hitObject">The object that was hit</param>
        protected abstract void OnPointerExit(GameObject hitObject);

        #endregion
    }
}