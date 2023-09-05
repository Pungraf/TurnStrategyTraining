using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private List<Unit> unitList;
    private List<Unit> enemyUnitList;
    private List<Unit> friendlyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        unitList = new List<Unit>();
        enemyUnitList= new List<Unit>();
        friendlyUnitList= new List<Unit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Unit.OnAnyUnitSpawn += Unit_OnAnyUnitSpawn;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Unit_OnAnyUnitSpawn(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        unitList.Add(unit);

         if(unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
         else
        {
            friendlyUnitList.Add(unit);
        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        Debug.Log("Despawned: " + unit);

        unitList.Remove(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitList.Remove(unit);
        }
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitList;
    }
}
