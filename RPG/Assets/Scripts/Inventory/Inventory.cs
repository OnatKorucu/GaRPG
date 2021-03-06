using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> ActiveItemChanged;
    public event Action<Item> ItemPickedUp;

    [SerializeField] private Transform _rightHand = null;

    private List<Item> _items = new List<Item>();
    public List<Item> Items => _items;
    
    private Transform _itemRoot;

    public Item ActiveItem { get; private set; }

    private void Awake()
    {
        _itemRoot = new GameObject("Items").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item)
    {
        _items.Add(item);
        item.transform.SetParent(_itemRoot);
        ItemPickedUp?.Invoke(item);
        item.WasPickedUp = true;

        Equip(item);
    }

    public void Equip(Item item)
    {
        if (ActiveItem != null)
        {
            ActiveItem.transform.SetParent(_itemRoot);
            ActiveItem.gameObject.SetActive(false);
        }
        
        
        Debug.Log($"Equipped Item {item.gameObject.name}");

        Transform itemTransform;
        itemTransform = item.transform;
        itemTransform.SetParent(_rightHand);
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;

        ActiveItem = item;
        ActiveItemChanged?.Invoke(ActiveItem);
    }
}