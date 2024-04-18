using System.Collections;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Edited by: Conner Zepeda
*  Last Updated: 04/18/2024
*  Script Description: Implementing the Lobber enemy behavior
*/

public class Lobber : BaseEnemy
{
    private float rockRange = 7f;
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
        Debug.Log("Throwing rock");
        //instansiate rock prefab towards player
    }
    private bool PlayerInRockRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rockRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can throw rock towards player if player is found in range
            {
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
}