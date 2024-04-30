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
    [SerializeField, Range(1, 100)] private int maxSpawnAmount = 25;

    private readonly List<GameObject> enemies = new();

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnInterval);

        if (enemies.Count < maxSpawnAmount)
        {
            Vector3 spawnPos = transform.position + (transform.forward * transform.localScale.magnitude + new Vector3(Random.Range(-.500f, .500f), 0, Random.Range(-.500f, .500f)));

            GameObject spawnedEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
            enemies.Add(spawnedEnemy);
        }
        else
        {
            //Clear up space so we can spawn more later
            List<GameObject> enemiesToRemove = new();
            foreach(GameObject enemy in enemies)
            {
                if (enemy == null)
                    enemiesToRemove.Add(enemy);
            }

            foreach (GameObject enemy in enemiesToRemove)
                enemies.Remove(enemy);
        }

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
}
