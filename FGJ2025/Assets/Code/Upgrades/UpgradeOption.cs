using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float hoverScale = 1.25f;
    [SerializeField] float scaleDuration = 0.25f;

    [SerializeField] TextMeshProUGUI upgradeNameLabel;
    [SerializeField] TextMeshProUGUI upgradeDescriptionLabel;
    [SerializeField] Image upgradeBackground;


    public event Action<UpgradeData> UpgradeOptionSelected;

    bool allowInput = false;
    UpgradeData upgradeData;


    public void SetUpgradeData(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;     
        
        upgradeNameLabel.SetText(upgradeData.upgradeName);
        upgradeDescriptionLabel.SetText(upgradeData.description);
        upgradeBackground.sprite = upgradeData.icon;
    }

    public void ToggleAllowInput(bool state) => allowInput = state;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!allowInput) return;

        transform.DOScale(hoverScale, scaleDuration).SetEase(Ease.InOutBounce).SetUpdate(true);
    }

    public void OnButtonClicked()
    {
        if (!allowInput) return;

        UpgradeOptionSelected?.Invoke(upgradeData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!allowInput) return;

        transform.DOScale(1f, scaleDuration).SetEase(Ease.InOutBounce).SetUpdate(true);
    }
}
