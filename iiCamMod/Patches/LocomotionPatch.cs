using System;
using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;

namespace iiCamMod.Patches
{
    [HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
    internal class LocomotionPatch
    {
        private static void Postfix()
        {
            CameraController.Instance.LocomotionUpdate();
        }
    }
}
