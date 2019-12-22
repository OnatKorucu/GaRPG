using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private TMP_Text _text;
    public Item Item { get; private set; }
    
    public bool IsEmpty => Item == null;

    public void SetItem(Item item)
    {
        Item = item;
        _icon.sprite = item.Icon;
    }

    private void OnValidate()
    {
        _text = GetComponentInChildren<TMP_Text>();

        int hotkeyNumber = transform.GetSiblingIndex() + 1;
        _text.SetText(hotkeyNumber.ToString());

        gameObject.name = "Slot" + hotkeyNumber;
    }
}