using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    [SerializeField] private Material colorGreen;
    [SerializeField] private Material colorRed;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Item itemToDrop;

    private GridPosition gridPosition;
    private bool isGreen;
    private Action OnInteractionComplete;
    private bool isActive;
    private float timer;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        SetColorGreen();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActive = false;
            OnInteractionComplete();
        }
    }

    private void SetColorGreen()
    {
        isGreen = true;
        meshRenderer.material = colorGreen;
    }

    private void SetColorRed()
    {
        isGreen = false;
        meshRenderer.material = colorRed;
    }

    public void Interact(Action OnInteractionComplete)
    {
        this.OnInteractionComplete = OnInteractionComplete;

        isActive = true;
        timer = 0.5f;

        if (isGreen)
        {
            SetColorRed();
            if (!InventoryManager.Instance.AddItem(itemToDrop))
            {
                Debug.Log("Max inventory");
            }
        
        }
        else
        {
            SetColorGreen();
        }
    }
}
