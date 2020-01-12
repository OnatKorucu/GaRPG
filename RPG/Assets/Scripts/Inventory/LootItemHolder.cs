using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private float _rotationSpeed;

    public void TakeItem(Item item)
    {
        item.transform.SetParent(_itemTransform);
        item.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(true);
    }

    void Update()
    {
        float amount = Time.deltaTime * _rotationSpeed;
        _itemTransform.Rotate(0, amount, 0);
    }
}