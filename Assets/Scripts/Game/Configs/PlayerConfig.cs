using UnityEngine;

namespace Game.Configs
{
    
    [System.Serializable]
    public class PlayerConfig : MonoBehaviour
    {
        public int health;
        public float speed;
        public WeaponConfig weapon;
    }
    
}
