using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic(music);
    }

    private void OnDestroy()
    {
        SoundManager.Instance.StopMusic();
    }
}
