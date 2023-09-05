using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private bool isStackable;
    [SerializeField] private int maxStack;
    [SerializeField] private string spriteName;

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

    public Sprite GetSprite()
    {
        return UIAssets.Instance.GetSprite(spriteName);
    }
}
