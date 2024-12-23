using System.Collections;
using System.Collections.Generic;
using Core;
using Infrastructure.States;
using Main;
using UnityEngine;
using Zenject;

public class FirstSceneInstaller : MonoInstaller{
   
        [SerializeField] private MonoBehaviour coroutineRunner; // GameObject с MonoBehaviour для ICoroutineRunner

    public override void InstallBindings() {
            Debug.Log("ALOOOOO");
        // Бинд ICoroutineRunner
        Container.Bind<ICoroutineRunner>()
            .To<CoroutineRunnerWrapper>()
            .AsSingle()
            .WithArguments(coroutineRunner);

        // Бинд SceneLoader
        Container.Bind<SceneLoader>()
            .AsSingle();

        // Бинд GameStateMachine
        Container.Bind<GameStateMachine>()
            .AsSingle();

        // Бинд GameInit
        Container.Bind<GameInit>()
            .AsSingle();
    }
}
