using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private List<BackgroundLayer> _backgroundLayers;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _heightOffset;

    private void Update()
    {
        CalculatePositions();
    }

    private void CalculatePositions()
    {
        foreach (var layer in _backgroundLayers)
        {
            float horizontalDistance = _camera.transform.position.x * layer.Displacement;

            int multiplier = Mathf.FloorToInt(_camera.transform.position.x * layer.Displacement / layer.Size.x);
            horizontalDistance -= multiplier * layer.Size.x;

            Vector2 position = _camera.transform.position + Vector3.up * _heightOffset - Vector3.right * horizontalDistance;
            layer.transform.position = position;
        }
    }
}
