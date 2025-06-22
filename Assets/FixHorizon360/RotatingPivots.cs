using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPivots : MonoBehaviour
{
    [SerializeField] private TwoPointsRotation[] axisTwoPointsRotations;
    [SerializeField] private PanoHandler panoHandler;

    private TwoPointsRotation currentPivot;
    private Axis currentAxis;

    public Transform currentPivotTransform => currentPivot.transform;

    private enum Axis
    {
        Pitch, // X
        Yaw,   // Y
        Roll   // Z
    }

    private void ChangeAxis(Axis newAxis)
    {
        // Disable all previous pivots
        foreach (var axis in axisTwoPointsRotations)
        {
            axis.gameObject.SetActive(false);
        }

        currentAxis = newAxis;
        currentPivot = axisTwoPointsRotations[(int)currentAxis];

        if (currentPivot != null)
        {
            currentPivot.gameObject.SetActive(true);
            // The pivot was not updated as the gameobject was not active.
            // It is required to update it before attaching the pano.
            currentPivot.UpdatePivotFromTwoPoints();
            Debug.Log($"Changed to axis: {currentAxis}");
        }
        else
        {
            Debug.LogWarning($"No pivot assigned for axis {currentAxis}");
        }
        
        //if pano meant to be attached, update the parent
        panoHandler.ChangeAttachPoint();
    }

    void Awake()
    {
        if (axisTwoPointsRotations.Length != 3)
        {
            Debug.LogError("RotatingPivots: Please assign exactly 3 TwoPointsRotation objects for Pitch, Yaw, Roll (in that order).");
            return;
        }

        // Hide all and set default axis
        foreach (var axis in axisTwoPointsRotations)
        {
            axis.gameObject.SetActive(false);
        }

        ChangeAxis(Axis.Roll); // Default
    }
    
    public void IncrementRotation(float degrees)
    {
        if (currentPivot != null)
        {
            currentPivot.AddRotationOffset(degrees);
            currentPivot.UpdatePivotFromTwoPoints();
        }
    }

    public void SetPitch() => ChangeAxis(Axis.Pitch);
    public void SetYaw() => ChangeAxis(Axis.Yaw);
    public void SetRoll() => ChangeAxis(Axis.Roll);
}