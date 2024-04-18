using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/18/2024
*  Script Description: Handles the BasePlayer class that all players inherit from
*/

public class BasePlayer : Subject
{
    public PlayerType playerType;

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

    protected PlayerData _playerData;

    protected virtual void Awake()
    {

    }

    public virtual void Attack1() //Ranged
    {
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
            if (col.tag == "Enemy")
            {
                Debug.Log("Hit enemy");
            }
        }
    }
}
