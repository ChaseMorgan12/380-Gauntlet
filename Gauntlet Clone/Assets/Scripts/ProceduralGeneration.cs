using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 01/19/2024
*  Script Description: Handles the procedural generation of the given prefab list
*/


public class ProceduralGeneration : MonoBehaviour
{
    //Fields
    [SerializeField] private bool generateOnAwake;
    [SerializeField] private GameObjectList prefabList;
    [SerializeField, Range(0, 100)] private int roomAmount;

    public delegate void GenerateDelegate(GameObject generatedObject);
    public static event GenerateDelegate OnGenerate;

    //Properties
    public List<GameObject> SpawnedObjects {get; private set;} = new();

    private List<GameObject> spawnableObjects = new();

    public List<Vector2Int> Map {get; private set;} = new();
    //Constructors
    
    //Methods
    private void Awake()
    {
        spawnableObjects = prefabList.list.ToList();
        
        if(generateOnAwake)
        {
            GenerateRooms(roomAmount);
        }
    }

    public void GenerateRoomMap(int amount)
    {
        Map.Add(Vector2Int.zero); //The map will be in Vector2 format. Each 1 "meter" is considered a room
        List<Vector2Int> availableVectors = new()
        {
            Vector2Int.zero
        };
        for (int index = 1; index < amount; index++)
        {
            List<Vector2Int> vectorsToBeRemoved = new();
            foreach (Vector2Int vector2 in availableVectors)
            {
                if (Map.Contains(vector2 + Vector2Int.up) && Map.Contains(vector2 + Vector2Int.down) && Map.Contains(vector2 + Vector2Int.right) && Map.Contains(vector2 + Vector2Int.left))
                {
                    vectorsToBeRemoved.Add(vector2);
                }
            }
            foreach (Vector2Int vector2 in vectorsToBeRemoved)
            {
                availableVectors.Remove(vector2);
            }

            int maxTries = 50;
            Vector2Int vector = availableVectors[Random.Range(0, availableVectors.Count)];
            bool unique = false;
            do
            {
                maxTries--;
                int num = Random.Range(0, 4);
                switch (num)
                {
                    case 0:
                        if (!Map.Contains(vector + Vector2Int.up))
                        {
                            vector += Vector2Int.up;
                            unique = true;
                        }
                        break;
                    case 1:
                        if (!Map.Contains(vector + Vector2Int.down))
                        {
                            vector += Vector2Int.down;
                            unique = true;
                        }
                        break;
                    case 2:
                        if (!Map.Contains(vector + Vector2Int.right))
                        {
                            vector += Vector2Int.right;
                            unique = true;
                        }
                        break;
                    case 3:
                        if (!Map.Contains(vector + Vector2Int.left))
                        {
                            vector += Vector2Int.left;
                            unique = true;
                        }
                        break;
                }
            } while (!unique && maxTries > 0);

            if (unique)
            {
                Map.Add(vector);
                availableVectors.Add(vector);
            }
        }
        Debug.Log(Map.Count);
    }

    /// <summary>
    /// Generates the amount of rooms given
    /// </summary>
    /// <param name="amount">The amount of rooms</param>
    public void GenerateRooms(int amount)
    {
        string[] specialTags = new string[1] {"Start"};
        List<GameObject> spawnableRooms = prefabList.list.ToList();

        for(int index = 0; index < amount; index++)
        {
            GameObject room = null;
            float totalRarity = 0;
            if (index == 0) //First one spawned should always be the spawn
            {
                room = Instantiate(spawnableRooms[0], new Vector3(0,0,0), Quaternion.identity);
                spawnableRooms.Remove(room); //Only one spawn room should spawn so we don't need to check any special parameters
                SpawnedObjects.Add(room);
                continue;
            }

            foreach(GameObject obj in spawnableRooms)
            {
                totalRarity += obj.GetComponent<ProceduralRoom>().RoomInfo.Rarity;
            }

            float number = Random.Range(0, totalRarity);

            foreach(GameObject obj in spawnableRooms)
            {
                if(number <= obj.GetComponent<ProceduralRoom>().RoomInfo.Rarity)
                {
                    room = obj;
                    break;
                }
                else{ number -= obj.GetComponent<ProceduralRoom>().RoomInfo.Rarity; }
            }

            List<GameObject> exits = new();

            foreach(GameObject obj in SpawnedObjects)
            {
                obj.GetComponent<ProceduralRoom>().GetConnectedRooms();
                foreach(GameObject go in obj.GetComponent<ProceduralRoom>().ExitsToBeBlocked)
                {
                    exits.Add(go);
                }
            }

            //Debug.Log(exits.Count);

            if (exits.Count == 0) break; //There are no more exits that need a room (Impossible, but good as a failsafe)

            GameObject exitChosen = null;

            room = Instantiate(spawnableRooms[0], new Vector3(0,0,0), Quaternion.identity);
            room.name = "SpawnedRoom" + index;

            int tries = 10;
            bool unique = true;

            do
            {
                unique = true;
                tries--;
                exitChosen = exits[Random.Range(0, exits.Count)];
                if (exitChosen.transform.Find("Block").localScale.x > exitChosen.transform.Find("Block").localScale.y) //Either top or bottom
            {
                if (exitChosen.transform.Find("Block").position.y < 0) //Bottom
                {
                    //Top exit should connect
                    room.transform.position = exitChosen.transform.Find("Block").position + (Vector3.down * (room.transform.Find("Background").localScale.y / 2));
                }
                else //Top
                {
                    //Bottom exit should connect
                    room.transform.position = exitChosen.transform.Find("Block").position + (Vector3.up * (room.transform.Find("Background").localScale.y / 2));
                }
            }
            else //Either left or right
            {
                if (exitChosen.transform.Find("Block").position.x < 0) //Left
                {
                    //Right exit should connect
                    room.transform.position = exitChosen.transform.Find("Block").position + (Vector3.left * (room.transform.Find("Background").localScale.x / 2));
                }
                else //Right
                {
                    //Left exit should connect
                    room.transform.position = exitChosen.transform.Find("Block").position + (Vector3.right * (room.transform.Find("Background").localScale.x / 2));
                }
            }

            foreach (GameObject go in SpawnedObjects)
            {
                if (go.transform.Find("Background").position == room.transform.Find("Background").position)
                {
                    unique = false;
                    break;
                }
            }

            } while (!unique && tries > 0);

            if(tries > 0)
            {
                SpawnedObjects.Add(room);
            }
            else
            {
                Destroy(room);
            }
        }

        foreach(GameObject go in SpawnedObjects)
        {
            Debug.Log(go.name);
            go.GetComponent<ProceduralRoom>().BlockExits();
        }
    }

}
