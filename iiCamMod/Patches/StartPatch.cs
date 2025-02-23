using System;
using HarmonyLib;
using UnityEngine;

namespace iiCamMod.Patches
{
    [HarmonyPatch(typeof(GorillaTagger))]
    [HarmonyPatch("Start", 0)]
    internal class StartPatch
    {
        private static void Postfix()
        {
            new GameObject().AddComponent<CameraController>();
            CameraController.Instance.YizziStart();
        }
    }
}
