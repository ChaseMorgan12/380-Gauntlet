using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameObject List", menuName = "GameObject List", order = 2)]
public class GameObjectList : ScriptableObject
{
    public GameObject[] list;
}
