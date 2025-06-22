using UnityEngine;

public class FollowRotationUpdate : MonoBehaviour
{
    [SerializeField] private Transform transformToFollow;

    void LateUpdate()
    {
        transform.rotation = transformToFollow.rotation;
    }
}
