using Game_state_machine;
using UI.Warning_panel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu: MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private string failedToLoadSaveTerm;
    [SerializeField] private string progressWillBeLostTerm;

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
        exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(StartNewGame);
        continueButton.onClick.RemoveListener(LoadSave);
        exitButton.onClick.RemoveListener(Exit);
    }

    private void Exit() => 
        _gameStateMachine.Enter<ExitState>();

    private void StartNewGame()
    {
        if(_progressService.SaveExists())
            _warningPanel.Show(progressWillBeLostTerm, ResetProgressAndStartNewGame);
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
            _warningPanel.Show(failedToLoadSaveTerm);
    }

    private void StartLevelLoading() => 
        _gameStateMachine.Enter<LoadLevelState>();
}