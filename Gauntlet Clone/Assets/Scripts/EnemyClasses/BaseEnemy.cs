using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 04/18/2024
*  Script Description: Implementing the base enemy behavior for all enemies to inherit
*/

public abstract class BaseEnemy : Damageable
{
    [SerializeField] protected int scoreGiven = 100;
    protected float damage = 50, speed, attackRange, attackCooldown = 2, detectionRange, moveRange;

    protected int enemyLevel;

    protected bool canAttack = true;

    protected GameObject _player;

    public static event Action<GameObject> enemyDied;
    
    private void OnTriggerEnter(Collider other)
    {
        //Projectiles
        if (other.CompareTag("PlayerProjectile")) //Replace rock with something PLayer Projectiles
        {
            Destroy(other.gameObject);

            Damage(other.GetComponent<ProjectileInfo>().damage);
        }
        else if (other.CompareTag("Fireball")) //Can die to other enemy's fireball
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("Hit by fireball, Damage: 5");
        }
    }

    protected IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public virtual void MoveTowardsPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                _player = hitColliders[i].gameObject;
                transform.LookAt(hitColliders[i].transform);
                transform.position = Vector3.MoveTowards(transform.position, hitColliders[i].transform.position, moveRange);
                return;
            }
        }
    }
    public override void Damage(float amount)
    {
        _health -= amount;

        if (Health <= 0)
        {
            if (_player)
                _player.GetComponent<BasePlayer>().IncreasePoints(scoreGiven);
            Destroy(gameObject);
            enemyDied?.Invoke(gameObject);
        }
    }
}