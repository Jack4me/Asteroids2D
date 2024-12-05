using System.ComponentModel;
using CodeBase.Infrastructure.AssetsManagement;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory {
    public class GameFactory : IGameFactory {
        private readonly IInstantiateProvider _instantiate;
        private readonly IRandomService _random;
        private readonly IStaticDataService _staticData;

        public GameFactory(IInstantiateProvider instantiate, IStaticDataService staticData, IRandomService random
        ){
            _instantiate = instantiate;
            _staticData = staticData;
            _random = random;
        }

        public GameObject HeroGameObject{ get; set; }

        public void CreateUnit(){
            _instantiate.Instantiate(_staticData.GetUnitConfig().Prefab);
        }

        public GameObject CreateHero(GameObject at){
            HeroGameObject = InstantiateRegister(AssetPath.HERO_PATH, at.transform.position);
            return HeroGameObject;
            
        }

        public GameObject CreateHud(){
            var hud = InstantiateRegister(AssetPath.HUD_PATH);
            return hud;
        }

        public void CleanUp(){
        }

        private GameObject InstantiateRegister(string path, Vector3 position){
            var gameObject = _instantiate.Instantiate(path, position);
            return gameObject;
        }

        private GameObject InstantiateRegister(string path){
            var gameObject = _instantiate.Instantiate(path);
            return gameObject;
        }
    }
}