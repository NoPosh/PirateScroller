using System.Collections;
using System.Collections.Generic;
using TestGame.Core.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame.UI
{
    public class UI_HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _firstHearth;
        [SerializeField] private Image _secindHearth;
        [SerializeField] private Image _thirdHearth;

        private void Awake()
        {
            EventBus.Subscribe<OnCharacterHealthChange>(UpdateUI);
        }
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnCharacterHealthChange>(UpdateUI);
        }
        private void UpdateUI(OnCharacterHealthChange e)
        {
            if (e.CurrentHealth < 3) _thirdHearth.gameObject.SetActive(false);

            if (e.CurrentHealth < 2) _secindHearth.gameObject.SetActive(false);

            if (e.CurrentHealth < 1) _firstHearth.gameObject.SetActive(false);
        }    
    }
}