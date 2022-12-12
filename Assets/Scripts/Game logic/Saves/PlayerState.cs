using System;
using UnityEngine;

namespace Game_logic
{
    [Serializable]
    public class PlayerState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Quaternion cameraRotation;
    }
}