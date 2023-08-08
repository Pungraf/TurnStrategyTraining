using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool isOpen;

    private GridPosition gridPosition;
    private Animator animator;
    private Action OnInteractionComplete;
    private bool isActive;
    private float timer;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetDoorAtGridPosition(gridPosition, this);

        if(isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            isActive = false;
            OnInteractionComplete();
        }
    }

    public void Interact(Action OnInteractionComplete)
    {
        this.OnInteractionComplete = OnInteractionComplete;
        isActive = true;
        timer = 0.5f;

        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetWalkableGridPosition(gridPosition, true);
    }

    private void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetWalkableGridPosition(gridPosition, false);
    }
}
