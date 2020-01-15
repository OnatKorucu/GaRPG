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
    
    private void HandleEntityStateChanged(IState state, IState previousState)
    {
        Debug.Log($"HandleEntityStateChanged to {state.GetType()} from {previousState.GetType()}");
        
        if (state is Dead)
        {
            DropLoot();
        }
    }

    private void DropLoot()
    {
        foreach (Item item in _inventory.Items)
        {
            LootSystem.Drop(item, transform);
            // var lootItemHolder = FindObjectOfType<LootItemHolder>();
            // lootItemHolder.TakeItem(item);
        }
        
        _inventory.Items.Clear();
    }
}