using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* FILE HEADER
*  Edited by: Conner Zepeda
*  Last Updated: 05/13/2024
*  Script Description: Manages the behavior for the narrator, putting text onto the screen when needed
*/

public class NarratorManager : Singleton<NarratorManager>, IObserver
{
    [SerializeField]
    private TextMeshProUGUI NarrartorTMP;

    //Queue
    private Queue<string> textQueue = new Queue<string>();

    private string NarratorText;

    private int dialogueIndex;

    private bool queueIsEmpty;

    private PlayerType referencePlayerType;

    public void Notify(Subject subject)
    {
        if (subject.GetComponent<PlayerData>().Health < 200)
        {
            referencePlayerType = ((BasePlayer)subject).playerType;
            dialogueIndex = Random.Range(0, 3); //runs through dialogue options
            if (dialogueIndex == 0) 
            {
                NarratorText = referencePlayerType.ToString() + "'s life force is running out";
            }
            else if (dialogueIndex == 1)
            {
                NarratorText = referencePlayerType.ToString() + " needs food";
            }
            else
            {
                NarratorText = referencePlayerType.ToString() + " is about to die!";
            }
            UpdateNarratorText(NarratorText);
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
        NarrartorTMP.gameObject.SetActive(false);
        //StartCoroutine(NarratorDialogueQueue());
    }

    // Update is called once per frame
    void Update()
    {
        if (textQueue.Count < 0)
        {
            queueIsEmpty = false;
        }
    }

    private void UpdateNarratorText(string desiredText)
    {
        textQueue.Enqueue(desiredText);
    }

    /*private IEnumerator NarratorDialogueQueue()
    {
        //yield return new WaitUntil(!queueIsEmpty);
    }*/
}
