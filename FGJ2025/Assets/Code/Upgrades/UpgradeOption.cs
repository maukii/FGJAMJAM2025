using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] float hoverScale = 1.1f;
    [SerializeField] float scaleDuration = 0.2f;

    [SerializeField] TextMeshProUGUI upgradeNameLabel;
    [SerializeField] TextMeshProUGUI upgradeDescriptionLabel;
    [SerializeField] Image upgradeBackground;
    [SerializeField] Image upgradeIcon;


    public event Action<UpgradeData> UpgradeOptionSelected;

    bool allowInput = false;
    UpgradeData upgradeData;


    public void SetUpgradeData(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;     
        
        upgradeNameLabel.SetText(upgradeData.upgradeName);
        upgradeDescriptionLabel.SetText(upgradeData.description);
        //upgradeBackground
        upgradeIcon.sprite = upgradeData.icon;
    }

    public void ToggleAllowInput(bool state) => allowInput = state;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!allowInput) return;

        transform.DOScale(hoverScale, scaleDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!allowInput) return;

        UpgradeOptionSelected?.Invoke(upgradeData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!allowInput) return;

        transform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack);
    }
}
