using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutineStarter
{
    public Coroutine StartCoroutine(IEnumerator routine);
    public void StopCoroutine(IEnumerator routine);
}
