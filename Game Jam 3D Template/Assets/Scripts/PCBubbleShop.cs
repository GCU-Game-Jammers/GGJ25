using System.Collections;
using UnityEngine;

public class PCBubbleShop : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject currentBottle;
    [SerializeField] private GameObject bottlePrefab;

    [SerializeField] private bool canSpawn = true;

    private void Update()
    {
        if (currentBottle == null && !canSpawn && !GameManager.Instance.hasBottle)
        {
            EnableSpawn();
        }
    }
    public void SpawnBottle()
    {
        if (!canSpawn) return;

        canSpawn = false;
        // Instantiate a bottle prefab at the spawn point
        GameObject bottle = Instantiate(bottlePrefab, spawnPoint.position, spawnPoint.rotation);
        currentBottle = bottle;
        // lerp bottle to the end point
        StartCoroutine(LerpBottle(bottle.transform));
    }
    public void EnableSpawn()
    {
        canSpawn = true;
    }

    IEnumerator LerpBottle(Transform bottle)
    {
        float time = 0;
        float duration = 1f;
        Vector3 startPos = bottle.position;
        Vector3 endPos = endPoint.position;
        while (time < duration && currentBottle != null)
        {
            bottle.position = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

}
