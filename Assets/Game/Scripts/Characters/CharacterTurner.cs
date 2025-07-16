using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurner : MonoBehaviour
{
    private readonly Vector3 _angleRight = new Vector3(0, 0, 0);
    private readonly Vector3 _angleLeft = new Vector3(0, 180, 0);

    public void Turn(bool isFacingRight)
    {
        if (isFacingRight)
            transform.localEulerAngles = _angleRight;
        else
            transform.localEulerAngles = _angleLeft;
    }
}
