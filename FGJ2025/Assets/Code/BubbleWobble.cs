using UnityEngine;

public class BubbleWobble : MonoBehaviour
{
    [SerializeField] float wobbleIntensity = 0.1f;
    [SerializeField] float wobbleSpeed = 2f;

    Vector3 initialLocalPosition;


    void Awake()
    {
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        float wobbleX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleIntensity;
        float wobbleY = Mathf.Sin(Time.time * wobbleSpeed * 1.5f) * wobbleIntensity;
        float wobbleZ = Mathf.Sin(Time.time * wobbleSpeed * 0.8f) * wobbleIntensity;

        transform.localPosition = initialLocalPosition + new Vector3(wobbleX, wobbleY, wobbleZ);
    }
}
