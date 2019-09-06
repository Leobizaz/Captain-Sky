using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveDialogo : MonoBehaviour
{
    public static bool move;
    public GameObject player;
    public GameObject WorldObject;
    public RectTransform UI_Element;
    RectTransform CanvasRect;

    public GameObject adam;
    public GameObject charlie;
    public GameObject marcus;
    public GameObject jack;

    public Camera boundaryCamera;

    [SerializeField]
    private GameObject dialogo;

    private void Start()
    {
        move = true;
        CanvasRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (ComunicadorAdam.active) WorldObject = adam;
        else if (ComunicadorCharlie.active) WorldObject = charlie;
        else if (ComunicadorMarcus.active) WorldObject = marcus;
        else if (ComunicadorJack.active) WorldObject = jack;
        else
        {
            WorldObject = null;
        }
        if(GameObject.Find("/Tech/Canvas/HUD/Dialogo") != null)
            UI_Element = GameObject.Find("/Tech/Canvas/HUD/Dialogo").GetComponent<RectTransform>();
        if (WorldObject != null || UI_Element != null)
        {
            if (WorldObject == null) return;
            if (UI_Element == null) return;
            Vector3 newPos = new Vector3(WorldObject.transform.position.x, WorldObject.transform.position.y - 7, WorldObject.transform.position.z);
            if (ComunicadorJack.active)
            {
                if (player.transform.localPosition.y > -10f)
                    newPos = Vector3.Lerp(newPos, new Vector3(WorldObject.transform.position.x, WorldObject.transform.position.y - 3, WorldObject.transform.position.z), Time.time * 1f);
                else
                    newPos = Vector3.Lerp(newPos, new Vector3(WorldObject.transform.position.x, WorldObject.transform.position.y + 3, WorldObject.transform.position.z), Time.time * 1f);
                    //newPos = new Vector3(WorldObject.transform.position.x, WorldObject.transform.position.y + 3, WorldObject.transform.position.z);
            }
            //clamping

            Vector3 pos = Camera.main.WorldToViewportPoint(newPos);

            //Boundary em relação a camera

            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);

            newPos = Camera.main.ViewportToWorldPoint(pos);

            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(newPos);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            UI_Element.anchoredPosition = WorldObject_ScreenPosition;
        }
    }
}
