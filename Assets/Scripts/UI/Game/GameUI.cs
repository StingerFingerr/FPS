using Game_state_machine;
using Player.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Zenject.Asteroids;

public class GameUI: MonoBehaviour
{
    [SerializeField] private BaseWindow gameMenu;
    [SerializeField] private BaseWindow settings;
    private BaseWindow _inventory;

    private PlayerInputs _inputs;
    private PlayerInput _playerInput;

    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(PlayerInputs playerInputs, UIInventory inventory, IGameStateMachine gameStateMachine)
    {
        _inventory = inventory;
        _inputs = playerInputs;
        _playerInput = _inputs.GetComponent<PlayerInput>();
        _gameStateMachine = gameStateMachine;
    }

    private void OnEnable()
    {
        _inputs.onTab += HandleTab;
        _inputs.onEscape += HandleEscape;
    }

    private void OnDisable()
    {
        _inputs.onTab -= HandleTab;
        _inputs.onEscape -= HandleEscape;
    }

    public void CloseGameMenu()
    {
        gameMenu.Close();
        SetGameActionMap();
    }

    public void ExitToMainMenu()
    {
        gameMenu.Close();
        _gameStateMachine.Enter<LoadMainMenuState>();
    }

    private void HandleEscape()
    {
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
    }

    private void SetGameActionMap()
    {
        _playerInput.SwitchCurrentActionMap("Game");
        Cursor.lockState = CursorLockMode.Locked;
    }
}