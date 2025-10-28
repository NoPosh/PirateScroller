using System.Collections;
using System.Collections.Generic;
using TestGame.Core.EventBus;
using TMPro;
using UnityEngine;

namespace TestGame.UI
{
    public class UI_BombCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bombText;

        private void Awake()
        {
            EventBus.Subscribe<BombAmountChange>(UpdateUI);
        }
        private void OnDisable()
        {
            EventBus.Unsubscribe<BombAmountChange>(UpdateUI);
        }

        private void UpdateUI(BombAmountChange e)
        {
            _bombText.text = e.Amount.ToString();
        }
    }
}