using UnityEngine;

namespace Game.Entities.Entities.UFO
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifetime = 5f;
        private Vector2 direction;

        public void Initialize(Vector2 shootDirection)
        {
            direction = shootDirection.normalized;
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Bullet hit the player!");
                Destroy(gameObject);
            }
        }
    }
}