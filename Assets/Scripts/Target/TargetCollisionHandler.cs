using Manager;
using UnityEngine;

namespace Target
{
    /// <summary>
    /// Class handles collision between targets and bullets
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>
    public class TargetCollisionHandler : MonoBehaviour
    {
        /// <summary>
        /// Handles the collision with a bullet
        /// </summary>
        /// <param name="bulletCollision">Object that collided with the target</param>
        private void OnCollisionEnter(Collision bulletCollision)
        {
            if (!bulletCollision.gameObject.CompareTag("Bullet")) return;
            
            TargetSpawner.Instance.SpawnNextTarget(gameObject.transform.parent);
            LevelManager.Instance.UpdateScore();
            
            // Destroy bullet and target
            Destroy(bulletCollision.gameObject);
            Destroy(gameObject);
        }
    }
}