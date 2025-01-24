using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Assign this in editor")]
    public Transform Target;
    public float SmoothTime = .1f;
    Vector3 offset;
    Vector3 targetPos;
    Vector3 cVel;

    void Start()
    {
        offset = Target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        targetPos = Target.transform.position - offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref cVel, SmoothTime);
    }
}
