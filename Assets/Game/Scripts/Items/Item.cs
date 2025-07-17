using System;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    [SerializeField] private int _value = 1;

    [field: SerializeField] public ItemType ItemType { get; private set; }

    public Action Taken;

    public int Take()
    {
        Taken?.Invoke();
        return _value;
    }
}

public enum ItemType
{
    Coin,
    Medicine
}
