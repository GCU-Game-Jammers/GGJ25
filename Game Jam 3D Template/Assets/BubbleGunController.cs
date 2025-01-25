using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGunController : MonoBehaviour
{
    public ParticleSystem bubbleGun;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bubbleGun.Emit(1);
        }
    }
}
