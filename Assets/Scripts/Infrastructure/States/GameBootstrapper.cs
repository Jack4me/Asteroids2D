using System;
using System.IO;
using System.Linq;
using Core;
using Main;
using Main.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameInit _gameInit;

        [Inject]
        public void Construct(GameInit gameInit)
        {
            _gameInit = gameInit;
        }

        private void Awake()
        {
            FIle();
            if (_gameInit == null)
            {
                Debug.Log("NULL _gameInit");
            }

            GameStateMachine stateMachine = _gameInit.StateMachine;
            if (stateMachine == null)
            {
                Debug.LogError("StateMachine is not initialized!");
                return;
            }

            stateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }

        public void FIle()
        {
            string directoryPath = @"D:\GIT\2DAsteroids\Assets\Scripts"; 
            var files = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);
            var classes = files.SelectMany(file => File.ReadLines(file)
                    .Where(line => line.TrimStart().StartsWith("class "))
                    .Select(line => Path.GetFileNameWithoutExtension(file) + ": " + line.Trim()))
                .ToList();
        
            string outputFilePath = @"D:\00_Main\Games\2.txt"; 

            try
            {
                File.WriteAllLines(outputFilePath, classes);
                Console.WriteLine($"Классы записаны в файл: {outputFilePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}