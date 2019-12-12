using System;
using UnityEngine;

public abstract class ItemComponent : MonoBehaviour
{
    public bool CanUse => Time.time >= nextUseTime;
    
    protected float nextUseTime;

    public abstract void Use();

    

}