using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProceduralRoomInfo", menuName = "New Procedural Room Info", order = 57)]
public class ProceduralRoomInfo : ScriptableObject
{
    //Fields
    [SerializeField] private int maxAmount = 1;
    [SerializeField] private float rarity = 1;
    [SerializeField] private string specialTag;

    //Properties
    public int MaxAmount => maxAmount;
    public float Rarity => rarity;
    public string SpecialTag => specialTag;
}
