using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 00/00/0000
*  Script Description:
*/

public class Grunt : BaseEnemy
{
    public GameObject rock;

    private float rockRange = 7f;
    private float rockVelocity = 7f;
    private bool inRockRange = false;
    private bool canThrowRock = true;

    private void Awake()
    {
        speed = 2f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInRockRange())
        {
            if (canThrowRock)
            {
                StartCoroutine(rockTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void ThrowRock()
    {
        //instansiate rock prefab towards player
        Debug.Log("Throwing rock");
        GameObject rockProj = Instantiate(rock, transform.position, transform.rotation);
        rockProj.GetComponent<Rigidbody>().velocity = transform.forward * rockVelocity;
        StartCoroutine(rockDestroyTimer(rockProj));
    }
    private bool PlayerInRockRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rockRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can throw rock towards player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator rockTimer()
    {
        canThrowRock = false;
        ThrowRock();
        yield return new WaitForSeconds(2f);
        canThrowRock = true;
    }
    private IEnumerator rockDestroyTimer(GameObject killRock)
    {
        yield return new WaitForSeconds(2f);
        Destroy(killRock);
    }
    private void ClubAttack()
    {

    }
}
