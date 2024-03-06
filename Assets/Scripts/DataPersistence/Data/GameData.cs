using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public List<SerializableEquipmentSlot> inventoryItems;
    public List<SerializableEquipmentSlot> equipmentSlots;

    public GameData()
    {
        inventoryItems = new List<SerializableEquipmentSlot> {};
        equipmentSlots = new List<SerializableEquipmentSlot> {};
    }
}