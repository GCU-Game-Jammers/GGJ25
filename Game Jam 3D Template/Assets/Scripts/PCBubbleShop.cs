using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PCBubbleShop : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject currentBottle;
    [SerializeField] private GameObject bottlePrefab;

    [SerializeField] private Image image;

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
        image.gameObject.SetActive(false);
        // lerp bottle to the end point
        StartCoroutine(LerpBottle(bottle.transform));
    }
    public void EnableSpawn()
    {
        canSpawn = true;
        image.gameObject.SetActive(true);
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
