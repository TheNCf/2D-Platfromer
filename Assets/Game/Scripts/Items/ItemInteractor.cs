using UnityEngine;

public class ItemInteractor : MonoBehaviour, IItem
{
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [SerializeField] private int _value = 1;

    public int Take()
    {
        Destroy(gameObject);
        return _value;
    }
}

public enum ItemType
{
    Coin,
    Medicine
}
