using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private bool[] playersSelected = new bool[4];

    private GameObject joinUI, leaderUI;

    /// <summary>
    /// Attempt to choose a player
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool ChoosePlayer(int index)
    {
        if (index < 0 && index > 3)
            return false;

        if (playersSelected[index] == false)
        {
            playersSelected[index] = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemovePlayer(int index)
    {
        if (index < 0 && index > 3)
            return false;

        if (playersSelected[index] == true)
        {
            playersSelected[index] = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
