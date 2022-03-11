using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal static class Extensions
    {
        internal static float Quantize(this float value) => (int) (value/0.1f)*0.1f;

        internal static Vector3 Quantize(this Vector3 value) => new Vector3(value.x.Quantize(), value.y.Quantize(), value.z);
    }
}
