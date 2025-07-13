using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Characteristics", menuName = "Character Values/Movement")]
public class MovementCharacteristics : ScriptableObject
{
    [Header("Walking")]
    [SerializeField] public float RunSpeed = 5.0f;
    [SerializeField] public float WalkSpeed = 2.0f;
    [SerializeField] public float GroundedAcceleration = 5.0f;
    [SerializeField] public float GroundedDeceleration = 20.0f;
    [SerializeField] public float AirAcceleration = 2.0f;
    [SerializeField] public float AirDeceleration = 2.0f;

    [Header("Jumping")]
    [SerializeField] public float JumpHeight = 5.0f;
}
