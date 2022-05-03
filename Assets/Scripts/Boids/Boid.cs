using UnityEngine;

namespace Boids
{
    public class Boid : MonoBehaviour
    {
        [SerializeField] private Vector3 forward;
        [SerializeField] private float speed;

        private void Start()
        {
            var angle = Random.Range(-Mathf.PI, Mathf.PI);
            forward = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
        }

        private void Update()
        {
            // move
            transform.position += speed * Time.deltaTime * forward;
        }
    }
}