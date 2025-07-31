using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class Vector3Extender
{
    public static bool IsCloseToTarget(this Vector3 current, Vector3 target, float sqrCloseDistance)
    {
        float sqrDistance = GetSqrDistance(current, target);
        return sqrDistance < sqrCloseDistance;
    }

    public static float GetSqrDistance(this Vector3 position, Vector3 target)
    {
        return (target - position).sqrMagnitude;
    }
}
