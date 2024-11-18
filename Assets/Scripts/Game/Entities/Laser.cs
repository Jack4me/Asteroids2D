using Game;
using UnityEngine;

public class Laser : MonoBehaviour {
    public float Speed = 20f;
    public float LifeTime = 3f;

    private void Start() {
        Destroy(gameObject, LifeTime);
    }

    private void Update() {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IDamageable>(out var damageable)) {
            damageable.TakeDamage(1); 
        }

    }
}