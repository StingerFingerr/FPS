using Game_logic;
using UnityEngine;

namespace Helpers
{
    public static class Vector3Extensions
    {
        public static Vector3 ToVector3(this Vec3 vec3) => 
            new Vector3(vec3.x, vec3.y, vec3.z);

        public static Vec3 ToVec3(this Vector3 vector3) => 
            new Vec3(vector3.x, vector3.y, vector3.z);
    }
}