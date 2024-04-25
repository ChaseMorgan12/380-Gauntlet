using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 00/00/0000
*  Script Description:
*/

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected float _health = 10;
    public float Health => _health;

    public abstract void Damage(float amount);
}
