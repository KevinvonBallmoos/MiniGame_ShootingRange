using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Target
{
    /// <summary>
    /// Class handles the spawning of targets
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">22.11.2024</para>
    public class TargetSpawner : MonoBehaviour
    {
        // SpawnPoints
        [SerializeField] private Transform[] _pointSpawnPoints;
        [SerializeField] private Transform[] _sideSpawnPoints;
        // Prefab
        [SerializeField] private GameObject _pointTargetPrefab;
        [SerializeField] private GameObject _movableTargetPrefab;
        // Instance
        public static TargetSpawner Instance;

        #region Awake

        /// <summary>
        /// Initializes the instance
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion

        #region Spawn Targets

        /// <summary>
        /// Spawn targets for the start of a new round
        /// </summary>
        public void SpawnStartTargets()
        {
            SpawnPointTarget(_pointSpawnPoints[Random.Range(0, _pointSpawnPoints.Length)]);
            SpawnPointTarget(_pointSpawnPoints[Random.Range(0, _pointSpawnPoints.Length)]);
            SpawnSideTarget();
        }

        /// <summary>
        /// Method to spawn the next target
        /// Decides randomly which kind of target appears next
        /// </summary>
        public void SpawnNextTarget(Transform parent)
        {
            var random = Random.Range(0, 2);
            switch (random)
            {
                case 0:
                    SpawnPointTarget(parent);
                    break;
                case 1:
                    if (_sideSpawnPoints[0].childCount == 0 && _sideSpawnPoints[2].childCount == 0 ||
                        _sideSpawnPoints[1].childCount == 0 && _sideSpawnPoints[3].childCount == 0)
                    {
                        SpawnSideTarget();
                    }
                    else
                    {
                        SpawnPointTarget(parent);
                    }

                    break;
            }
        }
        
        #endregion
        
        #region Spawn Point or Side Target

        /// <summary>
        /// Spawns a new point target at a random position
        /// </summary>
        private void SpawnPointTarget(Transform parent)
        {
            // Get random spawn position
            var rdmPos = Random.Range(0, _pointSpawnPoints.Length);

            while (_pointSpawnPoints[rdmPos].childCount > 0 && parent != _pointSpawnPoints[rdmPos])
            {
                rdmPos = Random.Range(0, _pointSpawnPoints.Length);
            }

            Instantiate(_pointTargetPrefab, _pointSpawnPoints[rdmPos].position, _pointSpawnPoints[rdmPos].rotation, _pointSpawnPoints[rdmPos]);
        }

        /// <summary>
        /// Spawns a target on the side
        /// </summary>
        private void SpawnSideTarget()
        {
            var indexes = GetSideSpawnPoint();
            var rdmPos = Random.Range(0, indexes.Count);
            
            var index = indexes[rdmPos];
            var target = Instantiate(_movableTargetPrefab, _sideSpawnPoints[index].position,
                _sideSpawnPoints[index].rotation, _sideSpawnPoints[index]);

            TargetMovement movement = target.GetComponent<TargetMovement>();
            switch (index)
            {
                // Movement left to right
                case 0 or 1:
                    var endPos = index == 0 ? 2 : 3;
                    movement.SetDirection(_sideSpawnPoints[index].position.z < 0 ? Vector3.right : Vector3.left,
                        _sideSpawnPoints[index].position, _sideSpawnPoints[endPos].position);
                    break;
                // Movement right to left
                case 2 or 3:
                    endPos = index == 2 ? 0 : 1;
                    movement.SetDirection(_sideSpawnPoints[index].position.z > 0 ? Vector3.left : Vector3.right,
                        _sideSpawnPoints[index].position, _sideSpawnPoints[endPos].position);
                    break;
            }
        }
        
        #endregion
        
        #region Helper

        /// <summary>
        /// Gets the free spawn points as an index list
        /// </summary>
        /// <returns>List with index integers</returns>
        private List<int> GetSideSpawnPoint()
        {
            List<int> indexes = new List<int>();
            if (_sideSpawnPoints[0].childCount == 0 && _sideSpawnPoints[2].childCount == 0)
            {
                indexes.AddRange(new List<int> { 0, 2 });
            }

            if (_sideSpawnPoints[1].childCount == 0 && _sideSpawnPoints[3].childCount == 0)
            {
                indexes.AddRange(new List<int> { 1, 3 });
            }

            return indexes;
        }

        /// <summary>
        /// Destroys all targets in every spawn point
        /// </summary>
        public void  ClearSpawnPoints()
        {
            foreach (Transform spawnPoint in _pointSpawnPoints.Concat(_sideSpawnPoints))
            {
                foreach (Transform t in spawnPoint)
                {
                    t.parent = null;
                    Destroy(t.gameObject);
                }
            }
        }
        
        #endregion
    }
}