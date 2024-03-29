using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSlot : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform diceTransform;
    [SerializeField] private GameObject trayPrefab;
    [SerializeField] private RawImage image;
    [SerializeField] private float shakeForce;
    [SerializeField] private Button slotButton;

    public Image backGround;
    private bool diceBusy;

    private Transform cameraTransform;
    private RenderTexture renderTexture;
    private GameObject Tray;
    private DiceObject diceObject;

    private float cameraHighOffset = 2f;


    // Start is called before the first frame update
    void Start()
    {
        renderTexture = new RenderTexture(512, 512, 32);
        renderTexture.Create();
        cameraTransform = camera.transform;
        image.texture = renderTexture;
        camera.targetTexture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = new Vector3(diceTransform.position.x,
                                               diceTransform.position.y + cameraHighOffset,
                                               diceTransform.position.z);
    }

    public bool DiceBusy
    {
        get { return diceBusy; }
        set 
        {
            diceBusy = value; 
            if(value == true)
            {
                backGround.color = Color.black;
                slotButton.interactable = false;
            }
            else
            {
                if(DiceManager.Instance.AvailableRerolls > 0)
                {
                    backGround.color = Color.yellow;
                    slotButton.interactable = true;
                }
                else
                {
                    backGround.color = Color.red;
                    slotButton.interactable = true;
                }
            }
        }
    }


    public void InitializeSlot(Dice dice)
    {
        Dice newDiceInstance = Instantiate(dice);
        Tray = Instantiate(trayPrefab, transform);
        diceObject = newDiceInstance.SummonDice();
        DiceManager.Instance.DiceObjects.Add(diceObject);
        diceObject.transform.SetParent(transform);
        diceObject.DiceSlot = this;
        diceTransform = diceObject.transform;
        diceTransform.position = new Vector3(Tray.transform.position.x + 7.5f, Tray.transform.position.y - 4f, Tray.transform.position.z);
    }

    public void ShakeDice()
    {
        if(diceObject.IsActive == true)
        {
            if (DiceManager.Instance.ChangeRerollValue(-1))
            {
                diceObject.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * shakeForce);
                diceObject.IsActive = true;
                DiceManager.Instance.ExecuteRollButtonEnabled(false);
                DiceBusy = true;

            }
            else
            {
                Debug.Log("No more rerolls available.");
            }
        }
        else
        {
            diceObject.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * shakeForce);
            diceObject.IsActive = true;
            DiceManager.Instance.ExecuteRollButtonEnabled(false);
            DiceBusy = true;
        }

    }
}
