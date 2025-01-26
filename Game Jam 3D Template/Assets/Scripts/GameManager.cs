using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioSource ambienceAudio;

    public bool gameStarted = false;
    public bool hasBottle = false;
    public Animator bubbleGodAnimator;
    public Material ditherMaterial;
    public GameObject hasBubbleUI;
    public GameObject NotHasBubbleUI;
    public int bubbleGodLevel = 0;

    public Transform camera;
    public float cameraSpeed = 1.0f;

    [SerializeField] private TextAsset[] buyDialogueinkJson;

    [SerializeField] private TextAsset[] bubbleFeedsinkJson;

    [SerializeField] private TextAsset[] hurryinkJson;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        bubbleGodAnimator.SetInteger("BubbleLevel", 0);
        HandlePostProcessing();
        HandleAmbience();
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
        if (Input.GetKeyDown(KeyCode.F8))
            hasBottle = true;


        if (hasBottle)
        {
            hasBubbleUI.SetActive(true);
            NotHasBubbleUI.SetActive(false);
        }
        else
        {
            hasBubbleUI.SetActive(false);
            NotHasBubbleUI.SetActive(true);
        }
    }

    public void BubbleGodFullyBubbled()
    {
        if (!hasBottle) return;

        hasBottle = false;

        // Play bubble god full voice

        if (bubbleGodLevel == 3)
        {
            //BLOW UP BUBBLE
        }

        if (bubbleGodLevel > bubbleFeedsinkJson.Length) // stop at max level
        {
            return;
        }
        InkDialogueManager.GetDialogueManager().StartDialogue(bubbleFeedsinkJson[bubbleGodLevel]);
    }
    
    // Clicking Screen
    public void BuyBubble()
    {
        // Play Bubble God Dialogue
        InkDialogueManager.GetDialogueManager().StartDialogue(hurryinkJson[bubbleGodLevel]);

        // Maybe spawn here maybe don't u choose
        // Spawning in PCBubbleShop.cs
    }

    // Clicking on spawned bubble
    public void ClickBubble()
    {
        bubbleGodLevel++;
        bubbleGodAnimator.SetInteger("BubbleLevel", bubbleGodLevel);


        // Change Post Processing to Next Material
        HandlePostProcessing();
        HandleAmbience();

        // Play Bubble God Dialogue
        if (bubbleGodLevel > bubbleFeedsinkJson.Length) // stop at max level
        {
            return;
        }
        InkDialogueManager.GetDialogueManager().StartDialogue(buyDialogueinkJson[bubbleGodLevel]);
    }


    private void HandlePostProcessing()
    {
        switch(bubbleGodLevel)
        {
            case 0:
                ditherMaterial.SetFloat("_Dither_Spread", 0.001f);
                ditherMaterial.SetFloat("_Color_Resolution", 1024);
                break;
            case 1:
                ditherMaterial.SetFloat("_Dither_Spread", 0.001f);
                ditherMaterial.SetFloat("_Color_Resolution", 128);
                break;
            case 2:
                ditherMaterial.SetFloat("_Dither_Spread", 0.005f);
                ditherMaterial.SetFloat("_Color_Resolution", 1024);
                break;
            case 3:
                ditherMaterial.SetFloat("_Dither_Spread", 0.01f);
                ditherMaterial.SetFloat("_Color_Resolution", 256);
                break;
            default:
                throw new System.Exception("THE BUBBLE GOD'S POWER LEVEL... ITS... OVER 3!!!!");
        }
    }

    private void HandleAmbience()
    {
        switch (bubbleGodLevel)
        {
            case 0:
                ambienceAudio.volume = .1f;
                break;
            case 1:
                ambienceAudio.volume = .25f;
                break;
            case 2:
                ambienceAudio.volume = .75f;
                break;
            case 3:
                ambienceAudio.volume = 1;
                break;
            default:
                throw new System.Exception("THE BUBBLE GOD'S POWER LEVEL... ITS... OVER 3!!!!");
        }
    }
}
