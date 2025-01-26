using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameStarted = false;
    public bool hasBottle = false;

    public int bubbleGodLevel = 0;

    public Transform camera;
    public float cameraSpeed = 1.0f;

    [SerializeField] private TextAsset[] buyDialogueinkJson;

    [SerializeField] private TextAsset[] bubbleLevelsinkJson;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    // Clicking Screen
    public void BuyBubble()
    {
        // Play Bubble God Dialogue (Random shop)
        int i = Random.Range(0, buyDialogueinkJson.Length);
        InkDialogueManager.GetDialogueManager().StartDialogue(buyDialogueinkJson[i]);

        // Maybe spawn here maybe don't u choose
        // Spawning in PCBubbleShop.cs
    }

    // Clicking on spawned bubble
    public void ClickBubble()
    {
        bubbleGodLevel++;

        // Change Bubble God Mesh to Next Stage

        // Change Post Processing to Next Material

        // Play Bubble God Dialogue
        InkDialogueManager.GetDialogueManager().StartDialogue(bubbleLevelsinkJson[bubbleGodLevel-1]);
    }

}
