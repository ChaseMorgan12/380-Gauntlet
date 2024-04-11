using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/11/2024
*  Script Description: Handles the generator class that spawns enemies after a certain amount of time
*/

public class Generator : MonoBehaviour, IDamageable
{
    //Fields
    [SerializeField] private GameObject enemy;
    [SerializeField, Range(0.25f, 30f)] private float spawnInterval = 1f;

    float IDamageable.Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {

    }

    private IEnumerator Spawn()
    {
        yield return null;
    }

    public void Damage(float amount)
    {
        throw new System.NotImplementedException();
    }
}
