using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles the game manager that manages the game
*/

public class GameManager : Singleton<GameManager>, IObserver
{
    //Fields

    [SerializeField] private GameObject _currentLevel;

    private void CallNarrator()
    {

    }

    private void SpawnEnemies()
    {

    }

    private void LoadNextLevel(GameObject level)
    {
        Destroy(_currentLevel);

        _currentLevel = Instantiate(level);
        BasePlayer[] players = PlayerManager.Instance.players.ToArray();

        GameObject spawn = level.transform.GetChild(0).gameObject;

        if (spawn.name != "Spawn")
            spawn = level.transform.Find("Spawn").gameObject;


        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawn.transform.position;
        }
    }

    public override void Awake()
    {
        base.Awake();
        Exit.OnExit += LoadNextLevel;

        if (_currentLevel == null)
        {
            _currentLevel = GameObject.Find("LevelA");
        }
    }

    public void Notify(Subject subject)
    {

    }

    public void ClearEnemies()
    {
        Debug.Log("Clearing enemies");
    }
}
