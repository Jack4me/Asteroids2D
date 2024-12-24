using Core;
using Firebase;
using Firebase.Analytics;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.States
{
    public class BootStrapState : IState
    {
        private const string INITIAL_LEVEL = "Initial";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly Transform _transform;

        public BootStrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;

            InitAnalytics();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL_LEVEL, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.EnterGeneric<LoadProgressState>();
        }

        public void InitAnalytics()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("Firebase initialized successfully.");

                    // Логирование тестового события
                    FirebaseAnalytics.LogEvent("test_event");
                    Debug.Log("Test event sent.");
                }
                else
                {
                    Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
                }
            });
        }
    }
}