using Core.Intrerfaces;
using Game.Controllers;
using Game.Handlers;
using UnityEngine;
using Zenject;

namespace Game {
    public class GameManager : MonoBehaviour
    {
        private IControlStrategy controlStrategy;
        private ShipController shipController;
        private bool isGameRunning;
        private int score;

        [Inject]
        public void Construct(IControlStrategy controlStrategy, ShipController shipController)
        {
            this.controlStrategy = controlStrategy;
            this.shipController = shipController;
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Настроим начальные условия
            isGameRunning = true;
            score = 0;
            SetupControl();
        }

        private void Update()
        {
            if (isGameRunning)
            {
                HandleGameLogic();
            }
        }

        private void HandleGameLogic()
        {
            // Здесь могут быть проверки на состояние игры, например, если игрок проиграл, то можно переключить состояние на GameOver
            if (CheckGameOver())
            {
                EndGame();
            }

            // Логика игры, например, обновление счёта, проверка столкновений и т.д.
            UpdateScore();
        }

        private bool CheckGameOver()
        {
            // Пример проверки, если здоровье корабля или другие параметры упали до 0
            return shipController.Health <= 0;
        }

        private void EndGame()
        {
            isGameRunning = false;
            Debug.Log("Game Over! Final Score: " + score);
            ShowGameOverScreen();
        }

        private void UpdateScore()
        {
            score += 10;
        }

        private void ShowGameOverScreen()
        {
        }

        private void SetupControl()
        {
            // Настроим контроллер на определённую платформу
            // Например, если у нас мобильная версия, можем инжектировать MobileController
            // Иначе — KeyboardController
        }
    }

}