using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;

    float shakeAmount = 0;

    private void Awake()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    public void Shake(float amount, float length) {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);

    }

    private void BeginShake() {
        if (shakeAmount>0)
        {
            Vector3 camPos = mainCam.transform.position; 
            float offSetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offSetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x = offSetX;
            camPos.y = offSetY;
            camPos.z = 0f;

            mainCam.transform.localPosition = camPos;
        }
    }

    private void StopShake() {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition =  Vector3.zero;
    }
}
