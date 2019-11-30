using UnityEngine;

public class ItemLogger : ItemComponent
{
    protected override void Use()
    {
        Debug.Log("Item was used...");
    }
}