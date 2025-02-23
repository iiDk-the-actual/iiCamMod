using iiCamMod;
using System;
using UnityEngine;

namespace iiCamMod.Comps
{
    internal class LeftGrabTrigger : MonoBehaviour
    {
        private void Start()
        {
            gameObject.layer = 18;
        }

        private void OnTriggerStay(Collider col)
        {
            if (col.name.Contains("Left") && InputManager.instance.LeftGrip & !CameraController.Instance.fpv)
            {
                CameraController.Instance.CameraTablet.transform.parent = CameraController.Instance.LeftHandGO.transform;
                if (CameraController.Instance.fp)
                {
                    CameraController.Instance.fp = false;
                }
            }
            if (!InputManager.instance.LeftGrip & CameraController.Instance.CameraTablet.transform.parent == CameraController.Instance.LeftHandGO.transform)
            {
                CameraController.Instance.CameraTablet.transform.parent = null;
            }
        }
    }
}
