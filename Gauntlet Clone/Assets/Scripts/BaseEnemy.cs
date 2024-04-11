using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles the base enemy
*/

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    protected float damage, speed, attackRange, attackCooldown;

    float IDamageable.Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public virtual void Attack1()
    {

    }

    public virtual void Attack2()
    {

    }

    public void Damage(float amount)
    {
        throw new System.NotImplementedException();
    }
}