using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles the generator class that spawns enemies after a certain amount of time
*/

public class Generator : Damageable
{
    //Fields
    [SerializeField] private GameObject enemy;
    [SerializeField, Range(0.25f, 30f)] private float spawnInterval = 1f;

    private void Start()
    {

    }

    private IEnumerator Spawn()
    {
        yield return null;
    }

    public override void Damage(float amount)
    {
        throw new System.NotImplementedException();
    }
}
