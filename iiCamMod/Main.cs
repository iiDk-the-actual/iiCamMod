using System;
using BepInEx;
using UnityEngine;

namespace iiCamMod
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Main : BaseUnityPlugin
    {
        private void Awake()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            DontDestroyOnLoad(this);
        }
    }
}
