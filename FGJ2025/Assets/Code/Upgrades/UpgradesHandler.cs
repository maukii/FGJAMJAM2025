using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public enum UpgradeType
{
    Damage,
    AttackRate,
    PlayerMovementSpeed,
    Heal,
}

public class UpgradesHandler : MonoBehaviour
{
    public static UpgradesHandler Instance;

    [SerializeField] AudioClip upgradeChosenAudio;
    [SerializeField] List<UpgradeData> allUpgrades = new List<UpgradeData>();
    [SerializeField] Canvas upgradesCanvas;
    [SerializeField] PlayerExperience playerExperience;
    [SerializeField] CanvasGroup upgradesUICanvasGroup;
    [SerializeField] CanvasGroup upgradesAreaCanvasGroup;
    [SerializeField] Transform upgradersArea;
    [SerializeField] UpgradeOption[] upgradeOptions = new UpgradeOption[3];

    Dictionary<UpgradeType, float> activeUpgrades = new Dictionary<UpgradeType, float>();


    void Awake() => Instance = this;

    void OnEnable()
    {
        playerExperience.OnPlayerLeveledUp += OnPlayerLeveledUp;

        foreach (var option in upgradeOptions)
            option.UpgradeOptionSelected += OnUpgradeOptionSelected;
    }

    void OnDisable()
    {
        playerExperience.OnPlayerLeveledUp -= OnPlayerLeveledUp;

        foreach (var option in upgradeOptions)
            option.UpgradeOptionSelected -= OnUpgradeOptionSelected;
    }

    void Start() => HideUpgradesUI();

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnPlayerLeveledUp();
    }
#endif
    
    void OnPlayerLeveledUp()
    {
        GameStateManager.Instance.SetGameState(GameState.LevelingUp);

        UpgradeData[] selectableOptions = GetRandomUpgrades();

        for (int i = 0; i < upgradeOptions.Length; i++)
            upgradeOptions[i].SetUpgradeData(selectableOptions[i]);

        Time.timeScale = 0f;
        ShowUpgradesUI();
    }

    UpgradeData[] GetRandomUpgrades()
    {
        List<UpgradeData> remainingUpgrades = new List<UpgradeData>(allUpgrades);
        UpgradeData[] randomUpgrades = new UpgradeData[3];

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, remainingUpgrades.Count);
            randomUpgrades[i] = remainingUpgrades[randomIndex];
            remainingUpgrades.RemoveAt(randomIndex);
        }

        return randomUpgrades;
    }

    void ShowUpgradesUI()
    {
        upgradesCanvas.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence().SetUpdate(true);

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
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

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

        Time.timeScale = 1f;
        AudioManager.Instance.PlaySound(upgradeChosenAudio);
        GameStateManager.Instance.SetGameState(GameState.Playing);
    }

    void OnUpgradeOptionSelected(UpgradeData upgradeData)
    {
        foreach (var upgradeOption in upgradeOptions)
        {
            upgradeOption.ToggleAllowInput(false);
        }

        ApplyUpgrade(upgradeData);
        HideUpgradesUI();
    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        if (upgrade.applyOnce)
        {
            switch (upgrade.upgradeType)
            {
                case UpgradeType.Damage:
                case UpgradeType.AttackRate:
                case UpgradeType.PlayerMovementSpeed:
                    Debug.LogWarning("Invalid upgrade choise for applyOnce");
                    return;
                case UpgradeType.Heal:
                    PlayerController.Instance.GetComponent<Health>().SetFullHealth();
                    break;
            }

            return;
        }

        if (!activeUpgrades.ContainsKey(upgrade.upgradeType))
            activeUpgrades[upgrade.upgradeType] = 0f;
        
        activeUpgrades[upgrade.upgradeType] += upgrade.value;
    }

    public float GetUpgradeValue(UpgradeType type) => activeUpgrades.ContainsKey(type) ? activeUpgrades[type] : 0f;
}
