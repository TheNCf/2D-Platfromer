using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteractor : ItemInteractor
{
    [field: SerializeField] public int Value { get; private set; } = 1;
}
