using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioClip sound;
    
    public void Play()
    {
        SoundManager.Instance.Play(sound);
    }
}
