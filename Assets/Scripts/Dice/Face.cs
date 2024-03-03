using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] private GameObject faceCenter;

    public int FaceIndex;
    public string FaceName;
    public string type;

    public int faceValue;

    public GameObject FaceCenter
    {
        get { return faceCenter; }
        set { faceCenter = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
