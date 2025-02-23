using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace iiCamMod.Comps
{
    public class InputManager : MonoBehaviour
    {
        private void Start()
        {
            instance = this;
        }

        private void Update()
        {
            LeftGrip = ControllerInputPoller.instance.leftGrab;
            RightGrip = ControllerInputPoller.instance.rightGrab;
            LeftPrimaryButton = ControllerInputPoller.instance.leftControllerPrimaryButton;
            RightPrimaryButton = ControllerInputPoller.instance.rightControllerPrimaryButton;
            if (Gamepad.current != null)
            {
                GPLeftStick = Gamepad.current.leftStick.ReadValue();
                GPRightStick = Gamepad.current.rightStick.ReadValue();
            }
        }

        public static InputManager instance;

        public bool LeftGrip;
        public bool RightGrip;

        public bool LeftPrimaryButton;
        public bool RightPrimaryButton;

        public Vector2 GPLeftStick;
        public Vector2 GPRightStick;
    }
}
