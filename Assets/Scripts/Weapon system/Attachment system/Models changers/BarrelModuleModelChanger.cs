using UnityEngine;

namespace Attachment_system
{
    public class BarrelModuleModelChanger: MonoBehaviour
    {
        public GameObject silencer;
        public GameObject compensator;
        
        public void SetModelFor(BarrelModuleType type)
        {
            ResetModel();

            switch (type)
            {
                case BarrelModuleType.Silencer: silencer.SetActive(true);
                    break;
                case BarrelModuleType.Сompensator: compensator.SetActive(true);
                    break;
            }
        }

        public void ResetModel()
        {
            silencer.SetActive(false);
            compensator.SetActive(false);
        }
    }
}