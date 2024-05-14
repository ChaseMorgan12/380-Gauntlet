using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan, Conner Zepeda
*  Last Updated: 05/13/2024
*  Script Description: Handles the pickup mechanics
*/

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType _pickupType;

    [SerializeField] private float healthGiven = 10;
    [SerializeField] private int treasureAmount = 100;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            BasePlayer player = collision.transform.GetComponent<BasePlayer>();

            switch (_pickupType)
            {
                case PickupType.Food:
                    player.TakeDamage(-healthGiven);
                    break;
                case PickupType.Potion:
                    GameManager.Instance.ClearEnemies();
                    break;
                case PickupType.Key:
                    player.PlayerData.Keys++;
                    break;
                case PickupType.Treasure:
                    player.IncreasePoints(treasureAmount);
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}

public enum PickupType
{
    Food,
    Potion,
    Key,
    Treasure
}
