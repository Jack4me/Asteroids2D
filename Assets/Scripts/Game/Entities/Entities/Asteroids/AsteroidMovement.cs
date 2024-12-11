using System;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Entities.Entities.Asteroids {
    public class AsteroidMovement : MonoBehaviour {
        private float _speed;
        private Vector2 direction;
        private Enemy _enemy;

      
        private void Start()
        {
            direction = Random.insideUnitCircle.normalized; 
            
        }

        private void Update() {
            transform.position += (Vector3)(direction * _speed * Time.deltaTime);
        }

       public void SetSpeed( float speed)
       {
           
           _speed = speed;
           Debug.Log(_speed + "_speed");
       }
    }
}