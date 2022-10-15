using Il2CppSystem.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BonelabMultiplayerMockup.Utils
{
    public static class Utils
    {
        public static T CopyComponent<T>(this Component original, GameObject destination) where T : Component
        {
            Il2CppSystem.Type type = Il2CppType.Of<T>(original);
            var dst = destination.GetComponent(type) as T;
            if (!dst) dst = destination.AddComponent(type) as T;
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            var fields = type.GetFields(flags);
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }
            var props = type.GetProperties(flags);
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }
            return dst as T;
        }
    }
}