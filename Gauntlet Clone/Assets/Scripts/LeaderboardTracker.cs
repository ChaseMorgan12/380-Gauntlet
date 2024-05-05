using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardTracker
{
    private GameObject _ui;
    private TMP_Text _typeText, _healthText, _armorText, _scoreText;
    private PlayerData _playerData;
    private PlayerType _playerType;

    public LeaderboardTracker(GameObject ui, PlayerData data, PlayerType type)
    {
        _ui = ui;
        _playerData = data;
        _playerType = type;

        _typeText = _ui.transform.GetChild(0).GetComponent<TMP_Text>();
        _healthText = _ui.transform.GetChild(1).GetComponent<TMP_Text>();
        _armorText = _ui.transform.GetChild(2).GetComponent<TMP_Text>();
        _scoreText = _ui.transform.GetChild(3).GetComponent<TMP_Text>();

        //Set up initial values

        _typeText.text = type.ToString();
        UpdateLeaderboard();

        //Subscribe to events

        _playerData.StatsChanged += UpdateLeaderboard;
    }

    ~LeaderboardTracker()
    {
        _playerData.StatsChanged -= UpdateLeaderboard; //Make sure to remove the listener when this tracker is destroyed
    }

    private void UpdateLeaderboard()
    {
        _healthText.text = "Health: " + _playerData.Health.ToString();
        _armorText.text = "Armor: " + _playerData.Armor.ToString();
        _scoreText.text = "Score: " + _playerData.CurrentScore.ToString();
    }
}
