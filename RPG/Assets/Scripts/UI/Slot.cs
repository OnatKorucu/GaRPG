using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Item _item;

    [SerializeField] private Image _icon;
    
    public bool IsEmpty => _item == null;

    public void SetItem(Item item)
    {
        _item = item;
        _icon.sprite = item.Icon;
    }
}