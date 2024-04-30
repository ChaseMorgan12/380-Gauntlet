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

    public virtual void Attack1()
    {

    }

    public virtual void Attack2()
    {

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