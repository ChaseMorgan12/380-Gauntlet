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

    public static GameObject lastPotionPlayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            BasePlayer player = collision.transform.GetComponent<BasePlayer>();

            switch (_pickupType)
            {
                case PickupType.Food:
                    player.TakeDamage(-healthGiven);
                    NarratorManager.Instance.AddTextToQueue(player.playerType.ToString() + " has found food... maybe they won't die after all!");
                    break;
                case PickupType.Potion:
                    NarratorManager.Instance.AddTextToQueue("The cleansing has begun.");
                    GameManager.Instance.ClearEnemies();
                    lastPotionPlayer = collision.gameObject;
                    break;
                case PickupType.Key:
                    NarratorManager.Instance.AddTextToQueue(player.playerType.ToString() + " has found the key!");
                    player.PlayerData.Keys++;
                    break;
                case PickupType.Treasure:
                    player.IncreasePoints(treasureAmount);
                    NarratorManager.Instance.AddTextToQueue(player.playerType.ToString() + " has found treasure!");
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
