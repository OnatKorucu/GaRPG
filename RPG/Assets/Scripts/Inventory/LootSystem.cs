using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private AssetReference _lootItemHolderPrefab = null;
    private static LootSystem _instance;
    private static Queue<LootItemHolder> _lootItemHolders = new Queue<LootItemHolder>();

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
        if (_lootItemHolders.Any())
        {
            var lootItemHolder = _lootItemHolders.Dequeue();
            lootItemHolder.gameObject.SetActive(true);
            AssignItemToHolder(item, droppingTransform, lootItemHolder);
        }
        else
        {
            _instance.StartCoroutine(_instance.DropAsync(item, droppingTransform));
        }
    }

    private IEnumerator DropAsync(Item item, Transform droppingTransform)
    {
        var operationHandle = _lootItemHolderPrefab.InstantiateAsync();
        yield return operationHandle;
        
        var lootItemHolder = operationHandle.Result.GetComponent<LootItemHolder>();
        AssignItemToHolder(item, droppingTransform, lootItemHolder);
    }

    private static void AssignItemToHolder(Item item, Transform droppingTransform, LootItemHolder lootItemHolder)
    {
        lootItemHolder.TakeItem(item);

        Vector2 randomCirclePoint = Random.insideUnitCircle * 2f;
        Vector3 randomPosition = droppingTransform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

        lootItemHolder.transform.position = randomPosition;
    }

    public static void AddToPool(LootItemHolder lootItemHolder)
    {
        lootItemHolder.gameObject.SetActive(false);
        _lootItemHolders.Enqueue(lootItemHolder);
    }
}