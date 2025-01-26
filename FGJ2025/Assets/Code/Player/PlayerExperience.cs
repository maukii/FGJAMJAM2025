using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public AudioClip ExpGetAudio;
    public AudioClip levelUpAudio;
    public event Action OnPlayerLeveledUp;

    int experience = 0;
    int expIncrement = 5;
    int level = 1;

    int ExpToNextLevel
    {
        get
        {
            return level * expIncrement;
        }
    }

    public void AddExperience(int exp)
    {
        experience += exp;
        
        float soundPitch = .5f;
        soundPitch += (float)experience / (float)ExpToNextLevel;
        AudioManager.Instance.PlaySound(ExpGetAudio, soundPitch);

        if(experience >= ExpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        experience -= ExpToNextLevel;
        level++;
        AudioManager.Instance.PlaySound(levelUpAudio);
        OnPlayerLeveledUp?.Invoke();
    }
}
