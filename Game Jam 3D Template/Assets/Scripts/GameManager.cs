using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameStarted = false;
    public bool hasBottle = false;
    public Animator bubbleGodAnimator;
    public Material ditherMaterial;

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

        bubbleGodAnimator.SetInteger("BubbleLevel", 0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1) && Input.GetKey(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (bubbleGodLevel == 3) bubbleGodLevel = -1;
            ClickBubble();
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
        bubbleGodAnimator.SetInteger("BubbleLevel", bubbleGodLevel);
        

        // Change Post Processing to Next Material

        // Play Bubble God Dialogue
        if (bubbleGodLevel > bubbleLevelsinkJson.Length) // stop at max level
        {
            return;
        }
        InkDialogueManager.GetDialogueManager().StartDialogue(bubbleLevelsinkJson[bubbleGodLevel-1]);
    }

}
