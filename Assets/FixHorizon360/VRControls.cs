using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRControls : MonoBehaviour
{
    [SerializeField] private InputActionReference toggleAttachButton;
    [SerializeField] private InputActionReference pitchAxis;
    [SerializeField] private InputActionReference yawAxis;
    [SerializeField] private InputActionReference rollAxis;
    [SerializeField] private InputActionReference rotate90CW;
    [SerializeField] private InputActionReference rotate90CCW;
    [SerializeField] private InputActionReference incrementRotationPositive;
    [SerializeField] private InputActionReference incrementRotationNegative;
    [SerializeField] private InputActionReference toggleVRUI;
    [SerializeField] private InputActionReference vrRecenter;


    [SerializeField] private PanoHandler panoHandler;

    private void Awake()
    {
        toggleAttachButton.action.started += SwitchState;
        pitchAxis.action.started += Pitch;
        yawAxis.action.started += Yaw;
        rollAxis.action.started += Roll;
        rotate90CW.action.started += Rotate90CW;
        rotate90CCW.action.started += Rotate90CCW;
        incrementRotationPositive.action.started += IncrementRotationPositive;
        incrementRotationNegative.action.started += IncrementRotationNegative;
        toggleVRUI.action.started += ToggleVRUI;
        vrRecenter.action.started += VRRecenter;
    }
    private void OnDestroy()
    {
        toggleAttachButton.action.started -= SwitchState;
        pitchAxis.action.started -= Pitch;
        yawAxis.action.started -= Yaw;
        rollAxis.action.started -= Roll;
        rotate90CW.action.started -= Rotate90CW;
        rotate90CCW.action.started -= Rotate90CCW;
        incrementRotationPositive.action.started -= IncrementRotationPositive;
        incrementRotationNegative.action.started -= IncrementRotationNegative;
        toggleVRUI.action.started -= ToggleVRUI;
        vrRecenter.action.started -= VRRecenter;
    }

    private void SwitchState(InputAction.CallbackContext context)
    {
        panoHandler.SwitchState();
    }
    
    private void Pitch(InputAction.CallbackContext context)
    {
        panoHandler.RotatingPivots.SetPitch();
    }
    private void Yaw(InputAction.CallbackContext context)
    {
        panoHandler.RotatingPivots.SetYaw();
    }
    private void Roll(InputAction.CallbackContext context)
    {
        panoHandler.RotatingPivots.SetRoll();
    }

    private void Rotate90CW(InputAction.CallbackContext context)
    {
        panoHandler.RotateYaw(90);
    }

    private void Rotate90CCW(InputAction.CallbackContext context)
    {
        panoHandler.RotateYaw(-90);
    }

    private void IncrementRotationPositive(InputAction.CallbackContext context)
    {
        panoHandler.ComputerUIControls.IncrementRotation(true);
    }
    private void IncrementRotationNegative(InputAction.CallbackContext context)
    {
        panoHandler.ComputerUIControls.IncrementRotation(false);
    }

    private void ToggleVRUI(InputAction.CallbackContext context)
    {
        panoHandler.VRUI.SwitchState();
    }

    private void VRRecenter(InputAction.CallbackContext context)
    {
        panoHandler.VRUI.Recenter();
    }
}
