using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : InventorySlot
{
    public static event EventHandler OnAnyWeaponSlotedEvent;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        OnAnyWeaponSlotedEvent?.Invoke(this, EventArgs.Empty);
    }
}
