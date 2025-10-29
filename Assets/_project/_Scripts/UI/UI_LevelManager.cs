using System.Collections;
using System.Collections.Generic;
using TestGame.Core.EventBus;
using TestGame.UI;
using UnityEngine;

public class UI_LevelManager : MonoBehaviour
{
    [SerializeField] private UI_ShowAnimation PauseMenu;  //Анимированная панель
    [SerializeField] private UI_ShowAnimation GameOverMenu;

    private void Awake()
    {
        EventBus.Subscribe<OnGameStateChange>(TogglePauseMenu);
        EventBus.Subscribe<OnGameOver>(ShowGameOverPanel);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGameStateChange>(TogglePauseMenu);
        EventBus.Unsubscribe<OnGameOver>(ShowGameOverPanel);
    }
    public void TogglePauseMenu(OnGameStateChange e)
    {
        PauseMenu.TogglePanel(e.IsPaused);
    }

    private void ShowGameOverPanel()
    {
        GameOverMenu.TogglePanel(true);
    }
}
