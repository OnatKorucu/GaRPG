using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private AssetReference _lootItemHolderPrefab = null;
    private static LootSystem _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        
    }

    public static void Drop(Item item, Transform droppingTransform)
    {
        _instance.StartCoroutine(_instance.DropAsync(item, droppingTransform));
        
    }

    private IEnumerator DropAsync(Item item, Transform droppingTransform)
    {
        var operationHandle = _lootItemHolderPrefab.InstantiateAsync();
        yield return operationHandle;
        
        var lootItemHolder = operationHandle.Result.GetComponent<LootItemHolder>();
        lootItemHolder.TakeItem(item);

        Vector2 randomCirclePoint = Random.insideUnitCircle * 2f;
        Vector3 randomPosition = droppingTransform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

        lootItemHolder.transform.position = randomPosition;
    }
    
}