using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private PlayerCoins _playerCoins;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemInteractor item))
        {
            switch (item.ItemType)
            {
                case ItemType.Coin:
                    AddCoins(item as CoinInteractor);
                    break;
            }
        }
    }

    private void AddCoins(CoinInteractor coinInteractor)
    {
        _playerCoins.Add(coinInteractor.Take());
    }
}
