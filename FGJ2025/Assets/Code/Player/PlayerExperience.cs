using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public event Action OnPlayerLeveledUp;


    void LevelUp()
    {
        OnPlayerLeveledUp?.Invoke();
    }
}
