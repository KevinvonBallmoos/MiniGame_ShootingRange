using UnityEngine;

namespace Target
{
    /// <summary>
    /// Class handles the movement of the targets
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">22.11.2024</para>
    public class TargetMovement : MonoBehaviour
    {
        public float speed = 1.8f;
        private const float Epsilon = 0.1f;
        
        private Vector3 _direction;
        private Vector3 _start;
        private Vector3 _destination;

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            transform.Translate(_direction * (speed * Time.deltaTime));
            if (Vector3.Distance(transform.position, _destination) <= Epsilon)
            {
                if (_direction == Vector3.left)
                {
                    SetDirection(Vector3.right, _destination, _start);
                }
                else
                {
                    SetDirection(Vector3.left, _destination, _start);
                }
            }
        }

        /// <summary>
        /// Sets the direction of the target
        /// Either left or right
        /// </summary>
        /// <param name="direction">Direction which the target should move</param>
        /// <param name="start">Spawn position of the target</param>
        /// <param name="destination">Position where the target should move in the opposite direction</param>
        public void SetDirection(Vector3 direction, Vector3 start, Vector3 destination)
        {
            _direction = direction;
            _start = start;
            _destination = destination;
        }
    }
}