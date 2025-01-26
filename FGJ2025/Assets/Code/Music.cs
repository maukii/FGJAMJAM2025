using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip music;


    void Start() => AudioManager.Instance.PlayMusic(music);
}
