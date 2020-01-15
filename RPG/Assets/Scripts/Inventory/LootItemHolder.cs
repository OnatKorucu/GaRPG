﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform = null;
    [SerializeField] private float _rotationSpeed = 0f;

    public void TakeItem(Item item)
    {
        item.transform.SetParent(_itemTransform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.gameObject.SetActive(true);
        item.WasPickedUp = false;
    }

    void Update()
    {
        float amount = Time.deltaTime * _rotationSpeed;
        _itemTransform.Rotate(0, amount, 0);
    }
}