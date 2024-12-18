using System;
using System.Collections.Generic;
using Core;
using Core.Factory;
using Core.Intrerfaces;
using Core.StaticData;
using Infrastructure.Ref.Services;
using UnityEngine;

namespace Infrastructure.States {
    public class GameStateMachine {
        private readonly Transform _transform;
        private readonly Dictionary<Type, IExitableState> _state;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services)
        {
            _state = new Dictionary<Type, IExitableState>{
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader,
                    services.GetService<IGameFactory>(), services.GetService<IStaticDataService>(), services.GetService<ISpawnService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this),
                [typeof(GameLoopState)] = new GameLoopState(this)
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