using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game_logic
{
    public class SaveTrigger: MonoBehaviour
    {
        private ISceneProgressService _progressService;

        [Inject]
        private void Construct(ISceneProgressService progressService)
        {
            _progressService = progressService;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("game saved");
            _progressService.SaveGame();
        }
    }
}