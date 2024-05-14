using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/13/2024
*  Script Description: Holds data for a player
*/

public class PlayerData
{
    public event Action StatsChanged;
    private float _health, _damage, _magicDamage, _meleeDamage, _armor;
    private int _currentScore, _keys;

    public PlayerData(float health, float damage, float magic, float melee, float armor)
    {
        _health = health;
        _damage = damage;
        _magicDamage = magic;
        _meleeDamage = melee;
        _armor = armor;
    }
    
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

    public float MagicDamage
    {
        get
        {
            return _magicDamage;
        }
        set
        {
            _magicDamage = value;
            StatsChanged?.Invoke();
        }
    }

    public float MeleeDamage
    {
        get
        {
            return _meleeDamage;
        }
        set
        {
            _meleeDamage = value;
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

    public int CurrentScore
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
    public int Keys
    {
        get
        {
            return _keys;
        }
        set
        {
            _keys = value;
            StatsChanged?.Invoke();
        }
    }
}
