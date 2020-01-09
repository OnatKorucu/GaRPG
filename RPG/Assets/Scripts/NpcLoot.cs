using System;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class NpcLoot : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs = null;
    
    private Inventory _inventory;
    private EntityStateMachine _entityStateMachine;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();

        _entityStateMachine = GetComponent<EntityStateMachine>();
        _entityStateMachine.OnEntityStateChanged += HandleEntityStateChanged;
    }

    private void Start()
    {
        foreach (Item itemPrefab in _itemPrefabs)
        {
            var itemInstance = Instantiate(itemPrefab);
            _inventory.Pickup(itemInstance);
        }
    }
    
    private void HandleEntityStateChanged(IState state)
    {
        Debug.Log($"HandleEntityStateChanged {state.GetType()}");
        if (state is Dead)
        {
            DropLoot();
        }
    }

    private void DropLoot()
    {
        foreach (Item item in _inventory.Items)
        {
            item.transform.SetParent(null);
            item.transform.position = transform.position + transform.right;
            item.gameObject.SetActive(true);
        }
        
        _inventory.Items.Clear();
    }

}