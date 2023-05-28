using Game_state_machine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameUI: MonoBehaviour
{
    [SerializeField] private BaseWindow gameMenu;
    [SerializeField] private BaseWindow settings;
    [SerializeField] private BaseWindow gameOver;
    private BaseWindow _inventory;

    private PlayerInputs _inputs;
    private PlayerInput _playerInput;
    private IGameStateMachine _gameStateMachine;
    private PlayerHealth _playerHealth;
    private WorldBlur _blur;

    [Inject]
    private void Construct(
        PlayerInputs playerInputs, 
        UIInventory inventory,
        IGameStateMachine gameStateMachine,
        PlayerHealth playerHealth,
        WorldBlur blur)
    {
        _inventory = inventory;
        _inputs = playerInputs;
        _playerInput = _inputs.GetComponent<PlayerInput>();
        _gameStateMachine = gameStateMachine;
        _playerHealth = playerHealth;
        _blur = blur;
    }

    private void OnEnable()
    {
        _inputs.onTab += HandleTab;
        _inputs.onEscape += HandleEscape;
        _playerHealth.onPlayerDied += ShowGameOverWindow;
    }

    private void OnDisable()
    {
        _inputs.onTab -= HandleTab;
        _inputs.onEscape -= HandleEscape;
        _playerHealth.onPlayerDied += ShowGameOverWindow;
    }

    public void CloseGameMenu()
    {
        gameMenu.Close();
        SetGameActionMap();
    }

    private void ShowGameOverWindow()
    {
        gameOver.Open();
        SetWindowActionMap();
    }

    public void ExitToMainMenu()
    {
        gameMenu.Close();
        _gameStateMachine.Enter<LoadMainMenuState>();
    }

    public void Exit()
    {
        _gameStateMachine.Enter<ExitState>();
    }

    public void Retry()
    {
        gameMenu.Close();
        _gameStateMachine.Enter<LoadLevelState>();
    }

    private void HandleEscape()
    {
        if (gameOver.IsOpened)
            return;
        if(_inventory.IsOpened)
        {
            _inventory.Close();
            SetGameActionMap();
            return;
        }

        if (settings.IsOpened)
        {
            settings.Close();
            return;
        }

        if (gameMenu.IsOpened)
        {
            CloseGameMenu();
        }
        else
        {
            gameMenu.Open();
            SetWindowActionMap();
        }
        
        
    }

    private void HandleTab()
    {
        if (gameOver.IsOpened)
            return;
        if(gameMenu.IsOpened)
            return;

        if(_inventory.IsOpened)
        {
            _inventory.Close();
            SetGameActionMap();
        }
        else
        {
            _inventory.Open();
            SetWindowActionMap();
        }
    }

    private void SetWindowActionMap()
    {
        _playerInput.SwitchCurrentActionMap("Window");
        Cursor.lockState = CursorLockMode.None;
        _blur.Show();
    }

    private void SetGameActionMap()
    {
        _playerInput.SwitchCurrentActionMap("Game");
        Cursor.lockState = CursorLockMode.Locked;
        _blur.Hide();
    }
}