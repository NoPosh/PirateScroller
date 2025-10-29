using System.Collections;
using System.Collections.Generic;
using TestGame.Core.EventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TestGame.Core.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        private bool _isPaused = false;
        public bool IsPaused => _isPaused;

        private void Awake()
        {          
            if (Instance != null && Instance != this )
            {
                Destroy(Instance);
            }
            Instance = this;
            Time.timeScale = 1.0f;

            EventBus.EventBus.Subscribe<OnToggleGamePause>(ToggleGame);
        }

        private void OnDisable()
        {
            EventBus.EventBus.Unsubscribe<OnToggleGamePause>(ToggleGame);
        }

        private void ToggleGame()
        {
            if (_isPaused) ResumeGame();
            else PauseGame();
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            _isPaused = true;
            EventBus.EventBus.Raise(new OnGameStateChange(_isPaused));
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f; 
            _isPaused = false;
            EventBus.EventBus.Raise(new OnGameStateChange(_isPaused));
        }
    
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}