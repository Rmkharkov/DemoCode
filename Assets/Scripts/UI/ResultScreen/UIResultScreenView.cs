using System;
using Regulators;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.ResultScreen
{
    public interface IUIResultScreenView
    {
        event Action OnResultPanelClosed;
    }
    public class UIResultScreenView : BaseView<UIResultScreenModel, UIResultScreenController>, IUIResultScreenView
    {
        public static IUIResultScreenView Instance => _instance;
        private static UIResultScreenView _instance;
        
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private TextMeshProUGUI scoreTimeText;
        [SerializeField] private Button restartButton;

        public event Action OnResultPanelClosed;
    
        
        private float _matchStartTime;
        private Action _onResultPanelClosed1;

        public override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartPressed);
            GameplayTurnRegulator.Instance.OnCleanMatchEvent += ShowResult;
            GameplayTurnRegulator.Instance.OnStartMatchEvent += SaveStartTime;
        }

        private void RestartPressed()
        {
            resultPanel.SetActive(false);
            OnResultPanelClosed?.Invoke();
        }

        private void ShowResult()
        {
            resultPanel.SetActive(true);
            var matchTime = Time.time - _matchStartTime;
            scoreTimeText.text = Controller.ParseTimer(matchTime);
        }

        private void SaveStartTime()
        {
            _matchStartTime = Time.time;
        }
    }
}