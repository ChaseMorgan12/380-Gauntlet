using System.Collections;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/04/2024
*  Script Description: Implementing the Lobber enemy behavior
*/

public class Lobber : BaseEnemy
{
    //Initializing unique enemy variables
    public GameObject rock;

    private float runRange = 5f;
    private float rockRange = 15f;
    private float rockVelocity = 10f;
    private bool canThrowRock = true;

    private void Awake()
    {
        damage = 300;
        speed = 2f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInRunRange())
        {
            RunAway();
        }
        else if (PlayerInRockRange())
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
    private bool PlayerInRunRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, runRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //will run away from player if player is found in range
            {
                transform.LookAt(hitColliders[i].transform);
                return true;
            }
        }
        return false;
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
    public virtual void RunAway()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                _player = hitColliders[i].gameObject;
                transform.LookAt(_player.transform);
                transform.position = Vector3.MoveTowards(transform.position, -_player.transform.position, moveRange);
            }
        }
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
}