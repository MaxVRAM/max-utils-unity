using UnityEngine;

namespace MaxVram.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject SetParentAndZero(this GameObject go, GameObject parent)
        {
            go.transform.parent = parent.transform;
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            return go;
        }
    }
}
