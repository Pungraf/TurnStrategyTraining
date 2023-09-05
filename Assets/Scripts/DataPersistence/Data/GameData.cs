using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public List<SerializableEquipmentSlot> inventoryItems;

    public GameData()
    {
        inventoryItems = new List<SerializableEquipmentSlot> {};
    }
}