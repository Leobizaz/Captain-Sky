using UnityEngine;

namespace AQUAS
{
    [AddComponentMenu("AQUAS/Essentials/Camera Lock")]

    //This Class locks the horizontal position of the waterplane to the camera's position
    //The waterplane can be scaled to fill the camera frustum (optional)
    public class AQUAS_CamLock : MonoBehaviour
    {
        public GameObject cam;
        public bool scaleToFrustum;

        // Use this for initialization
        void Start()
        {

            if (cam == null)
            {
                cam = Camera.main.transform.gameObject;
            }

            if (scaleToFrustum)
            {
                transform.localScale = new Vector3(cam.GetComponent<Camera>().farClipPlane*2f/10, 1, cam.GetComponent<Camera>().farClipPlane*2f/10);
                Vector3 boundaryScale = transform.Find("Static Boundary").localScale;
                transform.Find("Static Boundary").localScale = new Vector3(5, boundaryScale.y, 5);
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
        }
    }
}