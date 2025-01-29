using Core.Services.Randomizer;
using UnityEngine;

namespace Main {
    internal class RandomService : IRandomService {
        public int Next(int min, int max){
            return Random.Range(min, max);
        }
    }
}