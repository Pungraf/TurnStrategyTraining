using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceObject : MonoBehaviour
{
    public static event EventHandler OnAnyDiceRollAction;

    public bool isActive = false;

    private int currentRolledValue;
    private List<Face> diceFaces = new List<Face>();
    private List<GameObject> facesCenters = new List<GameObject>();
    private List<FaceGeometry> diceGeometries;
    private List<string> namesList;
    private DiceSlot diceSlot;
    private Rigidbody rb;

    private bool faceIsChoosed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(IsRolling() && faceIsChoosed)
        {
            faceIsChoosed = false;
        }

        if(!IsRolling() && !faceIsChoosed)
        {
            ChoosedFace();
        }
    }

    public int CurrentRolledValue
    {
        get { return currentRolledValue; }
        set { currentRolledValue = value; }
    }

    public List<Face> DiceFaces
    {
        get { return diceFaces; }
        set { diceFaces = value; }
    }

    public List<FaceGeometry> DiceGeometries
    {
        get { return diceGeometries; }
        set { diceGeometries = value; }
    }

    public List<string> NamesList
    {
        get { return namesList; }
        set { namesList = value; }
    }

    public DiceSlot DiceSlot
    {
        get { return diceSlot; }
        set { diceSlot = value; }
    }

    public bool IsRolling()
    {
        if(rb.velocity == Vector3.zero)
        {
            return false;
        }
        return true;
    }

    private void ChoosedFace()
    {
        Face currentHighest = null;
        foreach (GameObject faceCenter  in facesCenters)
        {
            if ((faceCenter.transform.position - this.gameObject.transform.position).normalized.y == Vector3.up.y)
            {
                currentHighest = faceCenter.transform.parent.GetComponent<Face>();
            }
        }
        currentRolledValue = currentHighest.faceValue;
        faceIsChoosed = true;
        OnAnyDiceRollAction?.Invoke(this, EventArgs.Empty);
    }

    public void GenerateFaces()
    {
        GameObject currentFace;
        foreach (Face face in diceFaces)
        {
            if (face != null)
            {
                Destroy(face.gameObject);
            }
        }
        diceFaces.Clear();
        for (int i = 0; i < namesList.Count; i++)
        {
            if (namesList[i] == "" || namesList[i] == "Empty")
            {
                currentFace = (GameObject)Instantiate(Resources.Load("D20Faces/Empty"), transform);
            }
            else
            {
                currentFace = (GameObject)Instantiate(Resources.Load("D20Faces/" + namesList[i]), transform);
            }
            diceFaces.Add(currentFace.GetComponent<Face>());
            facesCenters.Add(currentFace.GetComponent<Face>().FaceCenter);

            Face face = currentFace.GetComponent<Face>();
            face.FaceIndex = i;
            face.FaceName = namesList[i];

            currentFace.transform.position = transform.position;
            currentFace.layer = this.gameObject.layer;

            Quaternion baseRot = Quaternion.Euler(-90f, 0, 0);
            Quaternion rot = baseRot * new Quaternion(diceGeometries[i].rx, -diceGeometries[i].ry, -diceGeometries[i].rz, diceGeometries[i].rw);
            currentFace.transform.rotation = rot;
        }
    }
}
