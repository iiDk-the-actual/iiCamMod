using iiCamMod;
using System;
using UnityEngine;

namespace iiCamMod.Comps
{
    internal class RightGrabTrigger : MonoBehaviour
    {
        private void Start()
        {
            gameObject.layer = 18;
        }

        private void OnTriggerStay(Collider col)
        {
            if (col.name.Contains("Right") && InputManager.instance.RightGrip & !CameraController.Instance.fpv)
            {
                CameraController.Instance.CameraTablet.transform.parent = CameraController.Instance.RightHandGO.transform;
                if (CameraController.Instance.fp)
                {
                    CameraController.Instance.fp = false;
                }
            }
            if (!InputManager.instance.RightGrip & CameraController.Instance.CameraTablet.transform.parent == CameraController.Instance.RightHandGO.transform)
            {
                CameraController.Instance.CameraTablet.transform.parent = null;
            }
        }
    }
}
