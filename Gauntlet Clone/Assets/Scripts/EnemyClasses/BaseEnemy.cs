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
    protected float damage, speed, attackRange, attackCooldown, detectionRange, moveRange;

    protected int enemyLevel;

    protected GameObject _player;
    private void OnTriggerEnter(Collider other)
    {
        //Projectiles
        if (other.CompareTag("PlayerProjectile")) //Replace rock with something PLayer Projectiles
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Fireball")) //Can die to other enemy's fireball
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("Hit by fireball, Damage: 5");
        }
    }
    public virtual void MoveTowardsPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                transform.LookAt(hitColliders[i].transform);
                transform.position = Vector3.MoveTowards(transform.position, hitColliders[i].transform.position, moveRange);
            }
        }
    }
    public override void Damage(float amount)
    {
        throw new System.NotImplementedException();
    }
}