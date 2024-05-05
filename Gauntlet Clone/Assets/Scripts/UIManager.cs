using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    private bool[] playersSelected = new bool[4];
    private LeaderboardTracker[] trackers = new LeaderboardTracker[4];

    private GameObject joinUI, leaderUI;

    private GameObject _currentPlayer;

    public override void Awake()
    {
        base.Awake();

        PlayerManager.PlayerConnected += PlayerJoining;
        joinUI = transform.GetChild(0).gameObject;
        leaderUI = transform.GetChild(1).gameObject;
    }

    private void PlayerJoining(GameObject player)
    {
        joinUI.SetActive(true);
        _currentPlayer = player;
    }

    private IEnumerator SetupTracker(GameObject player) //Player may change while it waits so we need a reference
    {
        yield return new WaitUntil(() => player.GetComponent<BasePlayer>() != null);

        trackers[PlayerInputManager.instance.playerCount - 1] = new LeaderboardTracker(leaderUI.transform.GetChild(PlayerInputManager.instance.playerCount - 1).GetChild(1).gameObject,
                                                                                       player.GetComponent<BasePlayer>().PlayerData,
                                                                                       player.GetComponent<BasePlayer>().playerType);
    }

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
            joinUI.transform.GetChild(index + 1).GetChild(0).gameObject.SetActive(true);
            joinUI.SetActive(false);
            leaderUI.transform.GetChild(PlayerInputManager.instance.playerCount - 1).GetChild(0).gameObject.SetActive(false);
            leaderUI.transform.GetChild(PlayerInputManager.instance.playerCount - 1).GetChild(1).gameObject.SetActive(true);
            StartCoroutine(SetupTracker(_currentPlayer));
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
