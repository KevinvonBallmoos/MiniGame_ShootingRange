using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Rotates an Element
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">08.11.2024</para>
    public class ObjectRotator : MonoBehaviour
    {
        // X Rotation
        [SerializeField] private float xRotation;
        // Y Rotation
        [SerializeField] private float yRotation;
        // Z Rotation
        [SerializeField] private float zRotation;
        
        /// <summary>
        /// In each frame the object is rotated
        /// </summary>
        private void Update()
        {
            transform.Rotate(xRotation, yRotation, zRotation, Space.Self);
        }
    }
}
