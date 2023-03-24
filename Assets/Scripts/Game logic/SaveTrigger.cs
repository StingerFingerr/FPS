using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game_logic
{
    public class SaveTrigger: MonoBehaviour
    {
        private DiContainer _diContainer;
        private IProgressService _progressService;

        [Inject]
        private void Construct(DiContainer diContainer, IProgressService progressService)
        {
            _diContainer = diContainer;
            _progressService = progressService;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_progressService.Progress is null)
                _progressService.InitNewProgress();
            
            _diContainer.Resolve<List<IProgressWriter>>().ForEach(w => w.Save(_progressService.Progress));
            Debug.Log("Game Saved on save trigger");
            _progressService.Save();
        }
    }
}