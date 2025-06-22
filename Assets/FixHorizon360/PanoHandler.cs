using System;
using UnityEngine;

public class PanoHandler : MonoBehaviour
{
    [SerializeField] private Transform mainPanoPivot;
    [SerializeField] private RotatingPivots rotatingPivots;
    [SerializeField] private ComputerUIControls computerUIControls;
    [SerializeField] private VRUI vrUi;
    [SerializeField] private Transform parentWhenFree;
    [SerializeField] private Transform referenceZero;
    [SerializeField] private GameObject visualIndicatorAttached;

    private bool _isAttached;

    public RotatingPivots RotatingPivots => rotatingPivots;
    public ComputerUIControls ComputerUIControls => computerUIControls;
    public VRUI VRUI => vrUi;
    public Transform MainPanoPivot => mainPanoPivot;
    public Transform ReferenceZero => referenceZero;

    private void Start()
    {
        Attached(true);
    }

    public void SwitchState()
    {
        _isAttached = !_isAttached;
        ChangeAttachPoint();
    }

    public void Attached(bool status)
    {
        _isAttached = status;
        ChangeAttachPoint();
    }

    public void ChangeAttachPoint()
    {
        visualIndicatorAttached.SetActive(_isAttached);
        mainPanoPivot.SetParent(_isAttached ? rotatingPivots.currentPivotTransform : parentWhenFree);
        mainPanoPivot.localPosition = Vector3.zero;
    }
    
    public void RotateYaw(float degrees)
    {
        // Get the reference zero's up axis in world space
        Vector3 worldYawAxis = referenceZero.up;

        // Rotate around reference zero's position and up axis
        mainPanoPivot.RotateAround(referenceZero.position, worldYawAxis, degrees);
    }


}
