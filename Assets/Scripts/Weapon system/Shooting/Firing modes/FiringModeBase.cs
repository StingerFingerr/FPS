using System;
using UnityEngine;

namespace Shooting.Firing_modes
{
    public abstract class FiringModeBase: MonoBehaviour
    {
        public abstract void StartFiring(Action shot);
        public abstract void FinishFiring();
    }
}