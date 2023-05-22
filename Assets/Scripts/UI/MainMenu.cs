using System;
using Game_state_machine;
using UI.Warning_panel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu: MonoBehaviour
{
    public Button playButton;
    public Button continueButton;

    private IGameStateMachine _gameStateMachine;
    private IWarningPanel _warningPanel;
    private IProgressService _progressService;

    [Inject]
    private void Construct(
        IGameStateMachine gameStateMachine,
        IWarningPanel warningPanel,
        IProgressService progressService
    )
    {
        _gameStateMachine = gameStateMachine;
        _warningPanel = warningPanel;
        _progressService = progressService;
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(StartNewGame);
        continueButton.onClick.AddListener(LoadSave);
    }

    private void StartNewGame()
    {
        if(_progressService.SaveExists())
            _warningPanel.Show(String.Empty, ResetProgressAndStartNewGame);
        else
            ResetProgressAndStartNewGame();
    }

    private void ResetProgressAndStartNewGame()
    {
        _progressService.ResetProgress();
        StartLevelLoading();
    }

    private void LoadSave()
    {
        if(_progressService.Load())
            StartLevelLoading();
        else
            _warningPanel.Show(String.Empty);
    }

    private void StartLevelLoading() => 
        _gameStateMachine.Enter<LoadLevelState>();
}