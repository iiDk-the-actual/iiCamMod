using iiCamMod;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace iiCamMod.Comps
{
    internal class YzGButton : MonoBehaviour
    {
        private void Start()
        {
            gameObject.layer = 18;
        }

        private void OnEnable()
        {
            Invoke("ButtonTimer", 1f);
        }

        private void OnDisable()
        {
            CameraController.Instance.canbeused = false;
        }

        private void ButtonTimer()
        {
            if (!enabled)
            {
                CameraController.Instance.canbeused = false;
            }
            CameraController.Instance.canbeused = true;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (CameraController.Instance.canbeused && col.name == "RightHandTriggerCollider" | col.name == "LeftHandTriggerCollider")
            {
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, col.name != "RightHandTriggerCollider", 0.4f);
                GorillaTagger.Instance.StartVibration(col.name != "RightHandTriggerCollider", GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                CameraController.Instance.canbeused = false;
                Invoke("ButtonTimer", 1f);
                string name = base.name;
                if (name != null)
                {
                    switch (name.Length)
                    {
                        case 5:
                            if (!(name == "FovUP"))
                            {
                                return;
                            }
                            CameraController.Instance.TabletCamera.fieldOfView += 5f;
                            if (CameraController.Instance.TabletCamera.fieldOfView > 175f)
                            {
                                CameraController.Instance.TabletCamera.fieldOfView = 5f;
                                CameraController.Instance.ThirdPersonCamera.fieldOfView = 5f;
                            }
                            CameraController.Instance.ThirdPersonCamera.fieldOfView = CameraController.Instance.TabletCamera.fieldOfView;
                            CameraController.Instance.FovText.text = CameraController.Instance.TabletCamera.fieldOfView.ToString();
                            CameraController.Instance.canbeused = true;
                            return;
                        case 6:
                        case 18:
                            return;
                        case 7:
                            if (!(name == "FovDown"))
                            {
                                return;
                            }
                            CameraController.Instance.TabletCamera.fieldOfView -= 5f;
                            if (CameraController.Instance.TabletCamera.fieldOfView < 5f)
                            {
                                CameraController.Instance.TabletCamera.fieldOfView = 175f;
                                CameraController.Instance.ThirdPersonCamera.fieldOfView = 175f;
                            }
                            CameraController.Instance.ThirdPersonCamera.fieldOfView = CameraController.Instance.TabletCamera.fieldOfView;
                            CameraController.Instance.FovText.text = CameraController.Instance.TabletCamera.fieldOfView.ToString();
                            CameraController.Instance.canbeused = true;
                            return;
                        case 8:
                            if (!(name == "FPButton"))
                            {
                                return;
                            }
                            CameraController.Instance.fp = !CameraController.Instance.fp;
                            return;
                        case 9:
                            {
                                char c = name[0];
                                if (c != 'F')
                                {
                                    if (c != 'R')
                                    {
                                        if (c != 'T')
                                        {
                                            return;
                                        }
                                        if (!(name == "TPVButton"))
                                        {
                                            return;
                                        }
                                        if (CameraController.Instance.TPVMode == CameraController.TPVModes.BACK)
                                        {
                                            if (CameraController.Instance.flipped)
                                            {
                                                CameraController.Instance.flipped = false;
                                                CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                                                CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                                                CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                                            }
                                        }
                                        else if (CameraController.Instance.TPVMode == CameraController.TPVModes.FRONT && !CameraController.Instance.flipped)
                                        {
                                            CameraController.Instance.flipped = true;
                                            CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                                            CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                                            CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                                        }
                                        CameraController.Instance.fp = false;
                                        CameraController.Instance.fpv = false;
                                        CameraController.Instance.tpv = true;
                                        return;
                                    }
                                    else
                                    {
                                        if (!(name == "RedButton"))
                                        {
                                            return;
                                        }
                                        using (List<Material>.Enumerator enumerator = CameraController.Instance.ScreenMats.GetEnumerator())
                                        {
                                            while (enumerator.MoveNext())
                                            {
                                                Material material = enumerator.Current;
                                                material.color = Color.red;
                                            }
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!(name == "FPVButton"))
                                    {
                                        return;
                                    }
                                    if (CameraController.Instance.flipped)
                                    {
                                        CameraController.Instance.flipped = false;
                                        CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                                        CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                                        CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                                    }
                                    CameraController.Instance.fp = false;
                                    CameraController.Instance.fpv = true;
                                    return;
                                }
                            }
                        case 10:
                            {
                                char c = name[1];
                                if (c <= 'e')
                                {
                                    if (c != 'a')
                                    {
                                        if (c != 'e')
                                        {
                                            return;
                                        }
                                        if (!(name == "NearClipUp"))
                                        {
                                            return;
                                        }
                                        CameraController.Instance.TabletCamera.nearClipPlane += 0.01f;
                                        if ((double)CameraController.Instance.TabletCamera.nearClipPlane > 1.0)
                                        {
                                            CameraController.Instance.TabletCamera.nearClipPlane = 0.01f;
                                            CameraController.Instance.ThirdPersonCamera.nearClipPlane = 0.01f;
                                        }
                                        CameraController.Instance.ThirdPersonCamera.nearClipPlane = CameraController.Instance.TabletCamera.nearClipPlane;
                                        CameraController.Instance.NearClipText.text = CameraController.Instance.TabletCamera.nearClipPlane.ToString();
                                        CameraController.Instance.canbeused = true;
                                        return;
                                    }
                                    else
                                    {
                                        if (!(name == "BackButton"))
                                        {
                                            return;
                                        }
                                        CameraController.Instance.MainPage.SetActive(true);
                                        CameraController.Instance.MiscPage.SetActive(false);
                                        return;
                                    }
                                }
                                else if (c != 'i')
                                {
                                    if (c != 'l')
                                    {
                                        return;
                                    }
                                    if (!(name == "BlueButton"))
                                    {
                                        return;
                                    }
                                    goto IL_CF9;
                                }
                                else
                                {
                                    if (!(name == "MiscButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.MainPage.SetActive(false);
                                    CameraController.Instance.MiscPage.SetActive(true);
                                    return;
                                }
                            }
                        case 11:
                            {
                                char c = name[0];
                                if (c != 'G')
                                {
                                    if (c != 'T')
                                    {
                                        return;
                                    }
                                    if (!(name == "TPRotButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.followheadrot = !CameraController.Instance.followheadrot;
                                    CameraController.Instance.TPRotText.text = CameraController.Instance.followheadrot.ToString().ToUpper();
                                    return;
                                }
                                else if (!(name == "GreenButton"))
                                {
                                    return;
                                }
                                break;
                            }
                        case 12:
                            {
                                char c = name[0];
                                if (c != 'N')
                                {
                                    if (c != 'T')
                                    {
                                        return;
                                    }
                                    if (!(name == "TPRotButton1"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.followheadrot = !CameraController.Instance.followheadrot;
                                    CameraController.Instance.TPRotText.text = CameraController.Instance.followheadrot.ToString().ToUpper();
                                    return;
                                }
                                else
                                {
                                    if (!(name == "NearClipDown"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.TabletCamera.nearClipPlane -= 0.01f;
                                    if ((double)CameraController.Instance.TabletCamera.nearClipPlane < 0.01)
                                    {
                                        CameraController.Instance.TabletCamera.nearClipPlane = 1f;
                                        CameraController.Instance.ThirdPersonCamera.nearClipPlane = 1f;
                                    }
                                    CameraController.Instance.ThirdPersonCamera.nearClipPlane = CameraController.Instance.TabletCamera.nearClipPlane;
                                    CameraController.Instance.NearClipText.text = CameraController.Instance.TabletCamera.nearClipPlane.ToString();
                                    CameraController.Instance.canbeused = true;
                                    return;
                                }
                            }
                        case 13:
                            {
                                char c = name[0];
                                if (c != 'F')
                                {
                                    if (c != 'S')
                                    {
                                        return;
                                    }
                                    if (!(name == "SpeedUpButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.fpspeed += 0.01f;
                                    if (CameraController.Instance.fpspeed > 0.1)
                                    {
                                        CameraController.Instance.fpspeed = 0.1f;
                                    }
                                    CameraController.Instance.SpeedText.text = CameraController.Instance.fpspeed.ToString();
                                    CameraController.Instance.canbeused = true;
                                    return;
                                }
                                else
                                {
                                    if (!(name == "FlipCamButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.flipped = !CameraController.Instance.flipped;
                                    CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                                    CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                                    CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                                    return;
                                }
                            }
                        case 14:
                            {
                                char c = name[0];
                                if (c != 'C')
                                {
                                    if (c != 'T')
                                    {
                                        return;
                                    }
                                    if (!(name == "TPModeUpButton"))
                                    {
                                        return;
                                    }
                                    if (CameraController.Instance.TPVMode == CameraController.TPVModes.FRONT)
                                    {
                                        CameraController.Instance.TPVMode = CameraController.TPVModes.BACK;
                                    }
                                    else if (CameraController.Instance.TPVMode == CameraController.TPVModes.BACK)
                                    {
                                        CameraController.Instance.TPVMode = CameraController.TPVModes.ORBIT;
                                    }
                                    else if (CameraController.Instance.TPVMode == CameraController.TPVModes.ORBIT)
                                    {
                                        CameraController.Instance.TPVMode = CameraController.TPVModes.FRONT;
                                    }
                                    CameraController.Instance.TPText.text = CameraController.Instance.TPVMode.ToString();
                                    return;
                                }
                                else
                                {
                                    if (!(name == "ControlsButton"))
                                    {
                                        return;
                                    }
                                    if (!CameraController.Instance.openedurl)
                                    {
                                        Application.OpenURL("https://discord.gg/iidk");
                                        CameraController.Instance.openedurl = true;
                                        return;
                                    }
                                    return;
                                }
                            }
                        case 15:
                            {
                                char c = name[0];
                                if (c != 'M')
                                {
                                    if (c != 'S')
                                    {
                                        return;
                                    }
                                    if (!(name == "SpeedDownButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.fpspeed -= 0.01f;
                                    if (CameraController.Instance.fpspeed < 0.01)
                                    {
                                        CameraController.Instance.fpspeed = 0.01f;
                                    }
                                    CameraController.Instance.SpeedText.text = CameraController.Instance.fpspeed.ToString();
                                    CameraController.Instance.canbeused = true;
                                    return;
                                }
                                else
                                {
                                    if (!(name == "MinDistUpButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.minDist += 0.1f;
                                    if (CameraController.Instance.minDist > 10f)
                                    {
                                        CameraController.Instance.minDist = 10f;
                                    }
                                    CameraController.Instance.MinDistText.text = CameraController.Instance.minDist.ToString();
                                    CameraController.Instance.canbeused = true;
                                    return;
                                }
                            }
                        case 16:
                            if (!(name == "TPModeDownButton"))
                            {
                                return;
                            }
                            if (CameraController.Instance.TPVMode == CameraController.TPVModes.FRONT)
                            {
                                CameraController.Instance.TPVMode = CameraController.TPVModes.ORBIT;
                            }
                            else if (CameraController.Instance.TPVMode == CameraController.TPVModes.BACK)
                            {
                                CameraController.Instance.TPVMode = CameraController.TPVModes.FRONT;
                            }
                            else if (CameraController.Instance.TPVMode == CameraController.TPVModes.ORBIT)
                            {
                                CameraController.Instance.TPVMode = CameraController.TPVModes.BACK;
                            }
                            CameraController.Instance.TPText.text = CameraController.Instance.TPVMode.ToString();
                            return;
                        case 17:
                            {
                                char c = name[0];
                                if (c != 'G')
                                {
                                    if (c != 'M')
                                    {
                                        if (c != 'S')
                                        {
                                            return;
                                        }
                                        if (!(name == "SmoothingUpButton"))
                                        {
                                            return;
                                        }
                                        CameraController.Instance.smoothing += 0.01f;
                                        if (CameraController.Instance.smoothing > 0.11f)
                                        {
                                            CameraController.Instance.smoothing = 0.05f;
                                        }
                                        CameraController.Instance.SmoothText.text = CameraController.Instance.smoothing.ToString();
                                        CameraController.Instance.canbeused = true;
                                        return;
                                    }
                                    else
                                    {
                                        if (!(name == "MinDistDownButton"))
                                        {
                                            return;
                                        }
                                        CameraController.Instance.minDist -= 0.1f;
                                        if (CameraController.Instance.minDist < 1f)
                                        {
                                            CameraController.Instance.minDist = 1f;
                                        }
                                        CameraController.Instance.MinDistText.text = CameraController.Instance.minDist.ToString();
                                        CameraController.Instance.canbeused = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    if (!(name == "GreenScreenButton"))
                                    {
                                        return;
                                    }
                                    CameraController.Instance.ColorScreenGO.SetActive(!CameraController.Instance.ColorScreenGO.activeSelf);
                                    if (CameraController.Instance.ColorScreenGO.activeSelf)
                                    {
                                        CameraController.Instance.ColorScreenText.text = "(ENABLED)";
                                        return;
                                    }
                                    CameraController.Instance.ColorScreenText.text = "(DISABLED)";
                                    return;
                                }
                            }
                        case 19:
                            if (!(name == "SmoothingDownButton"))
                            {
                                return;
                            }
                            CameraController.Instance.smoothing -= 0.01f;
                            if (CameraController.Instance.smoothing < 0.05f)
                            {
                                CameraController.Instance.smoothing = 0.11f;
                            }
                            CameraController.Instance.SmoothText.text = CameraController.Instance.smoothing.ToString();
                            CameraController.Instance.canbeused = true;
                            return;
                        default:
                            return;
                    }
                    using (List<Material>.Enumerator enumerator = CameraController.Instance.ScreenMats.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Material material2 = enumerator.Current;
                            material2.color = Color.green;
                        }
                        return;
                    }
                IL_CF9:
                    foreach (Material material3 in CameraController.Instance.ScreenMats)
                    {
                        material3.color = Color.blue;
                    }
                }
            }
        }
    }
}
