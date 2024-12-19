using Core;
using Core.Factory;
using Core.Intrerfaces;
using Core.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Models;
using Main;

namespace Infrastructure.States {
    public class LoadLevelState : ILoadLvlState<string> {
        private const string INITIAL_POINT = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ISpawnService _spawnService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            IGameFactory gameFactory,
            IStaticDataService staticDataService, ISpawnService spawnService){
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _spawnService = spawnService;
        }

        public void Enter(string sceneName){
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit(){
        }

        private void OnLoaded(){
            InitGameWorld();
            
            _gameStateMachine.EnterGeneric<GameLoopState>();
        }

        private void InitGameWorld(){
            InitSpawners();
            _gameFactory.LoadConfigs();
            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(INITIAL_POINT));
           Transform obj = _gameFactory.CreatePoolParent();
          LaserViewModel laserViewModel =  hero.GetComponent<PlayerController>().LaserViewModel;
           InitHud(laserViewModel);
           InitEnemy(obj);
           _spawnService.SpawnAsteroid();

        }

        private void InitHud(LaserViewModel laserViewModel){
            var hud = _gameFactory.CreateHud(laserViewModel);
        }

        private void InitEnemy(Transform transform)
        {
            
        }

        private void InitSpawners(){
            

            // foreach (GameObject spawnerObj in GameObject.FindGameObjectsWithTag(ENEMYSPAWNER)){
            //     var spawner = spawnerObj.GetComponent<EnemySpawner>();
            //     _gameFactory.Register(spawner);
            // }
            var sceneNameKey = SceneManager.GetActiveScene().name;
        }
    }
}