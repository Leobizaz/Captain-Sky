using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDialogo : MonoBehaviour
{
    public static bool move;
    public GameObject WorldObject;
    public RectTransform UI_Element;
    RectTransform CanvasRect;

    public GameObject adam;
    public GameObject charlie;
    public GameObject marcus;
    public GameObject jack;

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
        else
        {
            WorldObject = null;
        }
        if(GameObject.Find("/Tech/Canvas/Dialogo") != null)
            UI_Element = GameObject.Find("/Tech/Canvas/Dialogo").GetComponent<RectTransform>();
        if (WorldObject != null || UI_Element != null)
        {
            if (WorldObject == null) return;
            if (UI_Element == null) return;
            Vector3 newPos = new Vector3(WorldObject.transform.position.x, WorldObject.transform.position.y - 4, WorldObject.transform.position.z);
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(newPos);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            UI_Element.anchoredPosition = WorldObject_ScreenPosition;
        }
    }
}
