using UnityEngine;

[CreateAssetMenu(fileName = "Movement Characteristics", menuName = "Character Values/Movement")]
public class MovementStats : ScriptableObject
{
    [field: Header("Walking")]
    [field: SerializeField] public float RunSpeed { get; private set; } = 5.0f;
    [field: SerializeField] public float WalkSpeed { get; private set; } = 2.0f;
    [field: SerializeField] public float GroundedAcceleration { get; private set; } = 5.0f;
    [field: SerializeField] public float GroundedDeceleration { get; private set; } = 20.0f;
    [field: SerializeField] public float AirAcceleration { get; private set; } = 2.0f;
    [field: SerializeField] public float AirDeceleration { get; private set; } = 2.0f;

    [field: Header("Ground Check")]
    [field: SerializeField] public float GroundBoxHeight { get; private set; } = 0.05f;

    [field: Header("Jumping")]
    [field: SerializeField] public float JumpHeight { get; private set; } = 5.0f;
    [field: SerializeField] public float GroundControlRecoverTime { get; private set; } = 0.5f;

    [field: Header("Dashing")]
    [field: SerializeField] public float DashCooldown { get; private set; } = 3.0f;
    [field: SerializeField] public float DashForce { get; private set; } = 10.0f;
    [field: SerializeField] public float DashDrag { get; private set; } = 1.0f;
    [field: SerializeField] public float DashDragOverrideTime { get; private set; } = 1.0f;
}
