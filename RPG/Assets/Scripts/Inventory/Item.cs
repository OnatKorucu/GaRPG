using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private UseAction[] _actions = new UseAction[0];
    [SerializeField] private CrosshairDefinition _crosshairDefinition;

    [SerializeField] private Sprite _icon;
 
    private bool _wasPickedUp;
    
    public UseAction[] Actions
    {
        get => _actions;
        set => _actions = value;
    }

    public CrosshairDefinition CrosshairDefinition
    {
        get => _crosshairDefinition;
        set => _crosshairDefinition = value;
    }

    public Sprite Icon => _icon;


    private void OnTriggerEnter(Collider other)
    {
        if (_wasPickedUp)
            return;
        
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Pickup(this);
            _wasPickedUp = true;
        }
    }

    private void OnValidate()
    {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
}