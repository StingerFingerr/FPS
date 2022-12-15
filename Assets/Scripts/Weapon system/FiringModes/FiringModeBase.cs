using System;
using UnityEngine;

namespace Weapon.FiringModes
{
    public abstract class FiringModeBase: MonoBehaviour
    {
        public abstract void StartFiring(Action shot);
        public abstract void FinishFiring();
    }
}