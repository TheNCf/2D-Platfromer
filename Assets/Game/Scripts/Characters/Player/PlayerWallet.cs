using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int _coins = 0;

    public Action<int> CoinsChanged;

    private int Coins
    {
        get
        {
            return _coins;
        }

        set
        {
            _coins = value;
            CoinsChanged?.Invoke(_coins);
        }
    }

    public bool TryRemove(int amount)
    {
        if (amount > Coins)
            return false;

        Coins -= amount;
        return true;
    }

    public void Add(int amount)
    {
        Coins += amount;
    }
}
