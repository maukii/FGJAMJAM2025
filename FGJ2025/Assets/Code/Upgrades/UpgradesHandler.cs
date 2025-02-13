using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public enum UpgradeType
{
    Damage,
    AttackRate,
    PlayerMovementSpeed,
    Heal,
    ProjectileSpeed,
    ProjectileRange,
    ProjectileCount,
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
    }

    void OnDisable()
    {
        playerExperience.OnPlayerLeveledUp -= OnPlayerLeveledUp;
    }

    void Start() => HideUpgradesUI();

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnPlayerLeveledUp();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var debugInfo = new System.Text.StringBuilder("Current Active Upgrades:\n");
            
            foreach (var upgrade in activeUpgrades)
                debugInfo.AppendLine($"Upgrade Type: {upgrade.Key}, Value: {upgrade.Value}");

            Debug.Log(debugInfo.ToString());
        }
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
        GameStateManager.Instance.SetGameState(GameState.Playing);
    }

    public void OnUpgradeOptionSelected(UpgradeData upgradeData)
    {
        foreach (var upgradeOption in upgradeOptions)
        {
            upgradeOption.ToggleAllowInput(false);
        }

        ApplyUpgrade(upgradeData);
        AudioManager.Instance.PlaySound(upgradeChosenAudio);
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
                case UpgradeType.ProjectileSpeed:
                case UpgradeType.ProjectileRange:
                    Debug.LogWarning("Invalid upgrade choise for applyOnce");
                    break;
                case UpgradeType.Heal:
                    PlayerController.Instance.GetComponent<Health>().SetFullHealth();
                    break;
                case UpgradeType.ProjectileCount:
                    for (int i = 0; i < allUpgrades.Count; i++)
                    {
                        UpgradeData upgradeOption = allUpgrades[i];
                        if (upgradeOption.upgradeType == UpgradeType.ProjectileCount)
                            allUpgrades.RemoveAt(i);
                    }
                    
                    activeUpgrades.Add(UpgradeType.ProjectileCount, 1);
                break;
            }

            return;
        }

        if (!activeUpgrades.ContainsKey(upgrade.upgradeType))
            activeUpgrades[upgrade.upgradeType] = 0f;
        
        activeUpgrades[upgrade.upgradeType] += upgrade.value;
    }

    public bool HasUpgrade(UpgradeType type) => activeUpgrades.ContainsKey(type);

    public float GetUpgradeValue(UpgradeType type) => activeUpgrades.ContainsKey(type) ? activeUpgrades[type] : 0f;
}
