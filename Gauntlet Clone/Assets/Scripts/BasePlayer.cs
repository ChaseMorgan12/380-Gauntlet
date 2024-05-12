using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/18/2024
*  Script Description: Handles the BasePlayer class that all players inherit from
*/

public class BasePlayer : Subject
{
    public PlayerType playerType;

    private int _keys = 0;

    public int Keys
    {
        get
        {
            return _keys;
        }
        set
        {
            _keys = value;
            Debug.Log(_keys);
        }
    }

    [Header("Projectile Info")]
    [SerializeField] protected GameObject _playerProjectile;
    [SerializeField] protected float _projectileSpeed = 10f;
    [SerializeField] protected float _projectileDamage = 1f;

    [Header("Magic Info")]
    [SerializeField] protected GameObject _magicProjectile;
    [SerializeField] protected float _magicDamage = 1f;

    [Header("Melee Info")]
    [SerializeField] protected float _meleeRange = 2f;
    [SerializeField] protected float _meleeDamage = 1f;

    public PlayerData PlayerData { get; protected set; }

    protected virtual void Awake()
    {
        PlayerData = new PlayerData();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Enemy Projectiles
        if (other.CompareTag("Rock"))
        {
            TakeDamage(3);
            Destroy(other.gameObject);
            Debug.Log("Hit by rock, Damage: 3");
        }
        else if (other.CompareTag("Fireball"))
        {
            TakeDamage(5);
            Destroy(other.gameObject);
            Debug.Log("Hit by fireball, Damage: 5");
        }
        else if (other.CompareTag("SorcererSpell"))
        {
            TakeDamage(7);
            Destroy(other.gameObject);
            Debug.Log("Hit by SorcererSpell, Damage: 7");
        }
    }
    public virtual void Attack1() //Ranged
    {
        Debug.Log(_playerProjectile);
        GameObject proj = Instantiate(_playerProjectile, transform.position, Quaternion.identity);

        proj.GetComponent<Rigidbody>().velocity = transform.forward * _projectileSpeed;
    }

    public virtual void Attack2() //Magic
    {
        if (_magicProjectile == null) return;

        GameObject proj = Instantiate(_magicProjectile, transform.position, Quaternion.identity);

        proj.GetComponent<Rigidbody>().velocity = transform.forward * _projectileSpeed;
    }

    public virtual void Attack3() //Melee
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + (transform.forward * _meleeRange), _meleeRange);

        foreach (Collider col in colliders)
        {
            Debug.Log(col.name);
            if (col.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy");
            }
        }
    }

    public virtual void TakeDamage(float amount)
    {
        PlayerData.Health -= amount;

        Debug.Log(PlayerData.Health);

        if (PlayerData.Health <= 0)
        {
            Debug.Log(gameObject.name + " has died!");
        }
    }

    public virtual void IncreasePoints(int amount)
    {
        PlayerData.CurrentScore += amount;
    }

    /// <summary>
    /// It's like a constructor, except it's not ;)
    /// </summary>
    /// <param name="info">The info to "construct" with</param>
    public virtual void ConstructWithInfo(PlayerInfo info)
    {
        _playerProjectile = info.playerProjectile;
        _projectileSpeed = info.projectileSpeed;
        _projectileDamage = info.projectileDamage;
        _magicProjectile = info.magicProjectile;
        _magicDamage = info.magicDamage;
        _meleeDamage = info.meleeDamage;
        _meleeRange = info.meleeRange;
    }
}