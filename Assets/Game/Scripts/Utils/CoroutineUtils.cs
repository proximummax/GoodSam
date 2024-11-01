using System;
using System.Collections;
using UnityEngine;

public static class CoroutineUtils
{
    public static IEnumerator CallWithDelay(float delay, Action<bool> action, bool value)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke(value);
    }
}