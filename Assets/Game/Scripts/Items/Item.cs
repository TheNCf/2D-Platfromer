using System;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    [SerializeField] private int _value = 1;

    public event Action Taken;

    [field: SerializeField] public ItemType Type { get; private set; }

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
