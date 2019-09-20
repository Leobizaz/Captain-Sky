using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class AllyDolly : MonoBehaviour
{

    public CinemachineDollyCart dolly;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Setspeed")
        {
            float newspeed = other.GetComponent<Boostzone>().Boost_speed;
            float currentspeed = dolly.m_Speed;
            DOVirtual.Float(currentspeed, newspeed, 4f, SetSpeed).SetEase(Ease.InOutQuad);
        }
    }

        void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }
}
