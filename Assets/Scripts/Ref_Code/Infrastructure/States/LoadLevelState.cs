using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States {
    public class LoadLevelState : ILoadLvlState<string> {
        private const string INITIAL_POINT = "InitialPoint";
        private const string ENEMYSPAWNER = "EnemySpawner";
        private readonly IGameFactory _gameFactory;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            IGameFactory gameFactory,
            IStaticDataService staticDataService){
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        public void Enter(string sceneName){
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit(){
        }

        private void OnLoaded(){
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.EnterGeneric<GameLoopState>();
        }

        private void InitGameWorld(){
            InitSpawners();
            _gameFactory.CreateUnit();
            // GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(INITIAL_POINT));
        }

        private void InitSpawners(){
            // foreach (GameObject spawnerObj in GameObject.FindGameObjectsWithTag(ENEMYSPAWNER)){
            //     var spawner = spawnerObj.GetComponent<EnemySpawner>();
            //     _gameFactory.Register(spawner);
            // }
            var sceneNameKey = SceneManager.GetActiveScene().name;
        }

        private void InformProgressReaders(){
        }

        private void InitHud(GameObject hero){
            var hud = _gameFactory.CreateHud();
        }
    }
}