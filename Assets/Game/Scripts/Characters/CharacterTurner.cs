using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurner : MonoBehaviour
{
    [SerializeField] private float _amountOfMovementForTurn;

    private readonly Vector3 _angleRight = new Vector3(0, 0, 0);
    private readonly Vector3 _angleLeft = new Vector3(0, 180, 0);
    private bool _isFacingRight = true;

    public bool IsFacingRight => _isFacingRight;

    public void Turn(Rigidbody2D rigidbody)
    {
        if (CheckFacingRight(rigidbody))
            transform.localEulerAngles = _angleRight;
        else
            transform.localEulerAngles = _angleLeft;
    }

    private bool CheckFacingRight(Rigidbody2D rigidbody)
    {
        if (rigidbody.velocity.x > _amountOfMovementForTurn)
            _isFacingRight = true;
        else if (rigidbody.velocity.x < -_amountOfMovementForTurn)
            _isFacingRight = false;

        return _isFacingRight;
    }
}
