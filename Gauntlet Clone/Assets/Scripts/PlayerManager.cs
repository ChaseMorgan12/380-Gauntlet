using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 05/07/2024
*  Script Description: Manages all of the players in the current scene
*/

public class PlayerManager : Singleton<PlayerManager>
{

    private bool _choosingPlayer = false;

    [SerializeField] private PlayerInfo _elf, _wizard, _warrior, _valkyrie; //Using these as templates, not really the best way, 

    [HideInInspector]
    public List<BasePlayer> players = new();

    public static event Action<GameObject> PlayerConnected;

    private PlayerType _playerChosen;
    private bool _chosePlayer = false;

    public override void Awake()
    {
        base.Awake();

        BasePlayer.PlayerDeath += PlayerDied;
        Exit.OnExit += RevivePlayers;
    }

    public void OnPlayerJoined(PlayerInput plr)
    {

        plr.actionEvents[4].AddListener(ChoosePlayerWithButton); //Dirty code, honestly don't know why we are even using UnityEvents, they suck for this...
        //It also breaks randomly which is probably due to the fact that I have no damn clue what index it is if it changes

        PlayerConnected?.Invoke(plr.gameObject);
        StartCoroutine(ChoosePlayer(plr));
    }

    public void OnPlayerLeft(PlayerInput plr)
    {
        Debug.Log(plr.gameObject.name + " has left");
        players.Remove(plr.GetComponent<BasePlayer>());
    }

    private IEnumerator ChoosePlayer(PlayerInput player)
    {
        yield return new WaitUntil(() => !_choosingPlayer);

        _choosingPlayer = true;

        Time.timeScale = 0;

        do
        {
            yield return new WaitUntil(() => _chosePlayer);
            _choosingPlayer = false;
        } 
        while (!UIManager.Instance.ChoosePlayer((int)_playerChosen));

        //Set up the player
        player.SwitchCurrentActionMap("Controls");
        Destroy(player.GetComponent<BasePlayer>());

        yield return null; //Skip a frame to give the system time to destroy BasePlayer (I spent an hour trying to get this to work, help me)

        player.gameObject.name = _playerChosen.ToString();
        BasePlayer plr = null;

        switch (_playerChosen)
        {
            case PlayerType.Elf:
                plr = player.AddComponent<Elf>();
                plr.ConstructWithInfo(_elf);
                break;
            case PlayerType.Wizard:
                plr = player.AddComponent<Wizard>();
                plr.ConstructWithInfo(_wizard);
                break;
            case PlayerType.Warrior:
                plr = player.AddComponent<Warrior>();
                plr.ConstructWithInfo(_warrior);
                break;
            case PlayerType.Valkyrie:
                plr = player.AddComponent<Valkyrie>();
                plr.ConstructWithInfo(_valkyrie);
                break;
            default:
                break;
        }

        plr.playerType = _playerChosen;
        plr.gameObject.GetComponent<PlayerController>().SetupCommands();
        players.Add(plr);
        if (players.Count > 1)
        {
            plr.transform.position = players[0].transform.position;
        }

        Time.timeScale = 1;
    }

    private void PlayerDied(GameObject player)
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;
        player.GetComponent<Collider>().enabled = false;

        bool allDead = true;

        foreach(BasePlayer p in players)
        {
            if (p.PlayerData.Health > 0)
                allDead = false;
        }

        if (allDead)
        {
            StartCoroutine(UIManager.Instance.GameOver());
            return;

            /*for (int index = 0; index < players.Count; index++)
            {
                Destroy(players[index].GetComponent<PlayerInput>());
                //players[index].gameObject.SetActive(false);
                UIManager.Instance.RemovePlayer(index);
            }

            while (players.Count > 0)
            {
                players.Remove(players[0]);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); */
        }
        player.GetComponent<BasePlayer>().Reset();
    }

    public void RevivePlayers()
    {
        foreach (var player in players)
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<MeshRenderer>().enabled = true;
            player.GetComponent<Collider>().enabled = true;


        }
    }

    public void ChoosePlayerWithButton(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        if (context.performed)
        {
            switch (context.ReadValue<Vector2>())
            {
                case Vector2 vec when vec == Vector2.up:
                    _playerChosen = PlayerType.Elf;
                    break;
                case Vector2 vec when vec == Vector2.down:
                    _playerChosen = PlayerType.Valkyrie;
                    break;
                case Vector2 vec when vec == Vector2.left:
                    _playerChosen = PlayerType.Warrior;
                    break;
                case Vector2 vec when vec == Vector2.right:
                    _playerChosen = PlayerType.Wizard;
                    break;
            }

            _chosePlayer = true;
        }
    }
}
