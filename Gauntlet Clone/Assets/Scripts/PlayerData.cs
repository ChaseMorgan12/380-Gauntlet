using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 05/02/2024
*  Script Description: Holds data for a player
*/

public class PlayerData
{
    public event Action StatsChanged;
    private float _health, _damage, _armor, _currentScore;
    
    public float Health
    {
        get 
        {
            return _health;
        }
        set
        {
            _health = value;
            StatsChanged?.Invoke();
        }
    }

    public float Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
            StatsChanged?.Invoke();
        }
    }

    public float Armor
    {
        get
        {
            return _armor;
        }
        set
        {
            _armor = value;
            StatsChanged?.Invoke();
        }
    }

    public float CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            StatsChanged?.Invoke();
        }
    }
}
