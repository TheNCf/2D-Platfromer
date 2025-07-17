using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerCoins;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            switch (item.ItemType)
            {
                case ItemType.Coin:
                    AddCoins(item as Coin);
                    break;
            }
        }
    }

    private void AddCoins(Coin coinInteractor)
    {
        _playerCoins.Add(coinInteractor.Take());
    }
}
