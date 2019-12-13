using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
    
    [SerializeField] private Sprite _staffSprite;
    [SerializeField] private Sprite _invalidSprite;
    
    private Inventory _inventory;
    

    private void OnEnable()
    {
        _inventory = FindObjectOfType <Inventory>();
        _inventory.ActiveItemChanged += HandleActiveItemChanged;

        if (_inventory.ActiveItem != null)
        {
            HandleActiveItemChanged(_inventory.ActiveItem);
        }
        else
        {
            _crosshairImage.sprite = _invalidSprite;
        }
    }

    private void OnValidate()
    {
        _crosshairImage = GetComponent<Image>();
    }

    private void HandleActiveItemChanged(Item item)
    {
        switch (item.CrosshairMode)
        {
            case CrosshairMode.Staff: 
                _crosshairImage.sprite = _staffSprite;
                break;
            
            case CrosshairMode.Invalid: 
                _crosshairImage.sprite = _invalidSprite;
                break;
                
        }
        Debug.Log($"Crosshair detected {item.CrosshairMode}");
    }
}

public enum CrosshairMode
{
    Invalid, 
    Staff
}
