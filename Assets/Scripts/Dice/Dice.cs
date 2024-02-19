using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Dice")]
public class Dice : Item
{
    [SerializeField] private GameObject dicePrefab;

    public GameObject GetDiceObject()
    {
        return dicePrefab;
    }
}
