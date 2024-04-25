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
    public float Health { get; protected set; }

    public abstract void Damage(float amount);
}
