using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private IEnumerator _disableControlsCoroutine;

    public bool CanMove { get; private set; } = true;

    public void DisableControls(float duration)
    {
        if (_disableControlsCoroutine != null)
            StopCoroutine(_disableControlsCoroutine);

        _disableControlsCoroutine = DisableControlsCoroutine(duration);
        StartCoroutine(_disableControlsCoroutine);
    }

    public void EnableControls()
    {
        CanMove = true;
    }

    private IEnumerator DisableControlsCoroutine(float timeInSeconds)
    {
        CanMove = false;
        yield return new WaitForSeconds(timeInSeconds);
        CanMove = true;
    }
}
