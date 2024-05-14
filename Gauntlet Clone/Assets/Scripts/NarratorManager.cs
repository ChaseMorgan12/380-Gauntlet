using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* FILE HEADER
*  Edited by: Conner Zepeda , Chase Morgan
*  Last Updated: 05/13/2024
*  Script Description: Manages the behavior for the narrator, putting text onto the screen when needed
*/

public class NarratorManager : Singleton<NarratorManager>, IObserver
{
    [SerializeField]
    private TextMeshProUGUI narrartorTMP;

    //Queue
    private readonly Queue<string> textQueue = new Queue<string>();

    private string narratorText;

    private int dialogueIndex;

    private PlayerType referencePlayerType;

    public void Notify(Subject subject)
    {
        BasePlayer player = (BasePlayer)subject;
        if (player.PlayerData.Health < 200)
        {
            referencePlayerType = player.playerType;
            dialogueIndex = Random.Range(0, 3); //runs through dialogue options
            if (dialogueIndex == 0) 
            {
                narratorText = referencePlayerType.ToString() + "'s life force is running out";
            }
            else if (dialogueIndex == 1)
            {
                narratorText = referencePlayerType.ToString() + " needs food";
            }
            else
            {
                narratorText = referencePlayerType.ToString() + " is about to die!";
            }
            AddTextToQueue(narratorText);
        }
    }

    //The narrator frequently makes statements repeating the game's rules, including:
    //"Shots do not hurt other players – yet", "Remember, don't shoot food!", "Elf – shot the food!",
    //and "Warrior needs food – badly!" Occasionally, the narrator will comment on the battle by saying, "I've not seen such bravery!" or
    //"Let's see you get out of here!" When a player's "life force" points fell below 200, the narrator states,
    //"Your life force is running out", "Elf needs food", or "Valkyrie ... is about to die!"


    // Start is called before the first frame update
    void Start()
    {
        narrartorTMP.gameObject.SetActive(false);
        StartCoroutine(NarratorDialogueQueue());
    }

    private IEnumerator NarratorDialogueQueue()
    {
        yield return new WaitUntil(() => textQueue.Count > 0);

        narrartorTMP.gameObject.SetActive(true);
        narrartorTMP.text = textQueue.Dequeue();

        yield return new WaitForSeconds(3);

        narrartorTMP.gameObject.SetActive(false);

        StartCoroutine(NarratorDialogueQueue());
    }

    public void AddTextToQueue(string desiredText)
    {
        if (textQueue.Contains(desiredText)) return;

        textQueue.Enqueue(desiredText);
    }
}
