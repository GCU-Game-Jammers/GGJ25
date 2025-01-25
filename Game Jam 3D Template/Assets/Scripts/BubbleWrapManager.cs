using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BubbleWrapManager : MonoBehaviour
{
    
    [SerializeField] private BubbleWrapButton[,] currentBubbleWrapGrid = new BubbleWrapButton[5, 5];

    [SerializeField] private BubbleWrapButton[,] bubbleWrapToMatch = new BubbleWrapButton[5, 5];

    public static BubbleWrapManager instance;

    [SerializeField] private int score = 0; // Score of the player aka how many bubbles they to pop before it is considered correct
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // We get our interactive bubble wrap grid, set the i value of each bubble
        RefreshGrid();

        // Listen for event interaction

    }
    public void BubblePop(int i)
    {
        if (i == 1)
        {
            Debug.Log("Correct Bubble");
            score++;
        }
        else
        {
            Debug.Log("Incorrect Bubble");
            score--;
        }

        if (score == 0)
        {
            Debug.Log("You Have Popped the Correct Pattern");

            // TEMP

        }
    }

    public void RefreshGrid()
    {
        // I can think of many ways why I dont like this way of doing it. this is what trying to brute force a solution gets you
        Debug.Log("Refreshing Grid");
        for (int i = 0; i < 5; i++)
        {
            GameObject row = GameObject.Find("Row " + i);

            GameObject row2 = GameObject.Find("Row1 " + i); // terrible I know
            
            for (int j = 0; j < 5; j++)
            {
                bubbleWrapToMatch[i, j] = row2.transform.GetChild(j).gameObject.GetComponent<BubbleWrapButton>();
                currentBubbleWrapGrid[i, j] = row.transform.GetChild(j).gameObject.GetComponent<BubbleWrapButton>();
                currentBubbleWrapGrid[i, j].body.SetActive(true);

                if (bubbleWrapToMatch[i, j].i == 1) // if the bubble is supposed to be popped 
                {
                    currentBubbleWrapGrid[i,j].i = 1;
                    score--;
                }
                else if (bubbleWrapToMatch[i, j].i == 0) // if the bubble is not supposed to be popped, only show the correct ones on the wall
                {
                    bubbleWrapToMatch[i, j].body.SetActive(false);
                }

                Debug.Log(currentBubbleWrapGrid[i, j].name);
            }
        }
    }
}


