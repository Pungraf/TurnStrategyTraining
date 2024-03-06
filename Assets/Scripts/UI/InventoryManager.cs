using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private GameObject InventoryGO;
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private InventorySlot[] equipmentSlots;
    [SerializeField] private GameObject inventoryItemPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more then one  InventoryManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ToggleInventory()
    {
        InventoryGO.SetActive(!InventoryGO.activeSelf);
    }

    public bool AddItem(Item item)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.GetItem() == item &&
                itemInSlot.GetItem().IsStackable() &&
                (itemInSlot.Count < itemInSlot.GetItem().MaxStack))
            {
                itemInSlot.Count++;
                itemInSlot.RefreshCount();
                return true;
            }
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        slot.SetItemToSlot(inventoryItem);
        inventoryItem.InitializeItem(item);
    }

    public void SpawnNewItem(Item item, InventorySlot slot, int count)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        slot.SetItemToSlot(inventoryItem);
        inventoryItem.Count = count;
        inventoryItem.InitializeItem(item);
    }

    public void LoadData(GameData data)
    {
        foreach (SerializableEquipmentSlot inventorySlot in data.inventoryItems)
        {
            SpawnNewItem(Resources.Load<Item>("Items/" + inventorySlot.itemName),
                         inventorySlots[inventorySlot.slotID],
                         inventorySlot.count);
        }

        foreach (SerializableEquipmentSlot equipmentSlot in data.equipmentSlots)
        {
            SpawnNewItem(Resources.Load<Item>("Items/" + equipmentSlot.itemName),
                         equipmentSlots[equipmentSlot.slotID],
                         equipmentSlot.count);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.inventoryItems.Clear();
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].GetItemInSlot() != null)
            {
                data.inventoryItems.Add(
                    new SerializableEquipmentSlot
                    {
                        slotID = i,
                        itemName = inventorySlots[i].GetItemInSlot().GetItem().name,
                        count = inventorySlots[i].GetItemInSlot().Count
                    });
            }
        }

        data.equipmentSlots.Clear();
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].GetItemInSlot() != null)
            {
                data.equipmentSlots.Add(
                    new SerializableEquipmentSlot
                    {
                        slotID = i,
                        itemName = equipmentSlots[i].GetItemInSlot().GetItem().name,
                        count = equipmentSlots[i].GetItemInSlot().Count
                    });
            }
        }
    }
}
