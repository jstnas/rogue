using System.Collections.Generic;
using UnityEngine;

namespace Boids
{
    public class Boids : MonoBehaviour
    {
        private const float HalfWidth = 6.4f;
        private const float HalfHeight = 6.4f;
        [SerializeField] private GameObject boidPrefab;
        [SerializeField] private int spawnAmount;
        [SerializeField] private int maxBoids;
        [SerializeField] private float spawnRate;
        private readonly List<GameObject> _boids = new List<GameObject>();
        private float _timeSinceSpawn;

        private void Update()
        {
            SpawnBoids();
            // delete offscreen boids
            for (var b = 0; b < _boids.Count; b++)
            {
                var boid = _boids[b];
                if (_boids.Count > maxBoids || IsOffscreen(boid.transform.position))
                {
                    Destroy(boid);
                    _boids.RemoveAt(b);
                    b--;
                }
            }
            print(_boids.Count);
        }

        private void SpawnBoids()
        {
            // increment timer if not enough time has passed
            if (_timeSinceSpawn < spawnRate)
            {
                _timeSinceSpawn += Time.deltaTime;
                return;
            }
            // spawn new boids otherwise
            _timeSinceSpawn = 0;
            var t = transform;
            for (var b = 0; b < spawnAmount; b++)
            {
                var newBoid = Instantiate(boidPrefab, t.position, Quaternion.identity, null);
                _boids.Add(newBoid);
            }
        }

        private bool IsOffscreen(Vector3 boid)
        {
            return boid.x < -HalfWidth
                   || boid.x > HalfWidth
                   || boid.y < -HalfHeight
                   || boid.y > HalfHeight;
        }
    }
}