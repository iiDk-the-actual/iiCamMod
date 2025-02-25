using System;
using System.Reflection;
using HarmonyLib;

namespace iiCamMod
{
    public class HarmonyPatches
    {
        public static bool IsPatched { get; private set; }

        internal static void ApplyHarmonyPatches()
        {
            if (!IsPatched)
            {
                if (instance == null)
                {
                    instance = new Harmony(PluginInfo.GUID);
                }
                instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            if (instance != null && IsPatched)
            {
                instance.UnpatchSelf();
                IsPatched = false;
            }
        }

        private static Harmony instance;
        public const string InstanceId = PluginInfo.GUID;
    }
}
