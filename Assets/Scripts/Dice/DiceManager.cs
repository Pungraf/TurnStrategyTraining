using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    [SerializeField] private Transform dicePanelTransform;
    [SerializeField] private GameObject diceSlotPrefab;
    [SerializeField] private List<Dice> dices;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more then one  DiceManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        SpawnDice(dices);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDice(List<Dice> dice)
    {
        foreach(Dice d in dice)
        {
            GameObject newSlot = Instantiate(diceSlotPrefab, dicePanelTransform);
            DiceSlot diceSlot = newSlot.GetComponent<DiceSlot>();
            diceSlot.InitializeSlot(d);
        }
    }
}
