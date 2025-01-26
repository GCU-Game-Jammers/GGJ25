using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGodController : MonoBehaviour
{
    int NumHitToAdvance = 250;
    public float scaleModifier = 1.5f;
    int hitCount = 0;
    private Vector3 initScale;

    private void Awake()
    {
        initScale = transform.localScale;
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("HIT");
        hitCount++; 

        transform.localScale = Vector3.Lerp(initScale, initScale * scaleModifier, (float)hitCount/(float)NumHitToAdvance);


        if (hitCount >= NumHitToAdvance)
        {
            GameManager.Instance.BubbleGodFullyBubbled();
        }
    }
}
