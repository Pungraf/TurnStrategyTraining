using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private bool isStackable;
    [SerializeField] private int maxStack;

    public Sprite image;

    public bool IsStackable()
    {
        return isStackable;
    }

    public int MaxStack
    {
        get => maxStack;
        set
        {
            maxStack = value;
        }
    }
}
