using System;
using UnityEngine;

public class ItemRaycaster : ItemComponent
{
    [SerializeField] private float delay = 0.2f;
    [SerializeField] private float _range = 20f;
    
    private RaycastHit[] _results = new RaycastHit[100];
    private int _layermask;

    private void Awake()
    {
        _layermask = LayerMask.GetMask("Default"); // can only be called in Awake() or...?
    }

    public override void Use()
    {
        _nextUseTime = Time.time + delay;

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
            Transform hitCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            hitCube.localScale = Vector3.one * 0.1f;
            hitCube.position = nearest.point;
        }
            
    }
}