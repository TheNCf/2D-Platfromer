using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private List<BackgroundLayer> _backgroundLayers;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _heightOffset;

    private bool _isValidate = true;

    private void OnValidate()
    {
        _isValidate = true;
        CalculatePositions(_isValidate);
    }

    private void Awake()
    {
        _isValidate = false;
    }

    private void Update()
    {
        CalculatePositions(_isValidate);
    }

    private void CalculatePositions(bool isValidate)
    {
        foreach (var layer in _backgroundLayers)
        {
            float horizontalDistance = _camera.transform.position.x * layer.Displacement;
            Vector2 position = _camera.transform.position + Vector3.up * _heightOffset - Vector3.right * horizontalDistance;
            layer.transform.position = position;
        }
    }
}
