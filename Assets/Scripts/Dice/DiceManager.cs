using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public enum SelectedDice
    {
        RangedWeapon,
        Grenade,
        None,
    }

    public static DiceManager Instance { get; private set; }

    public event EventHandler DiceRollFinished;

    [SerializeField] private Transform dicePanelTransform;
    [SerializeField] private GameObject diceSlotPrefab;
    [SerializeField] private List<Dice> dices;
    [SerializeField] private Transform weaponSlots;
    [SerializeField] private Transform grenadeSlots;
    [SerializeField] private Button executeRollButton;
    [SerializeField] private Transform availableRerollsTransform;

    [SerializeField] private int currentRolledTotal;
    [SerializeField] private int maxRerolls;
    [SerializeField] private int availableRerolls;

    private TextMeshProUGUI availableRerollsText;

    [SerializeField] private List<DiceObject> diceObjects;

    public bool allDiceRolled = false;

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
        DiceObject.OnAnyDiceRollAction += DiceObject_OnAnyDiceRoll;
        WeaponSlot.OnAnyWeaponSlotedEvent += WeaponSlot_onAnyWeaponSloted;
        availableRerollsText = availableRerollsTransform.GetComponent<TextMeshProUGUI>();
        RefreshCurrentWeaponDice(SelectedDice.RangedWeapon);
        SpawnDice(dices);
        ExecuteRollButtonEnabled(false);
    }


    private void DiceObject_OnAnyDiceRoll(object sender, EventArgs e)
    {
        allDiceRolled = true;
        currentRolledTotal = 0;
        foreach(DiceObject diceObject in diceObjects)
        {
            currentRolledTotal += diceObject.CurrentRolledValue;
            if(!diceObject.isActive || diceObject.IsRolling())
            {
                allDiceRolled = false;
            }
        }
        ExecuteRollButtonEnabled(allDiceRolled);
    }

    private void WeaponSlot_onAnyWeaponSloted(object sender, EventArgs e)
    {
        RefreshCurrentWeaponDice(SelectedDice.RangedWeapon);
    }

    public void ToggleDicePanel(bool state)
    {
        dicePanelTransform.parent.gameObject.SetActive(state);
    }

    public void RefreshCurrentWeaponDice(SelectedDice selectedDice)
    {
        dices.Clear();
        WeaponSlot[] weapons = new WeaponSlot[dices.Count];
        switch (selectedDice)
        {
            case SelectedDice.RangedWeapon:
                {
                    weapons = weaponSlots.GetComponentsInChildren<WeaponSlot>();
                    break;
                }
            case SelectedDice.Grenade:
                {
                    weapons = grenadeSlots.GetComponentsInChildren<WeaponSlot>();
                    break;
                }
            default: break;

        }
        foreach (WeaponSlot weapon in weapons)
        {
            if (weapon.itemInSlot != null)
            {
                Dice diceToSlot = (Dice)weapon.itemInSlot.Item;
                dices.Add(diceToSlot);
            }
        }
        SpawnDice(dices);
    }

    public void ExecuteRollButtonEnabled(bool enabled)
    {

        executeRollButton.interactable = enabled;
    }

    private void RefreshAllDice()
    {
        foreach (DiceObject diceObject in diceObjects)
        {
            diceObject.isActive = false;
        }
    }

    public List<Dice> Dices
    {
        get{ return dices; }
        set { dices = value; }
    }

    public int CurrentRolledTotal
    {
        get { return currentRolledTotal; }
        set { currentRolledTotal = value; }
    }

    public List<DiceObject> DiceObjects
    {
        get { return diceObjects; }
        set { diceObjects = value; }
    }

    public void SpawnDice(List<Dice> dice)
    {
        foreach (Transform child in dicePanelTransform)
        {
            GameObject.Destroy(child.gameObject);
        }

        diceObjects = new List<DiceObject>();
        foreach(Dice d in dice)
        {
            GameObject newSlot = Instantiate(diceSlotPrefab, dicePanelTransform);
            DiceSlot diceSlot = newSlot.GetComponent<DiceSlot>();
            diceSlot.InitializeSlot(d);
        }
    }

    private void ResetRerolls()
    {
        availableRerolls = maxRerolls;
        RefreshRerollsText();
    }

    public bool ChangeRerollValue(int amount)
    {
        availableRerolls += amount;
        if(availableRerolls < 0)
        {
            availableRerolls = 0;
            RefreshRerollsText();
            return false;
        }
        RefreshRerollsText();
        return true;
    }

    private void RefreshRerollsText()
    {
        availableRerollsText.text = availableRerolls.ToString();
    }

    public void DiceRollRequest()
    {
        RefreshAllDice();
        ResetRerolls();
        ToggleDicePanel(true);
    }

    public void DiceRollDone()
    {
        ToggleDicePanel(false);
        DiceRollFinished?.Invoke(this, EventArgs.Empty);
    }
}
