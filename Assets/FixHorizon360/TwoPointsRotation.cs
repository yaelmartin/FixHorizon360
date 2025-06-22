using UnityEngine;

public class TwoPointsRotation : MonoBehaviour
{
    [SerializeField] private PanoHandler panoHandler;
    [SerializeField] private Transform pivot1;
    [SerializeField] private Transform pivot2;
    
    [SerializeField] private Vector3 axisDirection = Vector3.up; // Set to (0,1,0) for Yaw, etc.
    
    private float _rotationOffsetDegrees = 0f;
    


    // Update is called once per frame
    void Update()
    {
        UpdatePivotFromTwoPoints();
    }

    public void UpdatePivotFromTwoPoints()
    {
        // Step 1: Base look direction
        Vector3 forward = (pivot2.position - pivot1.position).normalized;
        Vector3 referenceUp = panoHandler.ReferenceZero.up;
        Quaternion baseRotation = Quaternion.LookRotation(forward, referenceUp);

        // Step 2: Apply offset around a chosen axis from ReferenceZero's space
        Vector3 worldAxis = panoHandler.ReferenceZero.TransformDirection(axisDirection.normalized);
        Quaternion offsetRotation = Quaternion.AngleAxis(_rotationOffsetDegrees, worldAxis);

        transform.rotation = offsetRotation * baseRotation;
    }
    
    
    public void SetRotationOffset(float offsetDegrees)
    {
        _rotationOffsetDegrees = offsetDegrees;
    }

    public void AddRotationOffset(float deltaDegrees)
    {
        _rotationOffsetDegrees += deltaDegrees;
    }


}
