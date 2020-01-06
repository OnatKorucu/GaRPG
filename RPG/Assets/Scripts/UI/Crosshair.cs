using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage = null;
    [SerializeField] private Sprite _invalidSprite = null;
    
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
        if (item != null && item.CrosshairDefinition != null)
        {
            _crosshairImage.sprite = item.CrosshairDefinition.sprite;
            Debug.Log($"Crosshair detected {item.CrosshairDefinition}");
        }
        else
        {
            _crosshairImage.sprite = _invalidSprite;
        }
    }
}