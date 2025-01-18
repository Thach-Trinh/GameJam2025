using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ComponentExtensionMethod
{
    public static float GetDistanceOnXYPlane(this Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
    }
}
