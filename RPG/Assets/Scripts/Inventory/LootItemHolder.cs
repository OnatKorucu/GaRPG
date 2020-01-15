﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform = null;
    [SerializeField] private float _rotationSpeed = 0f;

    private Item _item;

    public void TakeItem(Item item)
    {
        _item = item;
        
        _item.transform.SetParent(_itemTransform);
        _item.transform.localPosition = Vector3.zero;
        _item.transform.localRotation = Quaternion.identity;
        _item.gameObject.SetActive(true);
        _item.WasPickedUp = false;

        _item.OnPickedUp += HandleItemPickedUp;
    }

    private void HandleItemPickedUp()
    {
        LootSystem.AddToPool(this);
    }

    void Update()
    {
        float amount = Time.deltaTime * _rotationSpeed;
        _itemTransform.Rotate(0, amount, 0);
    }
}