using Core.Intrerfaces;
using Game.Controllers;
using Game.Handlers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private IControlStrategy controlStrategy;
        private PlayerController playerController;
        private bool isGameRunning;
        private int score;

        [Inject]
        public void Construct(IControlStrategy controlStrategy, PlayerController playerController)
        {
            this.controlStrategy = controlStrategy;
            this.playerController = playerController;
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
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
            if (CheckGameOver())
            {
                EndGame();
            }

            UpdateScore();
        }

        private bool CheckGameOver()
        {
            return playerController.Health <= 0;
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
        }
    }
}