using System;
using UnityEngine;

namespace Player.Player_lean
{
    [Serializable]
    public struct Lean
    {
        public Vector3 cameraOffset;
        public Vector3 cameraRotation;

        public static Lean ZeroLean => new Lean();

        public static Lean Lerp(Lean a, Lean b, float t)
        {
            Lean newLean = new()
            {
                cameraOffset = Vector3.Lerp(a.cameraOffset, b.cameraOffset, t),
                cameraRotation = Vector3.Lerp(a.cameraRotation, b.cameraRotation, t)
            };

            return newLean;
        }
    }
}