using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles the game manager that manages the game
*/

public class GameManager : Singleton<GameManager>, IObserver
{
    //Fields
    private int level = 1;

    private void CallNarrator()
    {

    }

    private void SpawnEnemies()
    {

    }

    public void Notify(Subject subject)
    {

    }

    public void ClearEnemies()
    {
        Debug.Log("Clearing enemies");
    }
}
