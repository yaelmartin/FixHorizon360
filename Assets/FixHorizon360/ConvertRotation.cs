using UnityEngine;
using System;

public static class ConvertRotation
{
    [Serializable]
    public struct UptaleRotation
    {
        public float verticalAxis; // Y
        public float horizontalAxis; // X
        public float frontalAxis; // Z
    }

    public static Vector3 GetUnityDeltaEuler(Transform referenceZero, Transform panoPivot)
    {
        Quaternion deltaUnity = Quaternion.Inverse(referenceZero.rotation) * panoPivot.rotation;
        return NormalizeEuler(deltaUnity.eulerAngles);
    }
    public static UptaleRotation GetUptaleDeltaEuler(Transform referenceZero, Transform panoPivot)
    {
        // Apply Uptale compensation (rotate 90� around Y)
        /*
        Quaternion uptaleCompensation = Quaternion.Euler(0f, 90f, 0f);
        Quaternion adjustedPanoPivot = uptaleCompensation * panoPivot.rotation;
        */
        Quaternion adjustedPanoPivot = panoPivot.rotation;

        // Calculate the delta between reference and adjusted pano pivot
        Quaternion deltaUptale = Quaternion.Inverse(referenceZero.rotation) * adjustedPanoPivot;
        Vector3 uptaleUnityEuler = NormalizeEuler(deltaUptale.eulerAngles);

        // Convert Unity's XYZ to Uptale format
        Vector3 uptaleEuler = ToUptaleEuler(uptaleUnityEuler);

        return new UptaleRotation
        {
            horizontalAxis = uptaleEuler.x,
            verticalAxis = uptaleEuler.y,
            frontalAxis = uptaleEuler.z
        };
    }


    private static Vector3 NormalizeEuler(Vector3 euler)
    {
        return new Vector3(
            NormalizeAngle(euler.x),
            NormalizeAngle(euler.y),
            NormalizeAngle(euler.z)
        );
    }

    private static float NormalizeAngle(float angle)
    {
        return (angle > 180f) ? angle - 360f : angle;
    }

    private static Vector3 ToUptaleEuler(Vector3 unityEuler)
    {
        return new Vector3(
            -unityEuler.x,
            -unityEuler.y,
            unityEuler.z
        );
    }
}
