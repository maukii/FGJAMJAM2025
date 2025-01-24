using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Assign this in editor")]
    public Transform Target;
    Vector3 _offset;

    void Start()
    {
        _offset = Target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        transform.position = Target.transform.position - _offset;
    }
}
