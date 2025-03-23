using Cinemachine;
using GorillaLocomotion;
using iiCamMod.Comps;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace iiCamMod
{
    public class CameraController : MonoBehaviour
    {
        private void Awake()
        {
            Instance = this;
        }

        public void YizziStart()
        {
            gameObject.AddComponent<InputManager>().gameObject.AddComponent<UI>();
            ColorScreenGO = LoadBundle("ColorScreen", "iiCamMod.Assets.colorscreen");
            CameraTablet = LoadBundle("CameraTablet", "iiCamMod.Assets.yizzicam");
            FirstPersonCameraGO = GorillaTagger.Instance.mainCamera;
            ThirdPersonCameraGO = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera");
            CMVirtualCameraGO = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1");
            TPVBodyFollower = GorillaTagger.Instance.bodyCollider.gameObject;
            CMVirtualCamera = CMVirtualCameraGO.GetComponent<CinemachineVirtualCamera>();
            FirstPersonCamera = FirstPersonCameraGO.GetComponent<Camera>();
            ThirdPersonCamera = ThirdPersonCameraGO.GetComponent<Camera>();
            LeftHandGO = GorillaTagger.Instance.leftHandTransform.gameObject;
            RightHandGO = GorillaTagger.Instance.rightHandTransform.gameObject;
            CameraTablet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            CameraFollower = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/Camera Follower");
            TabletCameraGO = GameObject.Find("CameraTablet(Clone)/Camera");
            TabletCamera = TabletCameraGO.GetComponent<Camera>();
            FakeWebCam = GameObject.Find("CameraTablet(Clone)/FakeCamera");
            LeftGrabCol = GameObject.Find("CameraTablet(Clone)/LeftGrabCol");
            RightGrabCol = GameObject.Find("CameraTablet(Clone)/RightGrabCol");
            LeftGrabCol.AddComponent<LeftGrabTrigger>();
            RightGrabCol.AddComponent<RightGrabTrigger>();
            MainPage = GameObject.Find("CameraTablet(Clone)/MainPage");
            MiscPage = GameObject.Find("CameraTablet(Clone)/MiscPage");
            FovText = GameObject.Find("CameraTablet(Clone)/MainPage/Canvas/FovValueText").GetComponent<Text>();
            SmoothText = GameObject.Find("CameraTablet(Clone)/MainPage/Canvas/SmoothingValueText").GetComponent<Text>();
            NearClipText = GameObject.Find("CameraTablet(Clone)/MainPage/Canvas/NearClipValueText").GetComponent<Text>();
            MinDistText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/MinDistValueText").GetComponent<Text>();
            SpeedText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/SpeedValueText").GetComponent<Text>();
            TPText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/TPText").GetComponent<Text>();
            TPRotText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/TPRotText").GetComponent<Text>();
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/MiscButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FPVButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FovUP"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FovDown"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FlipCamButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/NearClipUp"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/NearClipDown"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FPButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/ControlsButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/TPVButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/SmoothingDownButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/SmoothingUpButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/BackButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/GreenScreenButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/MinDistDownButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/MinDistUpButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/SpeedDownButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/SpeedUpButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/SpeedDownButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPModeDownButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPModeUpButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPRotButton"));
            Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPRotButton1"));
            foreach (GameObject gameObject in Buttons)
            {
                gameObject.AddComponent<YzGButton>();
            }
            CMVirtualCamera.enabled = false;
            ThirdPersonCameraGO.transform.SetParent(CameraTablet.gameObject.transform, true);
            CameraTablet.transform.position = new Vector3(-65f, 12f, -82f);
            ThirdPersonCameraGO.transform.position = TabletCamera.transform.position;
            ThirdPersonCameraGO.transform.rotation = TabletCamera.transform.rotation;
            ThirdPersonCameraGO.transform.localPosition = Vector3.zero;
            ThirdPersonCameraGO.transform.localRotation = Quaternion.identity;
            CameraTablet.transform.Rotate(0f, 180f, 0f);
            ColorScreenText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/ColorScreenText").GetComponent<Text>();
            ColorButtons.Add(GameObject.Find("ColorScreen(Clone)/Stuff/RedButton"));
            ColorButtons.Add(GameObject.Find("ColorScreen(Clone)/Stuff/GreenButton"));
            ColorButtons.Add(GameObject.Find("ColorScreen(Clone)/Stuff/BlueButton"));
            foreach (GameObject gameObject2 in ColorButtons)
            {
                gameObject2.AddComponent<YzGButton>();
            }
            ScreenMats.Add(GameObject.Find("ColorScreen(Clone)/Screen1").GetComponent<MeshRenderer>().material);
            ScreenMats.Add(GameObject.Find("ColorScreen(Clone)/Screen2").GetComponent<MeshRenderer>().material);
            ScreenMats.Add(GameObject.Find("ColorScreen(Clone)/Screen3").GetComponent<MeshRenderer>().material);
            meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/FakeCamera").GetComponent<MeshRenderer>());
            meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/Tablet").GetComponent<MeshRenderer>());
            meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/Handle").GetComponent<MeshRenderer>());
            meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/Handle2").GetComponent<MeshRenderer>());
            ColorScreenGO.transform.position = new Vector3(-54.3f, 16.21f, -122.96f);
            ColorScreenGO.transform.Rotate(0f, 30f, 0f);
            ColorScreenGO.SetActive(false);
            MiscPage.SetActive(false);
            ThirdPersonCamera.nearClipPlane = 0.1f;
            TabletCamera.nearClipPlane = 0.1f;
            FakeWebCam.transform.Rotate(-180f, 180f, 0f);
            init = true;
            TabletCamera.fieldOfView = 90;
            ThirdPersonCamera.fieldOfView = 90;
            FovText.text = "90";
            NearClipText.text = "0.1";
        }

        public void LocomotionUpdate()
        {
            if (init)
            {
                ThirdPersonCamera.fieldOfView = TabletCamera.fieldOfView;
                ThirdPersonCamera.nearClipPlane = TabletCamera.nearClipPlane * GTPlayer.Instance.scale;
                if (fpv)
                {
                    if (MainPage.activeSelf)
                    {
                        foreach (MeshRenderer meshRenderer in meshRenderers)
                        {
                            meshRenderer.enabled = false;
                        }
                        MainPage.SetActive(false);
                    }
                    CameraTablet.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                    CameraTablet.transform.rotation = Quaternion.Lerp(CameraTablet.transform.rotation, GorillaTagger.Instance.headCollider.transform.rotation, smoothing);
                }
                if (ControllerInputPoller.instance.rightControllerPrimary2DAxis.y > 0.8f && ControllerInputPoller.instance.leftControllerPrimaryButton && ControllerInputPoller.instance.leftControllerSecondaryButton && CameraTablet.transform.parent == null)
                {
                    fp = false;
                    fpv = false;
                    tpv = false;
                    if (!MainPage.activeSelf)
                    {
                        foreach (GameObject gameObject in Buttons)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (MeshRenderer meshRenderer2 in meshRenderers)
                        {
                            meshRenderer2.enabled = true;
                            CameraTablet.transform.Rotate(0f, -180f, 0f);
                        }
                        MainPage.SetActive(true);
                    }
                    CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
                    CameraTablet.transform.LookAt(GTPlayer.Instance.headCollider.transform.position);
                    if (flipped)
                    {
                        flipped = false;
                        ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                        TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                        FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                    }
                }
                if (fp)
                {
                    CameraTablet.transform.LookAt(2f * CameraTablet.transform.position - CameraFollower.transform.position);
                    if (!flipped)
                    {
                        flipped = true;
                        ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                        TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                        FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                    }
                    dist = Vector3.Distance(CameraFollower.transform.position, CameraTablet.transform.position);
                    if (dist > minDist)
                    {
                        CameraTablet.transform.position = Vector3.Lerp(CameraTablet.transform.position, CameraFollower.transform.position, fpspeed);
                    }
                }
                if (tpv)
                {
                    if (MainPage.activeSelf)
                    {
                        foreach (MeshRenderer meshRenderer3 in meshRenderers)
                        {
                            meshRenderer3.enabled = false;
                        }
                        MainPage.SetActive(false);
                    }
                    if (TPVMode == TPVModes.BACK)
                    {
                        if (followheadrot)
                        {
                            targetPosition = CameraFollower.transform.TransformPoint(new Vector3(0.3f, 0.1f, -1.5f));
                        }
                        else
                        {
                            targetPosition = TPVBodyFollower.transform.TransformPoint(new Vector3(0.3f, 0.1f, -1.5f));
                        }
                        CameraTablet.transform.position = Vector3.SmoothDamp(CameraTablet.transform.position, targetPosition, ref velocity, 0.1f);
                        CameraTablet.transform.LookAt(CameraFollower.transform.position);
                    }
                    else if (TPVMode == TPVModes.FRONT)
                    {
                        if (followheadrot)
                        {
                            targetPosition = CameraFollower.transform.TransformPoint(new Vector3(0.1f, 0.3f, 2.5f));
                        }
                        else
                        {
                            targetPosition = TPVBodyFollower.transform.TransformPoint(new Vector3(0.1f, 0.3f, 2.5f));
                        }
                        CameraTablet.transform.position = Vector3.SmoothDamp(CameraTablet.transform.position, targetPosition, ref velocity, 0.1f);
                        CameraTablet.transform.LookAt(2f * CameraTablet.transform.position - CameraFollower.transform.position);
                    }
                    else if (TPVMode == TPVModes.ORBIT)
                    {
                        if (flipped)
                        {
                            flipped = false;
                            ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
                            TabletCameraGO.transform.Rotate(0f, 180f, 0f);
                            FakeWebCam.transform.Rotate(-180f, 180f, 0f);
                        }
                        targetPosition = CameraFollower.transform.position + new Vector3(Mathf.Cos(Time.time * 1f), 0.5f, Mathf.Sin(Time.time * 1f));
                        CameraTablet.transform.position = Vector3.SmoothDamp(CameraTablet.transform.position, targetPosition, ref velocity, 0.1f);
                        CameraTablet.transform.LookAt(CameraFollower.transform.position);
                    }
                    if (ControllerInputPoller.instance.rightControllerPrimary2DAxis.y > 0.8f && ControllerInputPoller.instance.leftControllerPrimaryButton && ControllerInputPoller.instance.leftControllerSecondaryButton)
                    {
                        CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
                        foreach (MeshRenderer meshRenderer4 in meshRenderers)
                        {
                            meshRenderer4.enabled = true;
                        }
                        CameraTablet.transform.parent = null;
                        tpv = false;
                    }
                }
            }
        }

        private GameObject LoadBundle(string goname, string resourcename)
        {
            Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcename);
            AssetBundle assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);
            GameObject result = Instantiate(assetBundle.LoadAsset<GameObject>(goname));
            assetBundle.Unload(false);
            manifestResourceStream.Close();
            return result;
        }

        public static CameraController Instance;
        public GameObject CameraTablet;
        public GameObject FirstPersonCameraGO;
        public GameObject ThirdPersonCameraGO;
        public GameObject CMVirtualCameraGO;
        public GameObject LeftHandGO;
        public GameObject RightHandGO;
        public GameObject FakeWebCam;
        public GameObject TabletCameraGO;
        public GameObject MainPage;
        public GameObject MiscPage;
        public GameObject LeftGrabCol;
        public GameObject RightGrabCol;
        public GameObject CameraFollower;
        public GameObject TPVBodyFollower;
        public GameObject ColorScreenGO;

        public List<GameObject> Buttons = new List<GameObject>();
        public List<GameObject> ColorButtons = new List<GameObject>();
        public List<Material> ScreenMats = new List<Material>();
        public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
        public Camera TabletCamera;
        public Camera FirstPersonCamera;
        public Camera ThirdPersonCamera;
        public CinemachineVirtualCamera CMVirtualCamera;

        public Text FovText;
        public Text NearClipText;
        public Text ColorScreenText;
        public Text MinDistText;
        public Text SpeedText;
        public Text SmoothText;
        public Text TPText;
        public Text TPRotText;
        public bool followheadrot = true;

        public bool canbeused;
        public bool flipped;
        public bool tpv;
        public bool fpv = true;
        public bool fp;
        public bool openedurl;
        public float minDist = 2f;
        private float dist;
        public float fpspeed = 0.01f;
        public float smoothing = 0.05f;

        private Vector3 targetPosition;
        private Vector3 velocity = Vector3.zero;

        public TPVModes TPVMode;
        private bool init;
        public enum TPVModes
        {
            BACK,
            FRONT,
            ORBIT
        }
    }
}
