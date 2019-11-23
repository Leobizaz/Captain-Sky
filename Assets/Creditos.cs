using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public float speed;
    public RectTransform text;
    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        up = false;
        Invoke("Uptext", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if(up == true)
        {
            text.transform.localPosition += new Vector3(0f, 1 * speed, 0f);
        }
    }

    public void Uptext()
    {
        up = true;
    }
}
