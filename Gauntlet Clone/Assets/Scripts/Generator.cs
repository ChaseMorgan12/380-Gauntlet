using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 04/25/2024
*  Script Description: Handles the generator class that spawns enemies after a certain amount of time
*/

public class Generator : Damageable
{
    //Fields
    [SerializeField] private GameObject enemy;
    [SerializeField, Range(0.25f, 30f)] private float spawnInterval = 1f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnInterval);

        Vector3 spawnPos = transform.position + (transform.forward * transform.localScale.magnitude + new Vector3(Random.Range(-.500f, .500f), 0, Random.Range(-.500f, .500f)));

        GameObject spawnedEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);

        StartCoroutine(Spawn());
    }

    public override void Damage(float amount)
    {
        _health -= amount;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*TESTING ONLY
    private void OnGUI()
    {
        if (GUILayout.Button("Damage Generator"))
            Damage(10);
    } */
}
