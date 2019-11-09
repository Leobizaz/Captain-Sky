using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AQUAS
{
    [AddComponentMenu("AQUAS/Essentials/Depth Support")]
    //This script is used for presampling screen & depth buffer outside of Unity's render pipeline
    //to allow AQUAS to write correct data to the depth buffer while being transparent
    public class AQUAS_DepthSupport : MonoBehaviour
    {

        public GameObject waterPlane;
        
        void Start()
        {

            GameObject depthCamObject = new GameObject("Depth Cam");
            depthCamObject.transform.SetParent(transform, false);

            GameObject colorCamObject = new GameObject("Color Cam");
            colorCamObject.transform.SetParent(transform, false);

            depthCamObject.hideFlags = HideFlags.HideInHierarchy;
            colorCamObject.hideFlags = HideFlags.HideInHierarchy;
            Camera depthCam = depthCamObject.AddComponent<Camera>();
            Camera colorCam = colorCamObject.AddComponent<Camera>();

            depthCam.CopyFrom(GetComponent<Camera>());
            colorCam.CopyFrom(GetComponent<Camera>());
            
            depthCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Water"));
            colorCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Water"));

            //The feature of this script is tied to one plane or more precisely to one material
            if (waterPlane == null)
            {
                waterPlane = GameObject.Find("Water Plane");

                if (waterPlane == null)
                {
#if UNITY_EDITOR
                    EditorUtility.DisplayDialog("Attention!", "The camera is missing a waterplane property. Depth rendering will fail.", "OK");
#endif
                    return;
                }
            }
            depthCamObject.AddComponent<AQUAS_RenderDepth>().plane = waterPlane;
            colorCamObject.AddComponent<AQUAS_RenderColor>().plane = waterPlane;
        }
    }
}
