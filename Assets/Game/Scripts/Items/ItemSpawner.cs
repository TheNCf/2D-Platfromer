using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Item> _itemPrefabs;
    [SerializeField] private List<EdgeCollider2D> _spawnEdges;
    [SerializeField] private LayerMask _spawnLayerMask;

    private Item _spawnedItem;

    private void Awake()
    {
        Spawn();
    }

    private void OnTaken()
    {
        _spawnedItem.Taken -= OnTaken;
        Destroy(_spawnedItem.gameObject);
        Spawn();
    }

    private void Spawn()
    {
        int itemIndex = Random.Range(0, _itemPrefabs.Count);
        _spawnedItem = Instantiate(_itemPrefabs[itemIndex]);
        RaycastHit2D hitInfo = new RaycastHit2D();

        while (hitInfo == false)
        {
            int edgeIndex = Random.Range(0, _spawnEdges.Count);
            EdgeCollider2D edge = _spawnEdges[edgeIndex];
            float minSpawnX = edge.bounds.center.x - edge.bounds.extents.x;
            float maxSpawnX = edge.bounds.center.x + edge.bounds.extents.x;
            float spawnX = Random.Range(minSpawnX, maxSpawnX);
            Vector2 spawnPosition = new Vector2(spawnX, edge.bounds.max.y);

            hitInfo = Physics2D.Raycast(spawnPosition, Vector2.down, float.MaxValue, _spawnLayerMask);
        }
        
        _spawnedItem.transform.position = hitInfo.point;
        _spawnedItem.Taken += OnTaken;
    }
}
