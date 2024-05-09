using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 01/17/2024
*  Script Description: Handles everything a procedural room needs or will need to do
*/

public class ProceduralRoom : MonoBehaviour
{
    //Fields
    [SerializeField] private ProceduralRoomInfo roomInfo;
    [SerializeField] private List<GameObject> exitPoints = new();

    public List<GameObject> ExitsToBeBlocked { get; private set; } = new();

    //Properties
    public ProceduralRoomInfo RoomInfo => roomInfo;
    public List<GameObject> ExitPoints => exitPoints;
    public List<GameObject> PlayersInRoom { get; private set; } = new();
    public List<GameObject> ConnectedRooms { get; private set; } = new();
    public Vector2Int MapLocation {get; private set;} = Vector2Int.zero;
    //Constructors
    
    //Methods
    private void Awake()
    {
        if (exitPoints[0] == null) //This means that they weren't set in the inspector
        {
            Debug.Log("Building Exits");
            exitPoints.Clear();
            foreach (Transform t in transform)
            {
                if(t.name.Contains("Exit")) //As long as an 'Exit' has "Exit" in the name, it will be regarded as an exit.
                {
                    exitPoints.Add(t.gameObject);
                }
            }
            MapLocation = Vector2Int.FloorToInt(transform.position / 100);
        }
        //GetConnectedRooms();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayersInRoom.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && PlayersInRoom.Contains(other.gameObject))
        {
            PlayersInRoom.Remove(other.gameObject);
        }
    }

    public void GetConnectedRooms()
    {
        ConnectedRooms.Clear();
        ExitsToBeBlocked.Clear();
        //Uses a for loop to go through all exit points to see if there is another room on the other side
        //using Physics2D.OverlapCircleAll and adds it to the connected rooms if finding any
        for (int index = 0; index < exitPoints.Count; index++)
        {
            bool found = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(exitPoints[index].transform.GetChild(0).position, 1);
            foreach (Collider2D col in colliders)
            {
                Transform root = col.transform.root;
                Debug.Log(root.gameObject.name != this.gameObject.name);
                if(root.GetComponent<ProceduralRoom>() != null && root.gameObject.name != this.gameObject.name) //This would be another room
                {
                    ConnectedRooms.Add(root.gameObject);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                ExitsToBeBlocked.Add(exitPoints[index]);
            }
        }
    }

    public void BlockExits()
    {
        Vector2Int[] map = GameManager.Instance.GetComponent<ProceduralGeneration>().Map.ToArray();
        Debug.Log(!map.Contains(Vector2Int.FloorToInt(transform.position / 100) + Vector2Int.left));
        if (!map.Contains(Vector2Int.FloorToInt(transform.position / 100) + Vector2Int.up) && exitPoints.Contains(transform.Find("TopExit").gameObject))
        {
            BlockExit(transform.Find("TopExit").gameObject);
        }
        if (!map.Contains(Vector2Int.FloorToInt(transform.position / 100) + Vector2Int.down) && exitPoints.Contains(transform.Find("BottomExit").gameObject))
        {
            BlockExit(transform.Find("BottomExit").gameObject);
        }
        if (!map.Contains(Vector2Int.FloorToInt(transform.position / 100) + Vector2Int.right) && exitPoints.Contains(transform.Find("RightExit").gameObject))
        {
            BlockExit(transform.Find("RightExit").gameObject);
        }
        if (!map.Contains(Vector2Int.FloorToInt(transform.position / 100) + Vector2Int.left) && exitPoints.Contains(transform.Find("LeftExit").gameObject))
        {
            BlockExit(transform.Find("LeftExit").gameObject);
        }
    }

    public void RemoveExit(GameObject exit)
    {
        if(exitPoints.Contains(exit))
        {
            exitPoints.Remove(exit);
            Destroy(exit);
        }
    }

    private void BlockExit(GameObject exit)
    {
        foreach (Transform t in exit.transform)
        {
            if(t.name == "Block")
            {
                t.GetComponent<SpriteRenderer>().enabled = true;
                t.GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
    } 
}
