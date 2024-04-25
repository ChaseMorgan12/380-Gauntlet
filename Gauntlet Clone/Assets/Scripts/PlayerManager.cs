using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/18/2024
*  Script Description: Manages all of the players in the current scene
*/

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject _elf, _wizard, _warrior, _valkyrie;

    private bool _choosingPlayer = false;

    [HideInInspector]
    public List<BasePlayer> players = new();

    public event Action<GameObject> PlayerConnected;

    public override void Awake()
    {
        GetComponent<PlayerInputManager>().playerJoinedEvent.AddListener(PlayerJoined);
        GetComponent<PlayerInputManager>().playerLeftEvent.AddListener(PlayerLeft);
        base.Awake();
    }

    private void PlayerJoined(PlayerInput plr)
    {
        players.Add(plr.GetComponent<BasePlayer>());

        PlayerConnected?.Invoke(plr.gameObject);
    }

    private void PlayerLeft(PlayerInput plr)
    {
        players.Remove(plr.GetComponent<BasePlayer>());
    }

    private void ChoosePlayer()
    {
        _choosingPlayer = true;
    }

    //REMOVE FOR TESTING ONLY!!!

    private void OnGUI()
    {
        if (_choosingPlayer)
        {
            Time.timeScale = 0;

            
        }
    }
}
