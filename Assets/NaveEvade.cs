using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NaveEvade : MonoBehaviour
{

    GameObject parent;
    bool once;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !once)
        {
            once = true;
            if (parent.GetComponent<BirdBehaviour>().cantGoDownOrLeftOrRight)
            {
                //up
                parent.transform.DOLocalRotate(new Vector3(0, 0, -30), 2f);
                parent.GetComponent<BirdForward>().fwdSpeed = 65f;
            }
            else
            {
                int randomNum = Random.Range(0, 4);
                switch (randomNum)
                {
                    case 0:
                        //left
                        parent.transform.DOLocalRotate(new Vector3(parent.transform.localRotation.x, 270, parent.transform.localRotation.z), 2f);
                        parent.GetComponent<BirdForward>().fwdSpeed = 65f;
                        break;
                    case 1:
                        //right
                        parent.transform.DOLocalRotate(new Vector3(parent.transform.localRotation.x, 90, parent.transform.localRotation.z), 2f);
                        parent.GetComponent<BirdForward>().fwdSpeed = 65f;
                        break;
                    case 2:
                        //up
                        parent.transform.DOLocalRotate(new Vector3(parent.transform.localRotation.x, parent.transform.localRotation.y, -30), 2f);
                        parent.GetComponent<BirdForward>().fwdSpeed = 65f;
                        break;
                    case 3:
                        //down
                        parent.transform.DOLocalRotate(new Vector3(parent.transform.localRotation.x, parent.transform.localRotation.y, 30), 2f);
                        parent.GetComponent<BirdForward>().fwdSpeed = 65f;
                        break;
                    default:
                        parent.transform.DOLocalRotate(new Vector3(parent.transform.localRotation.x, parent.transform.localRotation.y, -30), 2f);
                        parent.GetComponent<BirdForward>().fwdSpeed = 65f;
                        break;
                }
            }

            Invoke("Despawn", 6f);
        }
    }

    void Despawn()
    {
        parent.GetComponent<BirdBehaviour>().Despawn();
    }
}
