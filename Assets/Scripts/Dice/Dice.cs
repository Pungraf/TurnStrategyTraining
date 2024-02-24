using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class Dice : Item
{
    [SerializeField] private GameObject diceGO;

    public List<string> namesList = new List<string>();
    public List<FaceGeometry> faces = new List<FaceGeometry>();
    public List<Face> FacesObjects = new List<Face>();

    private GameObject diceObject;


    public GameObject GetDiceObject()
    {
        SummonDice();
        return diceObject;
    }

    private void SummonDice()
    {
        diceObject = Instantiate(diceGO);
        diceObject.name = this.name;
        GenerateFacesPlaces();
        GenerateFaces();
    }

    private void GenerateFaces()
    {
        GameObject currentFace;
        foreach (Face face in FacesObjects)
        {
            if(face != null)
            {
                Destroy(face.gameObject);
            }
        }
        FacesObjects.Clear();
        for (int i = 0; i < faces.Count; i++)
        {
            if (namesList[i] == "" || namesList[i] == "Empty")
            {
                currentFace = (GameObject)Instantiate(Resources.Load("D20Faces/Empty"), diceObject.transform);
            }
            else
            {
                currentFace = (GameObject)Instantiate(Resources.Load("D20Faces/" + namesList[i]), diceObject.transform);
            }
            FacesObjects.Add(currentFace.GetComponent<Face>());

            Face face = currentFace.GetComponent<Face>();
            face.FaceIndex = i;
            face.FaceName = namesList[i];

            currentFace.transform.position = diceObject.transform.position;
            currentFace.layer = diceObject.layer;

            Quaternion baseRot = Quaternion.Euler(-90f, 0, 0);
            Quaternion rot = baseRot * new Quaternion(faces[i].rx, -faces[i].ry, -faces[i].rz, faces[i].rw);
            currentFace.transform.rotation = rot;
        }
    }

    protected virtual void GenerateFacesPlaces()
    {
        faces.Clear();
    }
}
