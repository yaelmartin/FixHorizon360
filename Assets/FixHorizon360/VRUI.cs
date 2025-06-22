using System.Collections;
using UnityEngine;

public class VRUI : MonoBehaviour
{
    [SerializeField] private GameObject[] VRUIObjects;
    [SerializeField] private Transform XROrigin;
    [SerializeField] private Transform head;
    [SerializeField] private Transform parentFixHorizon360;

    private bool _isVisible;

    private void Start()
    {
        ToggleVRUI(true);
    }

    public void SwitchState()
    {
        ToggleVRUI(!_isVisible);
    }

    public void ToggleVRUI(bool status)
    {
        _isVisible = status;

        foreach (var obj in VRUIObjects)
        {
            obj.SetActive(_isVisible);
        }
    }

    public void Recenter()
    {
        if (head == null || XROrigin == null || parentFixHorizon360 == null)
        {
            Debug.LogWarning("VRUI: Head, XROrigin, or ParentFixHorizon360 is not assigned!");
            return;
        }

        // Define the offsets
        Vector3 offsetFixHorizon = new Vector3(0f, -0.4f, 0.45f);

        // === STEP 1: Rotate XR Origin to align head forward ===

        float headYawWorld = head.eulerAngles.y; // Head's current world yaw
        Vector3 originEuler = XROrigin.rotation.eulerAngles;
        XROrigin.rotation = Quaternion.Euler(0f, originEuler.y - headYawWorld, 0f);
        // Now the head is facing world forward

        // === STEP 2: Keep XR Origin Y position, adjust only X and Z ===

        Vector3 worldXROrigin = XROrigin.position;
        Vector3 worldHMD = head.position;
        Vector3 roomscaleOffset = worldXROrigin - worldHMD;

        // Flatten the head forward to XZ plane
        Vector3 flatForward = head.forward;
        flatForward.y = 0f;
        flatForward.Normalize();

        // Calculate offset move
        Vector3 adjustedOffset = flatForward * offsetFixHorizon.z + Vector3.up * offsetFixHorizon.y;

        // New position for XR Origin: X and Z adjusted, Y stays the same
        Vector3 newOriginPos = (head.position + adjustedOffset) + roomscaleOffset;
        XROrigin.position = new Vector3(newOriginPos.x, worldXROrigin.y, newOriginPos.z);

        // === STEP 3: Move FixHorizon360 Parent ===

        parentFixHorizon360.position = head.position + adjustedOffset;
    }






}
