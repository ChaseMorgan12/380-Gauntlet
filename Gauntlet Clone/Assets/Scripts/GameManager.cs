using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles the game manager that manages the game
*/

public class GameManager : Singleton<GameManager>, IObserver
{
    //Fields

    private GameObject _currentLevel;

    [SerializeField] private GameObjectList _levelList;
    [SerializeField] private GameObjectList _pickupList;

    private void CallNarrator()
    {

    }

    private void SpawnEnemies()
    {

    }

    private void LoadNextLevel()
    {
        GameObject level = GetRandomLevel();

        if (_currentLevel) Destroy(_currentLevel);

        level = Instantiate(level);

        _currentLevel = level;

        BasePlayer[] players = PlayerManager.Instance.players.ToArray();

        List<GameObject> pickupLocations = new List<GameObject>();
        
        foreach(Transform t in level.transform)
        {
            if (t.name.Contains("Pickup"))
            {
                t.gameObject.SetActive(false);
                pickupLocations.Add(t.gameObject);
            }
        }

        for (int index = 0; index < pickupLocations.Count; index++)
        {
            Instantiate(GetRandomPickup(), pickupLocations[index].transform.position, Quaternion.identity, level.transform);
        }


        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = Vector3.zero + (Vector3.up * 1.1f);
        }
    }

    private GameObject GetRandomLevel()
    {
        GameObject go = _levelList.list[Random.Range(0, _levelList.list.Length - 1)];

        return go;
    }

    private GameObject GetRandomPickup()
    {
        GameObject go = _pickupList.list[Random.Range(0, _pickupList.list.Length - 1)];

        return go;
    }

    private void Start()
    {
        LoadNextLevel();
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
