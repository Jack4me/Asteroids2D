using CodeBase.Infrastructure.Services.Randomizer;
using Core;
using Core.Analytics;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Intrerfaces.Services.Input;
using Core.Models;
using Core.Services;
using Core.Services.Randomizer;
using Core.StaticData;
using Game;
using Infrastructure.Ref.Services;
using Infrastructure.States;
using UnityEngine;

namespace Main.States
{
    public class BootStrapState : IState
    {
        private const string INITIAL_LEVEL = "Initial";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly Transform _transform;

        public BootStrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader
           )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            if (GameAnalytics.gameAnalytics == null)
            {
                Debug.LogError("GameAnalytics.gameAnalytics is null.");
            }
            else
            {
                GameAnalytics.gameAnalytics.InterstitialAd();
            }
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

        
    }
}