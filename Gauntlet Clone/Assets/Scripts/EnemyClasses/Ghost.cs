using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/02/20024
*  Script Description: Handles behavior for the Ghost Enemy
*/

public class Ghost : BaseEnemy
{
    private void Awake()
    {
        //Based on ememy level, damage will change
        switch (enemyLevel)
        {
            case 1:
                damage = 10;
                break;
            case 2:
                damage = 20;

                break;
            case 3:
                damage = 30;
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
        MoveTowardsPlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SelfDestruct();
        }
    }
    private void SelfDestruct()
    {
        //throw new System.NotImplementedException();
        Destroy(this.gameObject);
        Debug.Log("enemy got hit, destroyed: " + gameObject);
    }

}
