using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class Dice : Item
{
    [SerializeField] private DiceObject diceObjectPrefab;

    public List<string> namesList = new List<string>();

    protected List<FaceGeometry> faces = new List<FaceGeometry>();

    

    public DiceObject SummonDice()
    {
        DiceObject diceObject = Instantiate(diceObjectPrefab);
        diceObject.DiceGeometries = GenerateFacesPlaces();
        diceObject.NamesList = namesList;
        diceObject.name = this.name;
        diceObject.GenerateFaces();
        return diceObject;
    }

    protected virtual List<FaceGeometry> GenerateFacesPlaces()
    {
        List<FaceGeometry> faces = new List<FaceGeometry>();
        return faces;
    }
}
