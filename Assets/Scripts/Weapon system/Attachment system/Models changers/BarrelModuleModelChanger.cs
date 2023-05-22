using System;
using UnityEngine;

namespace Attachment_system
{
    public class BarrelModuleModelChanger: MonoBehaviour
    {
        [SerializeField] private GameObject silencer;
        [SerializeField] private GameObject compensator;

        [SerializeField] private AudioClip shotWithSilencer;
        [SerializeField] private AudioClip shotWithCompensator;
        
        private BarrelModuleType _type;
        
        public void SetModelFor(BarrelModuleType type)
        {
            ResetModel();
            _type = type;

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
            _type = BarrelModuleType.None;
            silencer.SetActive(false);
            compensator.SetActive(false);
        }

        public AudioClip OverrideShotSound(AudioClip defaultClip)
        {
            return _type switch
            {
                BarrelModuleType.Silencer => shotWithSilencer,
                BarrelModuleType.Сompensator => shotWithCompensator,
                _ => defaultClip
            };
        }
    }
}