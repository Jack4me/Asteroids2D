using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IPlayerStats 
    {
        public int health { get; set; }
        public float speed { get; set; }
        public string weaponName { get; set; }
    }
}