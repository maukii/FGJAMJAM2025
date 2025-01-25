using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ColliderButton : MonoBehaviour
{
    [SerializeField] float animationTime = 0.25f;
    [SerializeField] UnityEvent OnClicked = new UnityEvent();
	MeshRenderer meshRenderer;


    void Awake() => meshRenderer = GetComponent<MeshRenderer>();

    void OnEnable()
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetFloat("_Dissolve", 0f);
        meshRenderer.SetPropertyBlock(propertyBlock);
    }

    void OnMouseDown()
    {
        float pos = 0f;
        DOTween.To(() => pos, x => 
        {
            pos = x;

            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetFloat("_Dissolve", pos);
            meshRenderer.SetPropertyBlock(propertyBlock);

        }, 1f, animationTime).OnComplete(() => OnClicked?.Invoke());
    }
}
