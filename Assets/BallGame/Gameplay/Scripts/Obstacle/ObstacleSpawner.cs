using UnityEngine;
using System.Collections.Generic;

namespace BallGame.Gameplay.Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [Header("Spawn Zone")]
        public BoxCollider zone;

        [Header("Obstacle Settings")]
        [SerializeField]private GameObject obstaclePrefab;
        [SerializeField] private ObstacleManager obstacleManager;
        public int obstacleCount = 10;
        public float offsetY = 0f;
        public int maxAttempts = 30;

        private List<Bounds> spawnedBounds = new List<Bounds>();

        //Create obstacles
        private void Start()
        {
            SpawnAll();
        }
        /// <summary>
        /// Random spawn all obstacle 
        /// </summary>
        public void SpawnAll()
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                TrySpawnObstacle();
            }
        }

        //Find free position and spawn obstacle
        private void TrySpawnObstacle()
        {
            BoxCollider obstacleCol = obstaclePrefab.GetComponent<BoxCollider>();
            Vector3 size = obstacleCol.bounds.size;
            Vector3 extents = size * 0.5f;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                Vector3 pos = GetRandomPointInZone();

                pos.y = zone.bounds.center.y + offsetY;

                Bounds newBounds = new Bounds(
                    new Vector3(pos.x, 0, pos.z),
                    new Vector3(size.x, 0.1f, size.z)
                );

                if (IsPositionFree(newBounds))
                {
                    GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
                    spawnedBounds.Add(newBounds);
                    obstacleManager.Register(obj, obj.GetComponent<MeshRenderer>());
                    return;
                }
            }

            Debug.LogWarning("Failed to place obstacle: no free space.");
        }

        //Return random point in area
        private Vector3 GetRandomPointInZone()
        {
            Vector3 min = zone.bounds.min;
            Vector3 max = zone.bounds.max;

            return new Vector3(
                Random.Range(min.x, max.x),
                0,
                Random.Range(min.z, max.z)
            );
        }
        //Check position poin is free
        private bool IsPositionFree(Bounds newBounds)
        {
            foreach (Bounds b in spawnedBounds)
            {
                if (b.Intersects(newBounds))
                    return false;
            }
            return true;
        }
    }
}
