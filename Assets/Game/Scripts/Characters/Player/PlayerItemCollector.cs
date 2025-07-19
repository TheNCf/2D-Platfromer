using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private Collider2D _collectorCollider;
    [SerializeField] private LayerMask _itemLayerMask;
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerHealth _playerHealth;
    [field: Space(10)]
    [field: SerializeField] public float ItemPickUpTime { get; private set; } = 0.3f;

    public bool CollectItem()
    {
        Bounds bounds = _collectorCollider.bounds;
        float angle = 0.0f;
        Collider2D itemCollider = Physics2D.OverlapBox(bounds.center, bounds.size, angle, _itemLayerMask);

        if (itemCollider)
        {
            if (itemCollider.TryGetComponent(out Item item))
            {
                switch (item.Type)
                {
                    case ItemType.Coin:
                        AddCoins(item as Coin);
                        break;
                    case ItemType.Medicine:
                        AddHealth(item as Medicine);
                        break;
                }

                return true;
            }
        }

        return false;
    }

    private void AddCoins(Coin coin)
    {
        _playerWallet.Add(coin.Take());
    }

    private void AddHealth(Medicine medicine)
    {
        _playerHealth.Heal(medicine.Take());
    }
}
