using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/04/2024
*  Script Description: Handles behavior for the Grunt Enemy
*/

public class Grunt : BaseEnemy
{
    private float clubRange = 1.7f;
    private bool canSwingClub = true;

    private void Awake()
    {
        //Based on ememy level, damage will change
        switch (enemyLevel)
        {
            case 1:
                damage = 50;
                break;
            case 2:
                damage = 80;

                break;
            case 3:
                damage = 100;
                break;
            default:
                break;
        }
        speed = 2f;
        detectionRange = 25f;
        moveRange = 0.1f;
    }

    private void FixedUpdate()
    {
        if (PlayerInClubRange())
        {
            if (canSwingClub)
            {
                StartCoroutine(SwingClubTimer());
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    private void SwingClub()
    {
        Debug.Log("Swinging Club");
        _player.GetComponent<BasePlayer>().TakeDamage(damage);
    }
    private bool PlayerInClubRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, clubRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player")) //can swing club towards player if player is found in range
            {
                _player = hitColliders[i].gameObject;
                transform.LookAt(_player.transform);
                return true;
            }
        }
        return false;
    }
    private IEnumerator SwingClubTimer()
    {
        canSwingClub = false;
        SwingClub();
        yield return new WaitForSeconds(2f);
        canSwingClub = true;
    }
    
}
