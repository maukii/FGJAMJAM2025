using UnityEngine;
using DG.Tweening;
using System;

public class UpgradesHandler : MonoBehaviour
{
    [SerializeField] Canvas upgradesCanvas;
    [SerializeField] PlayerExperience playerExperience;
    [SerializeField] CanvasGroup upgradesUICanvasGroup;
    [SerializeField] CanvasGroup upgradesAreaCanvasGroup;
    [SerializeField] Transform upgradersArea;
    [SerializeField] UpgradeOption[] upgradeOptions = new UpgradeOption[3];


    void OnEnable()
    {
        playerExperience.OnPlayerLeveledUp += ShowUpgradesUI;

        foreach (var option in upgradeOptions)
            option.UpgradeOptionSelected += OnUpgradeOptionSelected;
    }

    void OnDisable()
    {
        playerExperience.OnPlayerLeveledUp -= ShowUpgradesUI;

        foreach (var option in upgradeOptions)
            option.UpgradeOptionSelected -= OnUpgradeOptionSelected;
    }

    void Start() => HideUpgradesUI();

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ShowUpgradesUI();

        if (Input.GetKeyDown(KeyCode.Q))
            HideUpgradesUI();
    }
#endif

    void ShowUpgradesUI()
    {
        upgradesCanvas.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(upgradesUICanvasGroup.DOFade(1, 0.15f));
        sequence.Append(upgradesAreaCanvasGroup.DOFade(1, 0.25f));
        sequence.Append(upgradersArea.DOScale(1, 0.25f).SetEase(Ease.OutBack));

        sequence.Append(upgradeOptions[0].transform.DOScale(1, 0.25f).SetEase(Ease.OutBack));
        sequence.Join(upgradeOptions[1].transform.DOScale(1, 0.25f).SetEase(Ease.OutBack).SetDelay(0.15f));
        sequence.Join(upgradeOptions[2].transform.DOScale(1, 0.25f).SetEase(Ease.OutBack).SetDelay(0.25f));

        sequence.Play().OnComplete(() => 
        {
            foreach (var upgradeOption in upgradeOptions)
            {
                upgradeOption.ToggleAllowInput(true);
            }
        });
    }

    void HideUpgradesUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(upgradersArea.DOScale(0, 0.25f).SetEase(Ease.InBack));
        sequence.Append(upgradesUICanvasGroup.DOFade(0, 0.25f));

        sequence.Play().OnComplete(() => 
        {
            upgradesUICanvasGroup.alpha = 0;
            upgradesAreaCanvasGroup.alpha = 0;
            upgradersArea.localScale = Vector3.zero;
            foreach (var option in upgradeOptions)
            {
                option.transform.localScale = Vector3.zero;
            }

            upgradesCanvas.gameObject.SetActive(false);
        });
    }

    void OnUpgradeOptionSelected()
    {
        foreach (var upgradeOption in upgradeOptions)
        {
            upgradeOption.ToggleAllowInput(false);
        }

        GrantUpgrade();
        HideUpgradesUI();
    }

    void GrantUpgrade()
    {
        // TODO::
        throw new NotImplementedException();
    }
}
