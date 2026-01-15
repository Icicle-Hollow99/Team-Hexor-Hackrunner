using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform pivot;
    public Vector3 offset = new Vector3(0, 0, -6);
    public float smoothSpeed = 8f;

    void LateUpdate()
    {
        if (!pivot) return;

        Vector3 desiredPos = pivot.position + pivot.rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
