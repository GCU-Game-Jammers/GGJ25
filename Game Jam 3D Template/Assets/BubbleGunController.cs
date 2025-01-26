using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGunController : MonoBehaviour
{
    public ParticleSystem bubbleGun;

    void Update()
    {
        if (Input.GetMouseButton(0) && GameManager.Instance.hasBottle)
        {
            bubbleGun.Emit(1);
        }
    }
}
