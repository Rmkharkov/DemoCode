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
        UnityEvent ResultPanelClosed { get; }
    }
    public class UIResultScreenView : BaseView<UIResultScreenModel, UIResultScreenController>, IUIResultScreenView
    {
        public static IUIResultScreenView Instance => _instance;
        private static UIResultScreenView _instance;
        
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private TextMeshProUGUI scoreTimeText;
        [SerializeField] private Button restartButton;

        private readonly UnityEvent _resultPanelClosed = new UnityEvent();
        public UnityEvent ResultPanelClosed => _resultPanelClosed;
    
        
        private float _matchStartTime;

        public override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartPressed);
            GameplayTurnRegulator.Instance.CleanMatchEvent.AddListener(ShowResult);
            GameplayTurnRegulator.Instance.StartMatchEvent.AddListener(SaveStartTime);
        }

        private void RestartPressed()
        {
            resultPanel.SetActive(false);
            _resultPanelClosed.Invoke();
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