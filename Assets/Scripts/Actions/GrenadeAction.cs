using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    [SerializeField] private Transform grenadeProjectilePrefab;

    private int maxThrowDistance = 7;
    private GridPosition targetPosition;
    private bool canThrowGrenade;
    private State state;
    private float stateTimer;
    private bool diceRolled = true;


    private enum State
    {
        DiceRolling,
        Throw,
        Cooloff,
    }

    private void Start()
    {
        DiceManager.Instance.DiceRollFinished += DiceManager_OnDiceRollFinished;
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        switch (state)
        {
            case State.DiceRolling:
                if (diceRolled == true)
                {
                    DiceManager.Instance.DiceRollRequest();
                    diceRolled = false;
                }
                break;
            case State.Throw:
                if(canThrowGrenade == true)
                {
                    ThrowGrenade();
                    canThrowGrenade = false;
                }
                break;
            case State.Cooloff:
                break;
        }

        if (stateTimer < 0f && diceRolled)
        {
            NextState();
        }
    }

    public override string GetActionName()
    {
        return "Grenade";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for (int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxThrowDistance)
                {
                    continue;
                }


                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetPosition = gridPosition;
        state = State.DiceRolling;
        float DiceRollingTimer = 0f;
        stateTimer = DiceRollingTimer;
        canThrowGrenade = true;

        ActionStart(onActionComplete);
    }

    private void OnGrenadeBehaviourComplete()
    {
        ActionComplete();
    }

    private void ThrowGrenade()
    {
        Transform grenadeProjectileTransform = Instantiate(grenadeProjectilePrefab, unit.GetWorldPosition(), Quaternion.identity);
        GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
        grenadeProjectile.Setup(targetPosition, OnGrenadeBehaviourComplete, DiceManager.Instance.CurrentRolledTotal);
    }

    private void DiceManager_OnDiceRollFinished(object sender, EventArgs e)
    {
        if (!isActive)
        {
            return;
        }

        diceRolled = true;
        NextState();
    }

    private void NextState()
    {
        switch (state)
        {
            case State.DiceRolling:
                state = State.Throw;
                stateTimer = 0f;
                break;
            case State.Throw:
                state = State.Cooloff;
                float CooloffStateTime = 0f;
                stateTimer = CooloffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }
}
