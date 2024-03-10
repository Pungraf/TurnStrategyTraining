using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordAction : BaseAction
{
    public static event EventHandler OnAnySwordHit;

    public event EventHandler OnSwordActionStarted;
    public event EventHandler OnSwordActionCompleted;

    private enum State
    {
        DiceRolling,
        SwingingSwordStarted,
        SwingingSwordHit,
        SwingingSwordAfterHit,
    }

    private int maxSwordDistance = 1;
    private State state;
    private float stateTimer;
    private Unit targetUnit;
    private bool diceRolled = true;

    // Start is called before the first frame update
    void Start()
    {
        DiceManager.Instance.DiceRollFinished += DiceManager_OnDiceRollFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.DiceRolling:
                if (diceRolled == true)
                {
                    DiceManager.Instance.DiceRollRequest();
                    diceRolled = false;
                }
                break;
            case State.SwingingSwordStarted:
                break;
            case State.SwingingSwordHit:
                float rotateSpeed = 10f;
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                break;
            case State.SwingingSwordAfterHit:
                break;
        }

        if (stateTimer < 0f && diceRolled)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case State.DiceRolling:
                state = State.SwingingSwordStarted;
                stateTimer = 0f;
                break;
            case State.SwingingSwordStarted:
                OnSwordActionStarted?.Invoke(this, EventArgs.Empty);
                state = State.SwingingSwordHit;
                float swingDelayTime = 0.7f;
                stateTimer = swingDelayTime;
                break;
            case State.SwingingSwordHit:
                state = State.SwingingSwordAfterHit;
                float afterHitStateTime = 0.1f;
                stateTimer = afterHitStateTime;
                targetUnit.Damage(DiceManager.Instance.CurrentRolledTotal);
                OnAnySwordHit?.Invoke(this, EventArgs.Empty);
                break;
            case State.SwingingSwordAfterHit:
                OnSwordActionCompleted?.Invoke(this, EventArgs.Empty);
                ActionComplete();
                break;
        }
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

    public override string GetActionName()
    {
        return "Sword";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 200,
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxSwordDistance; x <= maxSwordDistance; x++)
        {
            for (int z = -maxSwordDistance; z <= maxSwordDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList; ;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        state = State.DiceRolling;
        float DiceRollingTimer = 0f;
        stateTimer = DiceRollingTimer;


        ActionStart(onActionComplete);
    }

    public int GetMaxSwordistance()
    {
        return maxSwordDistance;
    }
}
