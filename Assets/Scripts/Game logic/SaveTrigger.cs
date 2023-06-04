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
            _progressService.InformProgressWritersForSave(_diContainer);
            _progressService.Save();
            Debug.Log("Game Saved on save trigger");
        }
    }
}