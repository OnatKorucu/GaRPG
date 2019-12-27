using System;
using UnityEngine;

public class ItemRaycaster : ItemComponent
{
    [SerializeField] private float delay = 0.2f;
    [SerializeField] private float _range = 20f;
    [SerializeField] private int _damage = 1;
    
    private RaycastHit[] _results = new RaycastHit[100];
    private int _layermask;
    

    private void Awake()
    {
        _layermask = LayerMask.GetMask("Default"); // can only be called in Awake() or...?
    }

    public override void Use()
    {
        nextUseTime = Time.time + delay;
        Debug.Log("Using the Use() method...");

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        int hits = Physics.RaycastNonAlloc(ray, _results, _range, _layermask, QueryTriggerInteraction.Collide);

        RaycastHit nearest = new RaycastHit();
        double nearestDistance = Double.MaxValue;
        for (int i = 0; i < hits; i++)
        {
            var distance = Vector3.Distance(transform.position, _results[i].point);
            if (distance < nearestDistance)
            {
                nearest = _results[i];
                nearestDistance = distance;
            }
        }

        if (nearest.transform != null)
        {
            var takeHits = nearest.collider.GetComponent<ITakeHits>();
            takeHits?.TakeHit(_damage);
        }
            
    }
}