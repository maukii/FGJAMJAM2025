using UnityEngine;

public class ExperienceDrop : MonoBehaviour
{
    PlayerExperience playerExperience;

    Transform target;
    Vector3 initialPos;
    bool homing = false;
    [SerializeField] AnimationCurve lerpCurve;

    float lerpT = 0f;
    [SerializeField] float lerpDuration = .5f;

    public int ExpValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HomeIn(other.transform);
            playerExperience = other.GetComponent<PlayerExperience>();
            homing = true;
        }
    }

    void HomeIn(Transform t)
    {
        target = t;
        initialPos = transform.position;
    }

    void Update()
    {
        if(homing)
        {
            transform.position = Vector3.Lerp(initialPos, target.position, lerpCurve.Evaluate(lerpT));
            lerpT += Time.deltaTime / lerpDuration;
            if(lerpT >= 1f)
            {
                // Reached target
                Destroy(gameObject);
                // todo: add expValue
                playerExperience.AddExperience(ExpValue);
            }
        }
    }
}
