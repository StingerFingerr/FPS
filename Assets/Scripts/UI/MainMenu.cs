using System;
using Game_state_machine;
using UI.Warning_panel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu: MonoBehaviour
    {
        public Button playButton;
        public Button continueButton;

        private IGameStateMachine _gameStateMachine;
        private IWarningPanel _warningPanel;

        [Inject]
        private void Construct(
            IGameStateMachine gameStateMachine,
            IWarningPanel warningPanel
            )
        {
            _gameStateMachine = gameStateMachine;
            _warningPanel = warningPanel;
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(StartNewGame);
            continueButton.onClick.AddListener(LoadSave);
        }

        private void StartNewGame()
        {
            
        }

        private void LoadSave()
        {
            _warningPanel.Show(String.Empty);
        }
    }
}