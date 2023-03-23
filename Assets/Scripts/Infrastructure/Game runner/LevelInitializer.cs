using System;
using System.Collections.Generic;
using Game_state_machine;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game_runner
{
    public class LevelInitializer: MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private GameFactory _gameFactory;
        private IProgressService _progressService;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(
            IGameStateMachine gameStateMachine,
            GameFactory gameFactory,
            DiContainer diContainer,
            IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _diContainer = diContainer;
            _progressService = progressService;
        }


        private void Start()
        {
            _gameFactory.CreatePlayer();
            _gameFactory.CreateCrosshairGameUI();
            
            LoadProgress(); // ??
        }

        private void LoadProgress()
        {
            if(_progressService.Progress is not null)
                _diContainer.Resolve<List<IProgressReader>>().ForEach(r => r.Load(_progressService.Progress));
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}