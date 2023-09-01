using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] private TextMeshProUGUI countText;

    private Item item;
    private int count = 1;

    private void Start()
    {
        RefreshCount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag= transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = InputManager.Instance.GetMouseScreenPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void InitializeItem(Item item)
    {
        this.item = item;
        image.sprite = item.image;
    }


    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public Item GetItem()
    {
        return item;
    }

    public int Count
    {
        get => count;
        set
        {
            count = value;
            if(count < 0) 
            {
                count = 0;
            }
            RefreshCount();
        }
    }
}