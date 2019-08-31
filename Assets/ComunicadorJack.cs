using UnityEngine;
public class ComunicadorJack : MonoBehaviour
{
    SpriteRenderer rend;

    public static bool active;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (active)
            rend.enabled = true;
        else
            rend.enabled = false;
    }
}

