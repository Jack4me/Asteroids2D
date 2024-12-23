using System;
using System.Collections.Generic;
using Core;
using Core.Factory;
using Core.Intrerfaces;
using Core.StaticData;
using Infrastructure.Ref.Services;
using Main.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.States {
    public class GameStateMachine {
        private readonly Transform _transform;
        private  Dictionary<Type, IExitableState> _state;
        private IExitableState _activeState;


        [Inject] private BootStrapState bootStrapState;
        [Inject]  private LoadLevelState loadLevelState;
        [Inject]  private LoadProgressState loadProgressState;
        [Inject]  private GameLoopState gameLoopState;
        
        
        [Inject]
        public void Initialize()
        {
            Debug.Log("GameStateMachine");
            _state = new Dictionary<Type, IExitableState>{
                [typeof(BootStrapState)] = bootStrapState,
                [typeof(LoadLevelState)] = loadLevelState,
                [typeof(LoadProgressState)] = loadProgressState,
                [typeof(GameLoopState)] = gameLoopState
            };
        }

        public void EnterGeneric<TState>() where TState : class, IState{
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void EnterGeneric<TState, TLoadScene>(TLoadScene loadScene)
            where TState : class, ILoadLvlState<TLoadScene>{
            var state = ChangeState<TState>();
            state.Enter(loadScene);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState{
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState{
            return _state[typeof(TState)] as TState;
        }
    }
}