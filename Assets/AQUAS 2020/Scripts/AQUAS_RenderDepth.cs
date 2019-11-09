using UnityEngine;

namespace AQUAS
{

    //This script renders a depth buffer to a render texture
    //The buffer can later be used to bypass a step in Unity's render pipeline
    public class AQUAS_RenderDepth : MonoBehaviour
    {

        [HideInInspector]
        public GameObject plane;

        Material material;
        RenderTexture target;

        string shaderPath;

        // Use this for initialization
        void Start()
        {

            material = new Material(Shader.Find("Hidden/AQUAS/Utils/Screen Depth"));

            target = new RenderTexture(Screen.width/1, Screen.height/1, 32, RenderTextureFormat.ARGBHalf);
            target.filterMode = FilterMode.Bilinear;

            GetComponent<Camera>().targetTexture = target;
            //GetComponent<Camera>().SetReplacementShader(Shader.Find("Depth Only"), null);

            shaderPath = plane.GetComponent<Renderer>().sharedMaterials[0].shader.name;
            plane.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Hidden/AQUAS/Desktop/Front/Opaque");
        }

        private void OnApplicationQuit()
        {
            plane.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find(shaderPath);
        }

        private void OnPreCull()
        {
            if (plane == null)
            {
                return;
            }
            plane.layer = LayerMask.NameToLayer("Water");
        }

        private void OnPostRender()
        {
            if (plane == null)
            {
                return;
            }

            plane.layer = LayerMask.NameToLayer("Default");
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);

            plane.GetComponent<Renderer>().sharedMaterial.SetTexture("_DeTex", target);

            target.Release();
        }
    }
}
