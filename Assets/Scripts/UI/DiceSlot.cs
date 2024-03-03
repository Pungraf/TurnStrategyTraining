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

    public void InitializeSlot(Dice dice)
    {
        Dice newDiceInstance = Instantiate(dice);
        Tray = Instantiate(trayPrefab, transform);
        diceObject = newDiceInstance.SummonDice();
        DiceManager.Instance.DiceObjects.Add(diceObject);
        diceObject.transform.SetParent(transform);
        diceObject.DiceSlot = this;
        diceTransform = diceObject.transform;
        diceTransform.position = new Vector3(Tray.transform.position.x + 5f, Tray.transform.position.y, Tray.transform.position.z);
    }

    public void ShakeDice()
    {
        diceObject.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * shakeForce);
    }
}
