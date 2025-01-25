using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWrapButton : MonoBehaviour
{
    public int i = 0;

    public GameObject body;

    private void Awake()
    {
        body = transform.GetChild(0).gameObject;
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        // is this the correct bubble to pop? send it to the manager to add to the score
        BubbleWrapManager.instance.BubblePop(i);
        body.SetActive(false);
    }
}
