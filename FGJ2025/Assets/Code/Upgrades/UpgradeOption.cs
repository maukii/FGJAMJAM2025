using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] float hoverScale = 1.1f;
    [SerializeField] float scaleDuration = 0.2f;


    public event Action UpgradeOptionSelected;

    bool allowInput = false;


    public void ToggleAllowInput(bool state) => allowInput = state;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!allowInput) return;

        transform.DOScale(hoverScale, scaleDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!allowInput) return;

        UpgradeOptionSelected?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!allowInput) return;

        transform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack);
    }
}
